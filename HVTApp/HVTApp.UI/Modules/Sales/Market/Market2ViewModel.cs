using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Market.Items;
using HVTApp.UI.Modules.Sales.ViewModels.Containers;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel : LoadableExportableExpandCollapseViewModel
    {
        private ProjectItem _selectedProjectItem;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageService _messageService;
        private readonly IModelsStore _modelsStore;

        public ObservableCollection<ProjectItem> ProjectItems { get; } = new ObservableCollection<ProjectItem>();

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                if (value != null)
                {
                    if (value is ProjectItem)
                    {
                        SelectedProjectItem = (ProjectItem)value;
                    }

                    if (value is ProjectUnitsGroup)
                    {
                        SelectedProjectUnitsGroup = (ProjectUnitsGroup)value;
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
            get { return _selectedProjectItem; }
            set
            {
                _selectedProjectItem = value;
                ProjectRaiseCanExecuteChanged();

                if (value != null)
                {
                    _eventAggregator.GetEvent<SelectedProjectChangedEvent>().Publish(value.Project);
                }

                OnPropertyChanged(nameof(Market2ViewModel.Notes));
                ((DelegateCommand)AddNoteCommand).RaiseCanExecuteChanged();
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
            get { return _offers; }
            private set
            {
                _offers = value;
                OnPropertyChanged();
            }
        }

        public TendersContainer Tenders
        {
            get { return _tenders1; }
            private set
            {
                _tenders1 = value;
                OnPropertyChanged();
            }
        }

        public TechnicalRequrementsTasksContainer TechnicalRequrementsTasks
        {
            get { return _technicalRequrementsTasks; }
            private set
            {
                _technicalRequrementsTasks = value;
                OnPropertyChanged();
            }
        }

        public PriceCalculationsContainer PriceCalculations
        {
            get { return _priceCalculations; }
            private set
            {
                _priceCalculations = value;
                OnPropertyChanged();
            }
        }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();
            _messageService = Container.Resolve<IMessageService>();
            _modelsStore = Container.Resolve<IModelsStore>();

            //при добавлении или удалении айтема, подписываем/отписываем на событие удаления
            ProjectItems.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var projectItem in args.NewItems.Cast<ProjectItem>())
                    {
                        projectItem.LastSalesUnitRemoveEvent += ProjectItemOnLastSalesUnitRemoveEvent;
                    }
                }

                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var projectItem in args.OldItems.Cast<ProjectItem>())
                    {
                        projectItem.LastSalesUnitRemoveEvent -= ProjectItemOnLastSalesUnitRemoveEvent;
                    }
                }
            };


            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectItem != null);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute, () => SelectedProjectItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute, () => SelectedProjectItem != null);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers?.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(() => Offers.RemoveSelectedItem(), () => Offers?.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers?.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers?.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders?.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(() => Tenders.RemoveSelectedItem(), () => Tenders?.SelectedItem != null);

            EditTechnicalRequrementsTaskCommand = new DelegateCommand(EditTechnicalRequrementsTaskCommand_Execute, () => TechnicalRequrementsTasks?.SelectedItem != null);
            EditPriceCalculationCommand = new DelegateCommand(EditPriceCalculationCommand_Execute);

            StructureCostsCommand = new DelegateCommand(StructureCostsCommand_Execute, () => SelectedProjectItem != null);

            SelectProjectsFolderCommand = new DelegateCommand(SelectProjectsFolderCommand_Execute);
            OpenFolderCommand = new DelegateCommand(OpenFolderCommand_Execute, () => SelectedProjectItem != null);

            MakeTceTaskCommand = new DelegateCommand(MakeTceTaskCommand_Execute, () => SelectedProjectItem != null);

            OpenTenderLinkCommand = new DelegateCommand(
                () =>
                {
                    if (!string.IsNullOrWhiteSpace(Tenders.SelectedItem?.Link))
                    {
                        Process.Start(Tenders.SelectedItem.Link);
                    }
                },
                () => !string.IsNullOrWhiteSpace(Tenders?.SelectedItem?.Link));

            #endregion

            #region Subscribe to Events

            //реакция на сохранение юнита
            _eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                //проверяем, можно ли юнит поместить в существующую группу
                ProjectItems.ToList().ForEach(x => x.Check(salesUnit));

                //если не смогли пристроить в существующую группу, создаем новую
                if (!ProjectItems.SelectMany(x => x.SalesUnits).Contains(salesUnit))
                {
                    ProjectItems.Add(new ProjectItem(new[] { salesUnit }, _eventAggregator));
                }
            });

            //подписка на выбор сущностей
            _eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion

            InitNotes();
        }

        protected override void ReloadCommand_Execute()
        {
            _modelsStore.Refresh();
            base.ReloadCommand_Execute();
        }

        protected override void GetData()
        {
            UnitOfWork = _modelsStore.UnitOfWork;

            _tenders = UnitOfWork.Repository<Tender>().GetAll();

            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsRemoved)
                : UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsRemoved && x.Project.Manager.IsAppCurrentUser());

            var items = salesUnits
                .GroupBy(unit => unit, new SalesUnitsComparer())
                .Select(units => new ProjectItem(units, _eventAggregator))
                .ToList();

            _projectItems = items.OrderBy(projectItem => projectItem.DaysToStartProduction).ThenBy(x => x.OrderInTakeDate);

            Offers = Container.Resolve<OffersContainer>();
            Tenders = Container.Resolve<TendersContainer>();
            TechnicalRequrementsTasks = Container.Resolve<TechnicalRequrementsTasksContainer>();
            PriceCalculations = Container.Resolve<PriceCalculationsContainer>();
        }

        protected override void AfterGetData()
        {
            ProjectItem.AllTenders.Clear();
            ProjectItem.AllTenders.AddRange(_tenders);

            ProjectItems.Clear();
            ProjectItems.AddRange(_projectItems);
        }

        private IEnumerable<Tender> _tenders;
        private IEnumerable<ProjectItem> _projectItems;
        private OffersContainer _offers;
        private TendersContainer _tenders1;
        private TechnicalRequrementsTasksContainer _technicalRequrementsTasks;
        private PriceCalculationsContainer _priceCalculations;
        private object _selectedItem;


        //удалить айтем, если он уже опустел
        private void ProjectItemOnLastSalesUnitRemoveEvent(ProjectItem item)
        {
            if (ProjectItems.Contains(item))
                ProjectItems.Remove(item);
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewSpecificationCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)StructureCostsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)MakeTceTaskCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenFolderCommand).RaiseCanExecuteChanged();
            OfferRaiseCanExecuteChanged();
            TenderRaiseCanExecuteChanged();
        }

        private void OfferRaiseCanExecuteChanged()
        {
            ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
        }

        private void TenderRaiseCanExecuteChanged()
        {
            ((DelegateCommand)NewTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenTenderLinkCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
