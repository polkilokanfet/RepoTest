using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using OfferView = HVTApp.Modules.Sales.Views.OfferView;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        #region ViewModels

        public OfferLookupListViewModel OfferListViewModel { get; }
        public TenderLookupListViewModel TenderListViewModel { get; }
        public UnitLookupListViewModel UnitListViewModel { get; }
        public NoteLookupListViewModel NoteListViewModel { get; }

        #endregion

        #region ICommand

        public ICommand NewProjectCommand { get; }
        public ICommand EditProjectCommand { get; }

        public ICommand NewSpecificationCommand { get; }


        public ICommand NewOfferByProjectCommand { get; }
        public ICommand NewOfferByOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }


        #endregion

        #region Ctor

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            //контексты
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            TenderListViewModel = container.Resolve<TenderLookupListViewModel>();
            UnitListViewModel = container.Resolve<UnitLookupListViewModel>();
            NoteListViewModel = container.Resolve<NoteLookupListViewModel>();

            //привязываем команды к соответствующим моделям
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = OfferListViewModel.PrintOfferCommand;
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);

            NewTenderCommand = TenderListViewModel.NewItemCommand;
            EditTenderCommand = TenderListViewModel.EditItemCommand;
            RemoveTenderCommand = TenderListViewModel.RemoveItemCommand;

            NewNoteCommand = NoteListViewModel.NewItemCommand;
            EditNoteCommand = NoteListViewModel.EditItemCommand;
            RemoveNoteCommand = NoteListViewModel.RemoveItemCommand;

            //реакция на смену выбранного проекта
            SelectedLookupChanged += project =>
            {
                UnitListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                TenderListViewModel.Load(project.Tenders);
                NoteListViewModel.Load(project.Notes);

                ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitListViewModel.Load(offer.OfferUnits);
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            };
        }

        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", SelectedItem } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { {"prj", SelectedItem} });
        }

        private void NewProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        #endregion

        protected override async Task<IEnumerable<ProjectLookup>> GetLookups()
        {
            var lookups = await base.GetLookups();
            return lookups.Where(x => x.Manager.Id == CommonOptions.User.Id);
        }

        #region OfferCommands

        /// <summary>
        /// ТКП по существующему ТКП
        /// </summary>
        private void NewOfferByOfferCommand_Execute()
        {
            var prms = new NavigationParameters {{"offer", OfferListViewModel.SelectedItem}, {"units", OfferListViewModel.SelectedLookup.OfferUnits.Select(x => x.Entity)}};
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// ТКП по проекту
        /// </summary>
        private void NewOfferByProjectCommand_Execute()
        {
            var offer = new Offer
            {
                Project = SelectedItem,
                ValidityDate = DateTime.Today.AddDays(90),
                Author = CommonOptions.User.Employee
            };

            var units = new List<OfferUnit>();
            foreach (var unit in SelectedLookup.SalesUnits.Select(x => x.Entity))
            {
                var offerUnit = new OfferUnit
                {
                    Cost = unit.Cost,
                    Facility = unit.Facility,
                    Product = unit.Product, Offer = offer,
                    PaymentConditionSet = unit.PaymentConditionSet,
                    ProductionTerm = unit.ProductionTerm
                };
                units.Add(offerUnit);
            }

            var prms = new NavigationParameters {{"offer", offer}, {"units", units}};
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// Изменить ТКП
        /// </summary>
        private void EditOfferCommand_Execute()
        {
            var prms = new NavigationParameters {{"offer", OfferListViewModel.SelectedItem}};
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
            
        }
        

        #endregion

    }
}
