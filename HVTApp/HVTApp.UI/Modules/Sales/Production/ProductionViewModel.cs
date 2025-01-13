using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.ProductionViewModelEntities;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionViewModel : LoadableExportableExpandCollapseViewModel
    {
        private ProductionGroup _selectedInProduction;
        private IEnumerable<ProductionGroup> _groupsInProduction;

        public ObservableCollection<ProductionGroup> GroupsInProduction { get; } = new ObservableCollection<ProductionGroup>();

        /// <summary>
        /// Открыть задачу ТСП, связанную с открытием производства
        /// </summary>
        public ICommandRaiseCanExecuteChanged OpenOrderTaskCommand { get; }

        /// <summary>
        /// Сформировать уведомление об окончании производства оборудования
        /// </summary>
        public ICommandRaiseCanExecuteChanged PrintNoticeCommand { get; }

        public ProductionGroup SelectedInProduction
        {
            get => _selectedInProduction;
            set
            {
                SetProperty(ref _selectedInProduction, value, () => OpenOrderTaskCommand.RaiseCanExecuteChanged());
            }
        }

        private ProductionGroup[] _selectedProductionGroups;
        public object[] SelectedProductionGroups
        {
            get => _selectedProductionGroups;
            set
            {
                if (value.Any() &&
                    value.All(x => x is ProductionGroup))
                {
                    _selectedProductionGroups = value.Select(x => x as ProductionGroup).ToArray();
                }
                else
                {
                    _selectedProductionGroups = null;
                }
                
                this.PrintNoticeCommand.RaiseCanExecuteChanged();
            }
        }


        public ProductionViewModel(IUnityContainer container) : base(container)
        {
            OpenOrderTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var salesUnits = SelectedInProduction.ProductionItems.Select(item => item.Model);
                    var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>()
                        .Find(task =>
                            task.ParentPriceEngineeringTasksId.HasValue &&
                            task.SalesUnits.Any() &&
                            task.SalesUnits.AllContainsInById(salesUnits))
                        .FirstOrDefault();

                    if (priceEngineeringTask != null)
                        container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(new NavigationParameters{{nameof(PriceEngineeringTask), priceEngineeringTask}});
                    else
                        container.Resolve<IMessageService>().Message("Информация", "Задача не найдена...");
                },
                () => SelectedInProduction != null);

            PrintNoticeCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    //выбор получателя уведомления о готовности
                    var contragent = _selectedProductionGroups.First().SalesUnit.Specification.Contract.Contragent;
                    var employees = UnitOfWork.Repository<Employee>().Find(e => e.Company.Id == contragent.Id);
                    var employee = container.Resolve<ISelectService>().SelectItem(employees);
                    if (employee == null) return;

                    //выбор контактного лица ответственного за отгрузку
                    Employee em = null;
                    if (container.Resolve<IMessageService>().ConfirmationDialog("Включить ли в уведомление контактную информацию лица ответственного за отгрузку?"))
                    {
                        var es = UnitOfWork.Repository<User>().GetAll().Select(user => user.Employee);
                        em = container.Resolve<ISelectService>().SelectItem(es);
                    }

                    var document = new Document
                    {
                        Number = new DocumentNumber(),
                        SenderEmployee = UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id),
                        RecipientEmployee = employee,
                        Author = UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.User.Id),
                        Comment = "Уведомление о готовности оборудования"
                        //Comment = $"О готовности оборудования для нужд объектов: {_selectedProductionGroups.Select(x => x.SalesUnit.Facility.ToString().ToStringEnum())}"
                    };
                    UnitOfWork.Repository<Document>().Add(document);
                    UnitOfWork.SaveChanges();

                    var path = container.Resolve<IFileManagerService>().GetPath(document);

                    container.Resolve<IPrintNoticeOfCompletionOfProductionService>()
                        .PrintNoticeOfCompletionOfProduction(_selectedProductionGroups, document, path, em);
                },
                () => _selectedProductionGroups != null && _selectedProductionGroups.Any());
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //оборудование, которое уже размещено в производстве
            _groupsInProduction = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllOfCurrentUser()
                .Where(salesUnit => salesUnit.SignalToStartProduction.HasValue)
                .Select(salesUnit => new ProductionItem(salesUnit))
                .GroupBy(productionItem => new
                {
                    ProductId = productionItem.Model.Product.Id,
                    FacilityId = productionItem.Model.Facility.Id,
                    OrderId = productionItem.Model.Order?.Id,
                    productionItem.IsProduced,
                    productionItem.Model.Comment,
                    productionItem.EndProductionDateExpected,
                    productionItem.Model.EndProductionDateCalculated
                })
                .OrderBy(x => x.Key.EndProductionDateCalculated)
                .ThenBy(x => x.Key.FacilityId)
                .Select(x => new ProductionGroup(x));
        }

        protected override void BeforeGetData()
        {
            GroupsInProduction.Clear();
            SelectedInProduction = null;
        }

        protected override void AfterGetData()
        {
            GroupsInProduction.AddRange(_groupsInProduction);
        }
    }
}