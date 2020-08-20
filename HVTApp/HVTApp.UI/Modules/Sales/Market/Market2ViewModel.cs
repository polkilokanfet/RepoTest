using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
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

        public ObservableCollection<ProjectItem> ProjectItems { get; } = new ObservableCollection<ProjectItem>();

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

                SelectedProjectItemChanged?.Invoke(SelectedProjectItem);
            }
        }

        public event Action<ProjectItem> SelectedProjectItemChanged;

        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();

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

            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();

            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectItem != null);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute, () => SelectedProjectItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute, () => SelectedProjectItem != null);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(() => Offers.RemoveSelectedItem(), () => Offers.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(() => Tenders.RemoveSelectedItem(), () => Tenders.SelectedItem != null);

            StructureCostsCommand = new DelegateCommand(StructureCostsCommand_Execute, () => SelectedProjectItem != null);

            SelectProjectsFolderCommand = new DelegateCommand(SelectProjectsFolderCommand_Execute);
            OpenFolderCommand = new DelegateCommand(OpenFolderCommand_Execute, () => SelectedProjectItem != null);

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

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            _tenders = UnitOfWork.Repository<Tender>().GetAll();

            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? UnitOfWork.Repository<SalesUnit>().GetAll()
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());

            var items = salesUnits
                .GroupBy(x => x, new SalesUnitsComparer())
                .Select(x => new ProjectItem(x, _eventAggregator))
                .ToList();

            _projectItems = items.OrderBy(x => x.DaysToStartProduction).ThenBy(x => x.OrderInTakeDate);
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
        }

        #endregion
    }
}
