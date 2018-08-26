using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI
{
    public partial class UiModule : ModuleBase
    {
        private readonly IDialogService _dialogService;
        private readonly ISelectService _selectService;
        private readonly IUpdateDetailsService _updateDetailsService;

        public UiModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService,
            ISelectService selectService, IUpdateDetailsService updateDetailsService) : base(container, regionManager)
        {
            _dialogService = dialogService;
            _selectService = selectService;
            _updateDetailsService = updateDetailsService;
        }

        protected override void RegisterTypes()
        {
            RegisterViews();

            _dialogService.Register<OfferUnitsDetailsViewModel, OfferUnitsDetailsView>();
            _dialogService.Register<UnitGroupViewModel, ProjectUnitGroupWindow>();
            _dialogService.Register<OfferUnitsGroupDetailsViewModel, OfferUnitsGroupDetailsWindow>();

            Container.RegisterViewForNavigation<PaymentPlannedListGeneratorView>();

        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}