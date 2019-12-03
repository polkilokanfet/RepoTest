using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationsViewModel : PriceCalculationLookupListViewModel
    {
        public ICommand NewCalculationCommand { get; }
        public ICommand EditCalculationCommand { get; }

        public ICommand ReloadCommand { get; }

        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup => { ((DelegateCommand)EditCalculationCommand).RaiseCanExecuteChanged(); };

            NewCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), new PriceCalculation() } });
                });


            EditCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), SelectedItem } });
                },
                () => SelectedItem != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var calculations = UnitOfWork.Repository<PriceCalculation>().Find(x => true).OrderByDescending(x => x.TaskOpenMoment);
            this.Load(calculations);
        }
    }
}