using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
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

        public override void Load()
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