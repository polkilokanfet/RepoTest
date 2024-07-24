using System;
using System.Collections.Generic;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using HVTApp.DataAccess;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.ForManager;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : UnitsContainer<Specification, SpecificationWrapper, SpecificationDetailsViewModel, SpecificationUnitsGroupsViewModel, SalesUnit, AfterSaveSpecificationEvent>
    {
        /// <summary>
        /// Задача на формирование счёта
        /// </summary>
        public ICommand MakeInvoiceForPaymentTaskCommand { get; }

        public ICommand LoadScanCommand { get; private set; }
        public ICommand OpenScanCommand { get; private set; }

        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
            MakeInvoiceForPaymentTaskCommand = new DelegateLogConfirmationCommand(container.Resolve<IMessageService>(),
                "Вы уверены, что хотите создать счёт на оплату?",
                () =>
                {
                    var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;

                    if (this.DetailsViewModel.Entity.PriceEngineeringTasks == null &&
                        this.DetailsViewModel.Entity.TechnicalRequrements == null)
                    {
                        container.Resolve<IMessageService>().Message("Уведомление", "Спецификация не связана ни с одной из задач");
                        return;
                    }

                    if (container.Resolve<IFilesStorageService>().FileContainsInStorage(this.DetailsViewModel.Entity.Id, storageDirectory) == false)
                    {
                        container.Resolve<IMessageService>().Message("Уведомление", "Сначала загрузите сканированную версию спецификации");
                        return;
                    }

                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<TaskInvoiceForPaymentManagerView>(new NavigationParameters(){{nameof(Specification), this.DetailsViewModel.Entity}});
                });
        }

        public override void Load(Specification model, bool isNew, object parameter = null)
        {
            base.Load(model, isNew, parameter);

            //при создании новой спецификации
            if (isNew)
            {
                DetailsViewModel.Item.Date = DateTime.Today;
                var specificationSimpleWrapper = new SpecificationSimpleWrapper(DetailsViewModel.Item.Model);
                EnumerableExtensions.ForEach(GroupsViewModel.Groups, x => x.Specification = specificationSimpleWrapper);
            }

            this.LoadScanCommand = new LoadSpecificationScanCommand(model, this.Container.Resolve<IFilesStorageService>(), this.Container.Resolve<IMessageService>());
            this.OpenScanCommand = new OpenSpecificationScanCommand(model, this.Container.Resolve<IFilesStorageService>(), this.Container.Resolve<IMessageService>());
        }

        protected override IEnumerable<SalesUnit> GetUnits(Specification specification, object parameter = null)
        {
            if(parameter is Project project)
            {
                var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(project.Id);
                return salesUnits
                    .Where(salesUnit => salesUnit.Specification == null)
                    .Where(salesUnit => !salesUnit.IsLoosen && !salesUnit.IsRemoved);
            }
            else
            {
                return ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetBySpecification(specification.Id);
            }
        }

        public override void AfterUnitsLoading()
        {
            var ourCompany = UnitOfWork.Repository<Company>().GetById(GlobalAppProperties.Actual.OurCompany.Id);
            var ourCompanyWrapper = new CompanySimpleWrapper(ourCompany);
            EnumerableExtensions.ForEach(GroupsViewModel.Groups, x => x.Producer = ourCompanyWrapper);
        }
    }
}