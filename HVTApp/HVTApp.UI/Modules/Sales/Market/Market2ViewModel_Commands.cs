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

        public DelegateLogCommand SelectProjectsFolderCommand { get; }
        public DelegateLogCommand OpenFolderCommand { get; }


        public ProjectNewCommand NewProjectCommand { get; }
        public ProjectEditCommand EditProjectCommand { get; }
        public ProjectRemoveCommand RemoveProjectCommand { get; }

        public SpecificationNewCommand NewSpecificationCommand { get; }


        public DelegateLogCommand NewOfferByProjectCommand { get; }
        public DelegateLogCommand NewOfferByOfferCommand { get; }
        public DelegateLogCommand EditOfferCommand { get; }
        public DelegateLogCommand RemoveOfferCommand { get; }
        public DelegateLogCommand PrintOfferCommand { get; }

        public DelegateLogCommand NewTenderCommand { get; }
        public DelegateLogCommand EditTenderCommand { get; }
        public DelegateLogCommand RemoveTenderCommand { get; }

        public DelegateLogCommand EditTechnicalRequrementsTaskCommand { get; }

        public DelegateLogCommand EditPriceCalculationCommand { get; }
        public PriceCalculationCopyCommand CopyPriceCalculationCommand { get; }

        public DelegateLogCommand StructureCostsCommand { get; }

        public DelegateLogCommand MakeTceTaskCommand { get; }


        public OpenTenderLinkCommand OpenTenderLinkCommand { get; }

        #endregion

        #region Commands

        private void PrintOfferCommand_Execute()
        {
            Container.Resolve<IPrintOfferService>().PrintOffer(Offers.SelectedItem.Id, PathGetter.GetPath(Offers.SelectedItem.Entity));
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
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Project.Id == SelectedProjectItem.Project.Id);
            RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(SalesUnit), salesUnits } });
        }

        private void OpenFolderCommand_Execute()
        {
            var path = PathGetter.GetPath(SelectedProjectItem.Project);
            Process.Start("explorer", $"\"{path}\"");
        }

        private void SelectProjectsFolderCommand_Execute()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ProjectsFolderPath = dialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void MakeTceTaskCommand_Execute()
        {
            if (SelectedItem != null)
            {
                List<SalesUnit> salesUnits = new List<SalesUnit>();
                
                //если выбран проект целиком
                if (SelectedItem is ProjectItem projectItem)
                {
                    salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(projectItem.Project.Id).Where(salesUnit => !salesUnit.IsRemoved).ToList();
                }
                
                //если выбрано конкретное оборудование
                else if (SelectedItem is ProjectUnitsGroup projectUnitsGroup)
                {
                    salesUnits = projectUnitsGroup.SalesUnits.ToList();
                }

                RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(SalesUnit), salesUnits } });
            }
        }

        private void EditTechnicalRequrementsTaskCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), TechnicalRequrementsTasks.SelectedItem.Entity } });
        }

        private void EditPriceCalculationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<PriceCalculations.View.PriceCalculationView>(new NavigationParameters
            {
                {nameof(PriceCalculation), PriceCalculations.SelectedItem.Entity}
            });
        }

        #endregion
    }
}
