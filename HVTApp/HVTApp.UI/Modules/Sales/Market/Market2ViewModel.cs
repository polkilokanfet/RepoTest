using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.Modules.Sales.ViewModels.Containers;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel : LoadableExportableExpandCollapseViewModel
    {
        private ProjectItem _selectedProjectItem;
        private readonly IEventAggregator _eventAggregator;
        private IModelsStore ModelsStore => Container.Resolve<IModelsStore>();

        public ProjectItemsCollection ProjectItems { get; } 

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                if (value != null)
                {
                    if (value is ProjectItem projectItem)
                    {
                        SelectedProjectItem = projectItem;
                    }

                    if (value is ProjectUnitsGroup projectUnitsGroup)
                    {
                        SelectedProjectUnitsGroup = projectUnitsGroup;
                        if (SelectedProjectItem != SelectedProjectUnitsGroup.ProjectItem)
                        {
                            SelectedProjectItem = SelectedProjectUnitsGroup.ProjectItem;
                        }
                    }
                }
            }
        }

        public ProjectItem SelectedProjectItem
        {
            get => _selectedProjectItem;
            set
            {
                _selectedProjectItem = value;
                ProjectRaiseCanExecuteChanged();

                if (value != null)
                {
                    _eventAggregator.GetEvent<SelectedProjectChangedEvent>().Publish(value.Project);
                }

                OnPropertyChanged(nameof(Market2ViewModel.Notes));
                (AddNoteCommand).RaiseCanExecuteChanged();
                SelectedNote = null;

                Offers.SelectedItem = null;
                Tenders.SelectedItem = null;
                TechnicalRequrementsTasks.SelectedItem = null;
                PriceCalculations.SelectedItem = null;

                SelectedProjectItemChanged?.Invoke(SelectedProjectItem);
            }
        }

        public ProjectUnitsGroup SelectedProjectUnitsGroup { get; set; }

        public event Action<ProjectItem> SelectedProjectItemChanged;

        public OffersContainer Offers
        {
            get => _offers;
            private set
            {
                _offers = value;
                OnPropertyChanged();
            }
        }

        public TendersContainer Tenders
        {
            get => _tenders1;
            private set
            {
                _tenders1 = value;
                OnPropertyChanged();
            }
        }

        public TechnicalRequrementsTasksContainer TechnicalRequrementsTasks
        {
            get => _technicalRequrementsTasks;
            private set
            {
                _technicalRequrementsTasks = value;
                OnPropertyChanged();
            }
        }

        public PriceCalculationsContainer PriceCalculations
        {
            get => _priceCalculations;
            private set
            {
                _priceCalculations = value;
                OnPropertyChanged();
            }
        }

        public Market2ViewModel(IUnityContainer container) : base(container, loadDataInCtor: false)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();
            ProjectItems = new ProjectItemsCollection(this, _eventAggregator);

            #region Commands definition

            //команды
            NewProjectCommand = new ProjectNewCommand(this.RegionManager);
            EditProjectCommand = new ProjectEditCommand(this, this.RegionManager);
            RemoveProjectCommand = new ProjectRemoveCommand(this, this.Container);

            NewSpecificationCommand = new SpecificationNewCommand(this, this.Container, this.RegionManager);

            EditOfferCommand = new DelegateLogCommand(EditOfferCommand_Execute, () => Offers?.SelectedItem != null);
            RemoveOfferCommand = new DelegateLogCommand(() => Offers.RemoveSelectedItem(), () => Offers?.SelectedItem != null);
            PrintOfferCommand = new DelegateLogCommand(PrintOfferCommand_Execute, () => Offers?.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateLogCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectItem != null);
            NewOfferByOfferCommand = new DelegateLogCommand(NewOfferByOfferCommand_Execute, () => Offers?.SelectedItem != null);

            NewTenderCommand = new DelegateLogCommand(NewTenderCommand_Execute, () => SelectedProjectItem != null);
            EditTenderCommand = new DelegateLogCommand(EditTenderCommand_Execute, () => Tenders?.SelectedItem != null);
            RemoveTenderCommand = new DelegateLogCommand(() => Tenders.RemoveSelectedItem(), () => Tenders?.SelectedItem != null);

            EditTechnicalRequrementsTaskCommand = new DelegateLogCommand(EditTechnicalRequrementsTaskCommand_Execute, () => TechnicalRequrementsTasks?.SelectedItem != null);
            
            EditPriceCalculationCommand = new DelegateLogCommand(EditPriceCalculationCommand_Execute);
            CopyPriceCalculationCommand = new PriceCalculationCopyCommand(this, this.RegionManager);


            StructureCostsCommand = new DelegateLogCommand(StructureCostsCommand_Execute, () => SelectedProjectItem != null);

            SelectProjectsFolderCommand = new DelegateLogCommand(SelectProjectsFolderCommand_Execute);
            OpenFolderCommand = new DelegateLogCommand(OpenFolderCommand_Execute, () => SelectedProjectItem != null);

            MakeTceTaskCommand = new DelegateLogCommand(MakeTceTaskCommand_Execute, () => SelectedProjectItem != null);

            OpenTenderLinkCommand = new OpenTenderLinkCommand(this);

            #endregion

            #region Subscribe to Events


            //подписка на выбор сущностей
            _eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedPriceCalculationChangedEvent>().Subscribe(calculation => ((DelegateCommandBase)CopyPriceCalculationCommand).RaiseCanExecuteChanged());

            #endregion

            InitNotes();

            //загрузка данных
            BeforeGetData();
            GetData();
            AfterGetData();
            IsLoaded = true;
        }

        protected override void ReloadCommand_Execute()
        {
            ModelsStore.Refresh();
            base.ReloadCommand_Execute();
        }

        protected override void GetData()
        {
            UnitOfWork = ModelsStore.UnitOfWork;

            _tenders = UnitOfWork.Repository<Tender>().GetAll();

            var salesUnits = (GlobalAppProperties.User.RoleCurrent == Role.Admin
                    ? UnitOfWork.Repository<SalesUnit>().GetAll()
                    : ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetAllOfCurrentUserForMarketView()).ToList();

            var items = salesUnits
                .GroupBy(unit => unit, new SalesUnitsMarketViewComparer())
                .Select(units => new ProjectItem(units, _eventAggregator))
                .ToList();

            _projectItems = items
                .OrderBy(projectItem => projectItem.DaysToStartProduction)
                .ThenBy(projectItem => projectItem.OrderInTakeDate)
                .ToList();

            Offers = Container.Resolve<OffersContainer>();
            Tenders = Container.Resolve<TendersContainer>();
            TechnicalRequrementsTasks = Container.Resolve<TechnicalRequrementsTasksContainer>();
            PriceCalculations = Container.Resolve<PriceCalculationsContainer>();
        }

        protected override void BeforeGetData()
        {
            ProjectItem.AllTenders.Clear();
            ProjectItems.Clear();
        }

        protected override void AfterGetData()
        {
            ProjectItem.AllTenders.AddRange(_tenders);
            ProjectItems.AddRange(_projectItems);
        }

        private IEnumerable<Tender> _tenders;
        private IEnumerable<ProjectItem> _projectItems;
        private OffersContainer _offers;
        private TendersContainer _tenders1;
        private TechnicalRequrementsTasksContainer _technicalRequrementsTasks;
        private PriceCalculationsContainer _priceCalculations;
        private object _selectedItem;


        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommandBase)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommandBase)EditProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommandBase)NewSpecificationCommand).RaiseCanExecuteChanged();
            (StructureCostsCommand).RaiseCanExecuteChanged();
            (MakeTceTaskCommand).RaiseCanExecuteChanged();
            (OpenFolderCommand).RaiseCanExecuteChanged();
            OfferRaiseCanExecuteChanged();
            TenderRaiseCanExecuteChanged();
        }

        private void OfferRaiseCanExecuteChanged()
        {
            (EditOfferCommand).RaiseCanExecuteChanged();
            (RemoveOfferCommand).RaiseCanExecuteChanged();
            (PrintOfferCommand).RaiseCanExecuteChanged();
            (NewOfferByOfferCommand).RaiseCanExecuteChanged();
            (NewOfferByProjectCommand).RaiseCanExecuteChanged();
        }

        private void TenderRaiseCanExecuteChanged()
        {
            (NewTenderCommand).RaiseCanExecuteChanged();
            (EditTenderCommand).RaiseCanExecuteChanged();
            (RemoveTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommandBase)OpenTenderLinkCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
