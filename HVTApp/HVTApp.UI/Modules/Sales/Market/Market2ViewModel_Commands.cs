using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel
    {
        #region ICommand

        public ICommand SelectProjectsFolderCommand { get; }
        public ICommand OpenFolderCommand { get; }

        public ICommand ReloadCommand { get; }

        public ICommand ExpandCommand { get; } 
        public ICommand CollapseCommand { get; }

        public event Action<bool> ExpandCollapseEvent;


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

        public ICommand SaveGridCustomisationsCommand { get; }
        public event Action SaveGridCustomisationsEvent;

        #endregion

        #region Commands

        private void PrintOfferCommand_Execute()
        {
            Container.Resolve<IPrintOfferService>().PrintOffer(Offers.SelectedItem.Id, PathGetter.GetPath(SelectedProjectItem.Project));
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

        private void RemoveProjectCommand_Execute()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var units = unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == SelectedProjectItem.Project.Id);

            if (units.Any(x => x.Order != null))
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Нельзя удалить проект, т.к. в нем есть оборудование, размещенное в производстве.");
                return;
            }

            var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удалить проект.", "Вы уверены, что хотите удалить проект?", defaultNo: true);
            if (dr != MessageDialogResult.Yes) return;

            unitOfWork.Repository<SalesUnit>().DeleteRange(units);
            unitOfWork.SaveChanges();
            var remove = ProjectItems.Where(x => x.Project.Id == SelectedProjectItem.Project.Id).ToList();
            remove.ForEach(x => ProjectItems.Remove(x));
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
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == SelectedProjectItem.Project.Id);
            RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(SalesUnit), salesUnits } });
        }

        #endregion

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
    }
}
