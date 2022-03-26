using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel
    {
        #region ICommand

        public SelectProjectsFolderCommand SelectProjectsFolderCommand { get; }
        public OpenFolderCommand OpenFolderCommand { get; }


        public ProjectNewCommand NewProjectCommand { get; }
        public ProjectEditCommand EditProjectCommand { get; }
        public ProjectRemoveCommand RemoveProjectCommand { get; }
        public UnionProjectsCommand UnionProjectsCommand { get; }

        public SpecificationNewCommand NewSpecificationCommand { get; }


        public OfferByProjectCommand OfferByProjectCommand { get; }
        public OfferByOfferCommand OfferByOfferCommand { get; }
        public EditOfferCommand EditOfferCommand { get; }
        public DelegateLogCommand RemoveOfferCommand { get; }
        public PrintOfferCommand PrintOfferCommand { get; }

        public NewTenderCommand NewTenderCommand { get; }
        public EditTenderCommand EditTenderCommand { get; }
        public DelegateLogCommand RemoveTenderCommand { get; }

        public EditTechnicalRequrementsTaskCommand EditTechnicalRequrementsTaskCommand { get; }

        public EditPriceCalculationCommand EditPriceCalculationCommand { get; }
        public PriceCalculationCopyCommand CopyPriceCalculationCommand { get; }

        public StructureCostsCommand StructureCostsCommand { get; }

        public MakeTceTaskCommand MakeTceTaskCommand { get; }

        public MakePriceEngineeringTaskCommand MakePriceEngineeringTaskCommand { get; }

        public OpenTenderLinkCommand OpenTenderLinkCommand { get; }

        #endregion

        #region Commands

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, SelectedProjectItem.Project);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        private void EditTechnicalRequrementsTaskCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), TechnicalRequrementsTasks.SelectedItem.Entity } });
        }

        #endregion
    }
}
