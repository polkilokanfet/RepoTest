using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationsViewModel : PriceCalculationLookupListViewModel
    {
        public ICommand NewCalculationCommand { get; }
        public ICommand EditCalculationCommand { get; }

        public ICommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            PreLoad();
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateCommand)EditCalculationCommand).RaiseCanExecuteChanged();
            };

            NewCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), new PriceCalculation() } });
                });


            EditCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), SelectedItem } });
                },
                () => SelectedItem != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        private void PreLoad()
        {
            if(GlobalAppProperties.User.RoleCurrent != Role.Admin) return;

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var calculations = unitOfWork.Repository<PriceCalculation>().Find(x => true).Select(x => new PriceCalculationWrapper(x));
            var list = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(calculations);

            foreach (var calculation in list)
            {
                foreach (var item in calculation.PriceCalculationItems)
                {
                    if (item.OrderInTakeDate == null) item.OrderInTakeDate = item.SalesUnits.First().OrderInTakeDate;
                    if (item.RealizationDate == null) item.RealizationDate = item.SalesUnits.First().RealizationDateCalculated;
                    if (item.PaymentConditionSet == null) item.PaymentConditionSet = item.SalesUnits.First().PaymentConditionSet;
                }
            }

            if (list.IsValid && list.IsChanged)
            {
                list.AcceptChanges();
                unitOfWork.SaveChanges();
                Container.Resolve<IMessageService>().ShowOkMessageDialog("", "Даты добавлены");
            }
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            IEnumerable<PriceCalculation> calculations = CurrentUserIsManager 
                ? UnitOfWork.Repository<PriceCalculation>().Find(IsCalculationOfManager) 
                : UnitOfWork.Repository<PriceCalculation>().Find(x => x.TaskOpenMoment.HasValue);

            this.Load(calculations.OrderByDescending(x => x.TaskOpenMoment));
        }

        private bool IsCalculationOfManager(PriceCalculation calculation)
        {
            return calculation.PriceCalculationItems.First().SalesUnits.First().Project.Manager.IsAppCurrentUser();
        }
    }
}