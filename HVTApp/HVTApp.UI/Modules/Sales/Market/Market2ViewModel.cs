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
using HVTApp.Model.Services;
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

        private IEnumerable<Tender> _tenders;
        private IEnumerable<ProjectItem> _projectItems;
        private OffersContainer _offers;
        private TendersContainer _tenders1;
        private TechnicalRequrementsTasksContainer _technicalRequrementsTasks;
        private PriceCalculationsContainer _priceCalculations;
        private object _selectedItem;
        private object[] _selectedItems;
        private bool _isShownDoneItems;
        private bool _isShownLoosenItems;
        private bool _isShownOnlyReportsItems;

        public bool IsShownDoneItems
        {
            get => _isShownDoneItems;
            set
            {
                _isShownDoneItems = value;
                IsShownDoneItemsChanged?.Invoke();
                RaisePropertyChanged();
            }
        }
        public event Action IsShownDoneItemsChanged;

        public bool IsShownLoosenItems
        {
            get => _isShownLoosenItems;
            set
            {
                _isShownLoosenItems = value;
                IsShownLoosenItemsChanged?.Invoke();
                RaisePropertyChanged();
            }
        }
        public event Action IsShownLoosenItemsChanged;

        public bool IsShownOnlyReportsItems
        {
            get => _isShownOnlyReportsItems;
            set
            {
                _isShownOnlyReportsItems = value;
                IsShownOnlyReportsItemsChanged?.Invoke();
                RaisePropertyChanged();
            }
        }
        public event Action IsShownOnlyReportsItemsChanged;

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

                RaisePropertyChanged(nameof(Market2ViewModel.Notes));
                AddNoteCommand.RaiseCanExecuteChanged();
                SelectedNote = null;

                Offers.SelectedItem = null;
                Tenders.SelectedItem = null;
                TechnicalRequrementsTasks.SelectedItem = null;
                PriceCalculations.SelectedItem = null;

                SelectedProjectItemChanged?.Invoke(SelectedProjectItem);
            }
        }

        public object[] SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                this.UnionProjectsCommand.RaiseCanExecuteChanged();
            }
        }

        public List<ProjectItem> SelectedProjectItems
        {
            get
            {
                List<ProjectItem> result = new List<ProjectItem>();

                if (SelectedItems == null) return result;

                foreach (var selectedItem in SelectedItems)
                {
                    if (selectedItem is ProjectItem item)
                    {
                        result.Add(item);
                    }
                    else if (selectedItem is ProjectUnitsGroup grp)
                    {
                        result.Add(grp.ProjectItem);
                    }
                }

                return result.Distinct().ToList();
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
                RaisePropertyChanged();
            }
        }

        public TendersContainer Tenders
        {
            get => _tenders1;
            private set
            {
                _tenders1 = value;
                RaisePropertyChanged();
            }
        }

        public TechnicalRequrementsTasksContainer TechnicalRequrementsTasks
        {
            get => _technicalRequrementsTasks;
            private set
            {
                _technicalRequrementsTasks = value;
                RaisePropertyChanged();
            }
        }

        public PriceCalculationsContainer PriceCalculations
        {
            get => _priceCalculations;
            private set
            {
                _priceCalculations = value;
                RaisePropertyChanged();
            }
        }

        public Outlook Outlook { get; }

        public Market2ViewModel(IUnityContainer container) : base(container, loadDataInCtor: false)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();
            ProjectItems = new ProjectItemsCollection(this, _eventAggregator);

            Outlook = new Outlook(this, container);

            #region Commands definition

            //команды
            NewProjectCommand = new ProjectNewCommand(this.RegionManager);
            EditProjectCommand = new ProjectEditCommand(this, this.RegionManager);
            RemoveProjectCommand = new ProjectRemoveCommand(this, this.Container);
            UnionProjectsCommand = new UnionProjectsCommand(this, this.Container);

            NewSpecificationCommand = new SpecificationNewCommand(this, this.Container, this.RegionManager);

            EditOfferCommand = new EditOfferCommand(this, this.RegionManager);
            RemoveOfferCommand = new DelegateLogCommand(() => Offers.RemoveSelectedItem(), () => Offers?.SelectedItem != null);
            PrintOfferCommand = new PrintOfferCommand(this, this.Container);
            OfferByProjectCommand = new OfferByProjectCommand(this, this.RegionManager);
            OfferByOfferCommand = new OfferByOfferCommand(this, this.RegionManager);

            NewTenderCommand = new NewTenderCommand(this, this.Container);
            EditTenderCommand = new EditTenderCommand(this, this.Container);
            RemoveTenderCommand = new DelegateLogCommand(() => Tenders.RemoveSelectedItem(), () => Tenders?.SelectedItem != null);

            EditTechnicalRequrementsTaskCommand = new EditTechnicalRequrementsTaskCommand(this, this.RegionManager);
            
            EditPriceCalculationCommand = new EditPriceCalculationCommand(this, this.RegionManager);
            CopyPriceCalculationCommand = new PriceCalculationCopyCommand(this, this.RegionManager);


            StructureCostsCommand = new StructureCostsCommand(this, this.RegionManager, this.UnitOfWork);

            SelectProjectsFolderCommand = new SelectProjectsFolderCommand(container.Resolve<IFileManagerService>());
            OpenFolderCommand = new OpenFolderCommand(this, container.Resolve<IFileManagerService>());

            MakeTceTaskCommand = new MakeTceTaskCommand(this, this.UnitOfWork, this.RegionManager);

            OpenTenderLinkCommand = new OpenTenderLinkCommand(this);

            #endregion

            #region Subscribe to Events


            //подписка на выбор сущностей
            _eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedPriceCalculationChangedEvent>().Subscribe(calculation => CopyPriceCalculationCommand.RaiseCanExecuteChanged());

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

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            RemoveProjectCommand.RaiseCanExecuteChanged();
            EditProjectCommand.RaiseCanExecuteChanged();
            NewSpecificationCommand.RaiseCanExecuteChanged();
            StructureCostsCommand.RaiseCanExecuteChanged();
            MakeTceTaskCommand.RaiseCanExecuteChanged();
            OpenFolderCommand.RaiseCanExecuteChanged();
            OfferRaiseCanExecuteChanged();
            TenderRaiseCanExecuteChanged();
        }

        private void OfferRaiseCanExecuteChanged()
        {
            (EditOfferCommand).RaiseCanExecuteChanged();
            (RemoveOfferCommand).RaiseCanExecuteChanged();
            (PrintOfferCommand).RaiseCanExecuteChanged();
            (OfferByOfferCommand).RaiseCanExecuteChanged();
            (OfferByProjectCommand).RaiseCanExecuteChanged();
        }

        private void TenderRaiseCanExecuteChanged()
        {
            NewTenderCommand.RaiseCanExecuteChanged();
            EditTenderCommand.RaiseCanExecuteChanged();
            RemoveTenderCommand.RaiseCanExecuteChanged();
            OpenTenderLinkCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
