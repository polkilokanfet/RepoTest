using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Commands;
using HVTApp.UI.Modules.Sales.Market.Converters;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.Modules.Sales.ViewModels.Containers;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.Specifications;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class Market2ViewModel : LoadableExportableExpandCollapseViewModel, ISelectedProjectItemChanged
    {
        private MarketProjectItem _selectedProjectItem;
        private IModelsStore ModelsStore => Container.Resolve<IModelsStore>();

        private object _selectedItem;
        private object[] _selectedItems;
        private bool _isShownDoneItems = true;
        private bool _isShownLoosenItems = true;
        private bool _isShownOnlyReportsItems = true;

        public NotesViewModel NotesViewModel { get; }

        public bool IsShownDoneItems
        {
            get => _isShownDoneItems;
            set => this.SetProperty(ref _isShownDoneItems, value);
        }

        public bool IsShownLoosenItems
        {
            get => _isShownLoosenItems;
            set => this.SetProperty(ref _isShownLoosenItems, value);
        }

        public bool IsShownOnlyReportsItems
        {
            get => _isShownOnlyReportsItems;
            set => this.SetProperty(ref _isShownOnlyReportsItems, value);
        }

        public ProjectItemsCollection ProjectItems { get; }

        #region SelectedItem

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.SetProperty(ref _selectedItem, value, () =>
                {
                    switch (value)
                    {
                        case null:
                        {
                            return;
                        }
                        case MarketProjectItem marketProjectItem:
                        {
                            SelectedProjectItem = marketProjectItem;
                            SelectedProjectUnitsGroup = null;
                            break;
                        }
                        case MarketSalesUnitsItem marketSalesUnitsItem:
                        {
                            SelectedProjectUnitsGroup = marketSalesUnitsItem;
                            SelectedProjectItem = SelectedProjectUnitsGroup.MarketProjectItem;
                            break;
                        }
                    }
                });
            }
        }

        public MarketProjectItem SelectedProjectItem
        {
            get => _selectedProjectItem;
            set
            {
                this.SetProperty(ref _selectedProjectItem, value, () =>
                {
                    ProjectRaiseCanExecuteChanged();

                    this.NotesViewModel.SelectProject(_selectedProjectItem.Project.Id);

                    Offers.SelectedItem = null;
                    Tenders.SelectedItem = null;
                    PriceEngineeringTasks.SelectedItem = null;
                    TechnicalRequrementsTasks.SelectedItem = null;
                    PriceCalculations.SelectedItem = null;

                    SelectedProjectItemChanged?.Invoke(SelectedProjectItem);
                });
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

        public List<MarketProjectItem> SelectedProjectItems
        {
            get
            {
                var result = new List<MarketProjectItem>();

                if (SelectedItems == null) return result;

                foreach (var selectedItem in SelectedItems)
                {
                    if (selectedItem is MarketProjectItem marketProjectItem)
                    {
                        result.Add(marketProjectItem);
                    }
                    else if (selectedItem is MarketSalesUnitsItem marketSalesUnitsItem)
                    {
                        result.Add(marketSalesUnitsItem.MarketProjectItem);
                    }
                }

                return result.Distinct().ToList();
            }
        }

        public MarketSalesUnitsItem SelectedProjectUnitsGroup { get; set; }

        public event Action<MarketProjectItem> SelectedProjectItemChanged;

        #endregion

        #region Containers

        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }
        public PriceEngineeringTasksContainer PriceEngineeringTasks { get; }
        public TechnicalRequrementsTasksContainer TechnicalRequrementsTasks { get; }
        public PriceCalculationsContainer PriceCalculations { get; }
        public SpecificationsViewModelForManager Specifications { get; }

        #endregion

        #region ICommand

        public SelectProjectsFolderCommand SelectProjectsFolderCommand { get; }
        public OpenFolderCommand OpenFolderCommand { get; }


        public ProjectNewCommand NewProjectCommand { get; }
        public ProjectEditCommand EditProjectCommand { get; }
        public ProjectRemoveCommand RemoveProjectCommand { get; }
        public UnionProjectsCommand UnionProjectsCommand { get; }

        public StructureCostsCommand StructureCostsCommand { get; }

        public MakeTceTaskCommand MakeTceTaskCommand { get; }

        public MakePriceEngineeringTaskCommand MakePriceEngineeringTaskCommand { get; }

        #endregion

        public Outlook Outlook { get; }

        public Market2ViewModel(IUnityContainer container) : base(container, loadDataInCtor: false)
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();
            ProjectItems = new ProjectItemsCollection(eventAggregator, this);

            Offers = new OffersContainer(Container, this);
            Tenders = new TendersContainer(Container, this);
            PriceEngineeringTasks = new PriceEngineeringTasksContainer(Container, this);
            TechnicalRequrementsTasks = new TechnicalRequrementsTasksContainer(Container, this);
            PriceCalculations = new PriceCalculationsContainer(Container, this);
            Specifications = new SpecificationsViewModelForManager(Container, this);

            Outlook = new Outlook(this, container);

            #region Commands definition

            //команды
            NewProjectCommand = new ProjectNewCommand(this.RegionManager);
            EditProjectCommand = new ProjectEditCommand(this, this.RegionManager);
            RemoveProjectCommand = new ProjectRemoveCommand(this, this.Container);
            UnionProjectsCommand = new UnionProjectsCommand(this, this.Container);

            StructureCostsCommand = new StructureCostsCommand(this, this.RegionManager, this.UnitOfWork);

            SelectProjectsFolderCommand = new SelectProjectsFolderCommand(container.Resolve<IFileManagerService>());
            OpenFolderCommand = new OpenFolderCommand(this, container.Resolve<IFileManagerService>());

            MakeTceTaskCommand = new MakeTceTaskCommand(this, this.UnitOfWork, this.RegionManager, container.Resolve<IMessageService>());

            MakePriceEngineeringTaskCommand = new MakePriceEngineeringTaskCommand(this, this.UnitOfWork, this.RegionManager);

            #endregion

            NotesViewModel = new NotesViewModel(container.Resolve<IUnitOfWork>());

            ProjectToProjectTypeNameConverter.ProductTypes = UnitOfWork.Repository<ProjectType>().GetAll();

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

        private IEnumerable<SalesUnit> _salesUnits;
        protected override void GetData()
        {
            UnitOfWork = ModelsStore.UnitOfWork;

            _salesUnits = (GlobalAppProperties.User.RoleCurrent == Role.Admin
                    ? UnitOfWork.Repository<SalesUnit>().GetAll()
                    : ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetAllOfCurrentUserForMarketView()).ToList();

            Offers.Load(UnitOfWork);
            Tenders.Load(UnitOfWork);
            PriceEngineeringTasks.Load(UnitOfWork);
            TechnicalRequrementsTasks.Load(UnitOfWork);
            PriceCalculations.Load(UnitOfWork);
        }

        protected override void BeforeGetData()
        {
            Offers.Clear();
            Tenders.Clear();
            PriceEngineeringTasks.Clear();
            TechnicalRequrementsTasks.Clear();
            PriceCalculations.Clear();
        }

        protected override void AfterGetData()
        {
            ProjectItems.Load(_salesUnits);
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            RemoveProjectCommand.RaiseCanExecuteChanged();
            EditProjectCommand.RaiseCanExecuteChanged();
            StructureCostsCommand.RaiseCanExecuteChanged();
            MakeTceTaskCommand.RaiseCanExecuteChanged();
            MakePriceEngineeringTaskCommand.RaiseCanExecuteChanged();
            OpenFolderCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
