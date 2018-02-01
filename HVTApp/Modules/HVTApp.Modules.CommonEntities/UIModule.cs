using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using HVTApp.UI.Wrapper;
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
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}