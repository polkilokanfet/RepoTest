﻿using System;
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
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.Modules.Sales.ViewModels.Containers;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.Specifications;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel : LoadableExportableExpandCollapseViewModel, ISelectedProjectItemChanged
    {
        private ProjectItem _selectedProjectItem;
        private readonly IEventAggregator _eventAggregator;
        private IModelsStore ModelsStore => Container.Resolve<IModelsStore>();

        private IEnumerable<Tender> _tenders;
        private IEnumerable<ProjectItem> _projectItems;
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

                this.NotesViewModel.SelectProject(_selectedProjectItem.Project);

                Offers.SelectedItem = null;
                Tenders.SelectedItem = null;
                PriceEngineeringTasks.SelectedItem = null;
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
                var result = new List<ProjectItem>();

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
            _eventAggregator = Container.Resolve<IEventAggregator>();
            ProjectItems = new ProjectItemsCollection(this, _eventAggregator);

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

            Offers.Load(UnitOfWork);
            Tenders.Load(UnitOfWork);
            PriceEngineeringTasks.Load(UnitOfWork);
            TechnicalRequrementsTasks.Load(UnitOfWork);
            PriceCalculations.Load(UnitOfWork);
        }

        protected override void BeforeGetData()
        {
            ProjectItem.AllTenders.Clear();
            ProjectItems.Clear();
            Offers.Clear();
            Tenders.Clear();
            PriceEngineeringTasks.Clear();
            TechnicalRequrementsTasks.Clear();
            PriceCalculations.Clear();
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
            StructureCostsCommand.RaiseCanExecuteChanged();
            MakeTceTaskCommand.RaiseCanExecuteChanged();
            MakePriceEngineeringTaskCommand.RaiseCanExecuteChanged();
            OpenFolderCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
