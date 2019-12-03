using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using HVTApp.UI.PriceCalculations;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel
    {
        #region ICommand

        public ICommand NewProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand RemoveProjectCommand { get; }

        public ICommand NewSpecificationCommand { get; }


        public ICommand NewOfferByProjectCommand { get; }
        public ICommand NewOfferByOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }

        public ICommand StructureCostsCommand { get; }

        #endregion

        #region Commands

        private async void PrintOfferCommand_Execute()
        {
            await Container.Resolve<IPrintOfferService>().PrintOfferAsync(Offers.SelectedItem.Id);
        }

        private void EditTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, Tenders.SelectedItem.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, SelectedProjectItem.Project);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }


        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Project), SelectedProjectItem.Project } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { { nameof(Project), SelectedProjectItem.Project} });
        }

        private void NewProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        #region OfferCommands

        /// <summary>
        /// ТКП по существующему ТКП
        /// </summary>
        private void NewOfferByOfferCommand_Execute()
        {
            var prms = new NavigationParameters { { "offer", Offers.SelectedItem.Entity } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// ТКП по проекту
        /// </summary>
        private void NewOfferByProjectCommand_Execute()
        {
            var prms = new NavigationParameters { { "project", SelectedProjectItem.Project } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// Изменить ТКП
        /// </summary>
        private void EditOfferCommand_Execute()
        {
            var prms = new NavigationParameters { { "offer", Offers.SelectedItem.Entity }, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }


        #endregion

        private void StructureCostsCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(SalesUnit), SelectedProjectItem.SalesUnits } });
        }

        #endregion
    }
}
