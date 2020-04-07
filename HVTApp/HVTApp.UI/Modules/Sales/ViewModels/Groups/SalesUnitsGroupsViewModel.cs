using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class SalesUnitsGroupsViewModel : BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, IGroupsViewModel<SalesUnit, ProjectWrapper>
    {
        protected override bool CanRemoveGroup(SalesUnitsWrappersGroup @group)
        {
            var result = @group.Model.Order == null;
            if(result == false)
                Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Удаление невозможно, т.к. это оборудование размещено в производстве.");
            return result;
        }

        private ProjectWrapper _projectWrapper;

        public ICommand ChangeProducerCommand { get; }


        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
            ChangeProducerCommand = new DelegateCommand<SalesUnitsWrappersGroup>(ChangeProducerCommand_Execute, ChangeProducerCommand_CanExecute);
        }

        private bool ChangeProducerCommand_CanExecute(SalesUnitsWrappersGroup wrappersGroup)
        {
            return wrappersGroup?.Specification == null;
        }

        private void ChangeProducerCommand_Execute(SalesUnitsWrappersGroup wrappersGroup)
        {
            var producers = UnitOfWork.Repository<Company>().Find(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
            var producer = Container.Resolve<ISelectService>().SelectItem(producers, wrappersGroup.Producer?.Id);
            if (producer == null) return;
            producer = UnitOfWork.Repository<Company>().GetById(producer.Id);
            wrappersGroup.Producer = new CompanyWrapper(producer);
        }


        protected override List<SalesUnitsWrappersGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units.GroupBy(x => x, new SalesUnitsGroupsComparer())
                        .OrderByDescending(x => x.Key.Cost)
                        .Select(x => new SalesUnitsWrappersGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _projectWrapper = parentWrapper;
        }

        protected override DateTime GetPriceDate(SalesUnitsWrappersGroup @group)
        {
            return @group.OrderInTakeDate < DateTime.Today ? @group.OrderInTakeDate : DateTime.Today;
        }


        #region AddCommand

        protected override void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_projectWrapper != null) salesUnit.Project = _projectWrapper;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new SalesUnitsWrappersGroup(units.ToList());
            Groups.Add(group);
            RefreshPrice(group);
            Groups.SelectedGroup = group;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (Groups.SelectedGroup == null)
            {
                var paymentConditionSet =
                    UnitOfWork.Repository<PaymentConditionSet>()
                        .Find(x => x.Id == GlobalAppProperties.Actual.PaymentConditionSet.Id).First();
                salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                salesUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            salesUnitWrapper.Cost = Groups.SelectedGroup.Cost;
            salesUnitWrapper.Facility = Groups.SelectedGroup.Facility;
            salesUnitWrapper.PaymentConditionSet = Groups.SelectedGroup.PaymentConditionSet;
            salesUnitWrapper.ProductionTerm = Groups.SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = Groups.SelectedGroup.Product;
            salesUnitWrapper.DeliveryDateExpected = Groups.SelectedGroup.DeliveryDateExpected;
                
            //создаем зависимое оборудование
            foreach (var prodIncl in Groups.SelectedGroup.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                salesUnitWrapper.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
            }
        }

        /// <summary>
        /// Клонирование юнитов по образцу.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private IEnumerable<SalesUnit> CloneSalesUnits(SalesUnit salesUnit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var unit = (SalesUnit)salesUnit.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                yield return unit;
            }
            
        }

        #endregion


    }
}