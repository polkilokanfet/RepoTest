using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel : ViewModelBase
    {
        private ProjectItem _selectedProjectItem;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<ProjectItem> ProjectItems { get; }

        public ProjectItem SelectedProjectItem
        {
            get { return _selectedProjectItem; }
            set
            {
                _selectedProjectItem = value;
                ProjectRaiseCanExecuteChanged();
                if(value != null)
                    _eventAggregator.GetEvent<SelectedProjectChangedEvent>().Publish(value.Project);
            }
        }

        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();

            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
            var salesUnitsGroups = salesUnits.GroupBy(x => x, new SalesUnitsComparer()).OrderBy(x => x.Key.OrderInTakeDate);
            ProjectItems = new ObservableCollection<ProjectItem>(salesUnitsGroups.Select(x => new ProjectItem(x)));

            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();

            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectItem != null);
            RemoveProjectCommand = new DelegateCommand(() => { }, () => SelectedProjectItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute, () => SelectedProjectItem != null);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(async () => await Offers.RemoveSelectedItemTask(), () => Offers.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(async () => await Tenders.RemoveSelectedItemTask(), () => Tenders.SelectedItem != null);

            StructureCostsCommand = new DelegateCommand(StructureCostsCommand_Execute, () => SelectedProjectItem != null);

            #endregion

            #region Subscribe to Events

            //подписка на выбор сущностей
            _eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion

            //реакция на удаление юнита
            _eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                foreach (var projectItem in ProjectItems)
                {
                    if (projectItem.SalesUnits.ContainsById(salesUnit))
                    {
                        if (projectItem.SalesUnits.Count == 1)
                        {
                            ProjectItems.Remove(projectItem);
                        }
                        else
                        {
                            projectItem.SalesUnits.RemoveById(salesUnit);
                        }
                        return;
                    }
                }

            });

            _eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                //редактируем юнит в существующей группе
                foreach (var projectItem in ProjectItems)
                {
                    if (projectItem.SalesUnits.ContainsById(salesUnit))
                    {
                        if (projectItem.SalesUnits.Count == 1 ||
                            new SalesUnitsComparer().Equals(salesUnit, projectItem.SalesUnits.First()))
                        {
                            projectItem.SalesUnits.ReAddById(salesUnit);
                            return;
                        }

                        projectItem.SalesUnits.RemoveById(salesUnit);
                    }
                }

                //добавляем юнит в подходящий юнит
                foreach (var projectItem in ProjectItems)
                {
                    if (new SalesUnitsComparer().Equals(salesUnit, projectItem.SalesUnits.First()))
                    {
                        projectItem.SalesUnits.Add(salesUnit);
                        return;
                    }
                }

                //создаем новую группу для юнита
                ProjectItems.Add(new ProjectItem(new List<SalesUnit>() { salesUnit }));
            });
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
