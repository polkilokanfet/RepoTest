using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Containers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel : ViewModelBase
    {
        private ProjectItem _selectedProjectItem;
        private readonly IEventAggregator _eventAggregator;
        private List<Tender> _tenders; //не заменять на локальную

        public ObservableCollection<ProjectItem> ProjectItems { get; }

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

                OnPropertyChanged(nameof(Notes));
                ((DelegateCommand)AddNoteCommand).RaiseCanExecuteChanged();
                SelectedNote = null;
            }
        }

        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }

        public ICommand ExpandCommand { get; }
        public ICommand CollapseCommand { get; }

        public event Action<bool> ExpandCollapseEvent;

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();

            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.Admin 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => true) 
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
            _tenders = UnitOfWork.Repository<Tender>().Find(x => true);

            ProjectItems = new ObservableCollection<ProjectItem>(GetProjectItems(salesUnits));

            //событие удаления юнита
            _eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                var projectItem = ProjectItems.SingleOrDefault(x => x.ContainsById(salesUnit));

                if(projectItem?.RemoveSalesUnit(salesUnit) == ProjectItemState.HasNoSalesUnit)
                    ProjectItems.Remove(projectItem);
            });


            _eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                //все айтемы проекта
                var itemsOfProject = ProjectItems.Where(x => x.Project.Id == salesUnit.Project.Id).ToList();
                
                //айтем, в котором содержится юнит
                var itemContainsSalesUnit = itemsOfProject.SingleOrDefault(x => x.ContainsById(salesUnit));
                
                //айтемы, в которые подойдет юнит
                var targetItems = itemsOfProject.Where(x => x.Fits(salesUnit)).ToList();

                if (targetItems.Any())
                {
                    //если подходит более, чем в 2 айтема, то это дичь какая-то
                    if(targetItems.Count > 2) throw new NotImplementedException();

                    //если подходит сразу в 2 айтема
                    if (targetItems.Count == 2)
                    {
                        targetItems.Remove(itemContainsSalesUnit);
                        //то тот айтем, что содержал юнит ранее - лишний
                        ProjectItems.Remove(itemContainsSalesUnit);
                    }
                    targetItems.First().AddSalesUnit(salesUnit);
                }
                else
                {
                    GetProjectItems(new []{salesUnit}).ForEach(x => ProjectItems.Add(x));

                    if (itemContainsSalesUnit != null && itemContainsSalesUnit.RemoveSalesUnit(salesUnit) == ProjectItemState.HasNoSalesUnit)
                    {
                        ProjectItems.Remove(itemContainsSalesUnit);
                    }
                }
            });

            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();

            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectItem != null);
            RemoveProjectCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удалить проект.", "Вы уверены, что хотите удалить проект?");
                    if (dr != MessageDialogResult.Yes) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var units = unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == SelectedProjectItem.Project.Id);
                    unitOfWork.Repository<SalesUnit>().DeleteRange(units);
                    unitOfWork.SaveChanges();
                    var remove = ProjectItems.Where(x => x.Project.Id == SelectedProjectItem.Project.Id).ToList();
                    remove.ForEach(x => ProjectItems.Remove(x));
                }, 
                () => SelectedProjectItem != null);

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

            SaveGridCustomisationsCommand = new DelegateCommand(() => { SaveGridCustomisationsEvent?.Invoke(); });

            #endregion

            #region Subscribe to Events

            //подписка на выбор сущностей
            _eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion

            _eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
            {
                _tenders.RemoveById(tender);
                ProjectItems.ForEach(x => x.Tenders.RemoveIfContainsById(tender));
            });

            _eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
            {
                _tenders.ReAddById(tender);
                foreach (var projectItem in ProjectItems)
                {
                    if (projectItem.Project.Id == tender.Project.Id)
                        projectItem.Tenders.ReAddById(tender);                    
                }
            });

            //развернуть
            ExpandCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(true); });
            //свернуть
            CollapseCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(false); });

            InitNotes();
        }

        private IEnumerable<ProjectItem> GetProjectItems(IEnumerable<SalesUnit> salesUnits)
        {
            return salesUnits
                .GroupBy(x => x, new SalesUnitsComparer())
                .OrderBy(x => x.Key.OrderInTakeDate)
                .Select(x => new ProjectItem(x, _tenders.Where(t => x.Key.Project.Id == t.Project.Id), _eventAggregator));
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewSpecificationCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)StructureCostsCommand).RaiseCanExecuteChanged();
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

    public class SalesUnitsComparer : IEqualityComparer<SalesUnit>
    {
        public bool Equals(SalesUnit x, SalesUnit y)
        {
            if (!Equals(x.RealizationDateCalculated, y.RealizationDateCalculated)) return false;
            if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.Project.Id, y.Project.Id)) return false;
            if (!Equals(x.IsDone, y.IsDone)) return false;
            if (!Equals(x.IsLoosen, y.IsLoosen)) return false;

            return true;
        }

        public int GetHashCode(SalesUnit obj)
        {
            return 0;
        }
    }
}
