﻿using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class ProjectNewCommand : DelegateLogCommand
    {
        private readonly IRegionManager _regionManager;
        public ProjectNewCommand(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }
    }
}