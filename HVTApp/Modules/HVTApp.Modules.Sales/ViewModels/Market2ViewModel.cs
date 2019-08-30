using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel : ViewModelBase
    {
        private ProjectLookup _selectedProjectLookup;

        private readonly IEventAggregator _eventAggregator;

        public ProjectsContainer Projects { get; }
        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        public ProjectLookup SelectedProjectLookup
        {
            get { return _selectedProjectLookup; }
            set
            {
                if (Equals(_selectedProjectLookup, value))
                    return;

                _selectedProjectLookup = value;

                LoadGroups(_selectedProjectLookup);

                ProjectRaiseCanExecuteChanged();

                OnPropertyChanged();
                _eventAggregator.GetEvent<SelectedProjectChangedEvent>().Publish(SelectedProjectLookup.Entity);
            }
        }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {

            Projects = container.Resolve<ProjectsContainer>();
            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();

            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectLookup != null);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute, () => SelectedProjectLookup != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(RemoveOfferCommand_Execute, () => Offers.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectLookup != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectLookup != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(RemoveTenderCommand_Execute, () => Tenders.SelectedItem != null);

            #region Subscribe to Events

            //подписка на создание, изменение и удаление сущностей
            _eventAggregator = container.Resolve<IEventAggregator>();

            //_eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(AfterSaveProjectEventExecute);
            _eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(AfterSaveSalesUnitEventExecute);

            //_eventAggregator.GetEvent<AfterRemoveProjectEvent>().Subscribe(AfterRemoveProjectEventExecute);
            _eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(AfterRemoveSalesUnitEventExecute);


            _eventAggregator.GetEvent<AfterSelectOfferEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            _eventAggregator.GetEvent<AfterSelectTenderEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion

            //UnitOfWork = Container.Resolve<IUnitOfWork>();

            ////достаем все проектные юниты
            //var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnits().ToList();

            ////формируем список проектов
            //var projects = salesUnits.Select(x => x.Project).Distinct().ToList();
            ////чтобы не дергать по выбору
            //var notes = UnitOfWork.Repository<Note>().Find(x => projects.SelectMany(p => p.Notes).Contains(x));

            ////формируем список конкурсов
            //var tenders = UnitOfWork.Repository<Tender>().Find(x => projects.Contains(x.Project)).ToList();
            //var companies = tenders.SelectMany(x => x.Participants).ToList();

            ////формируем список ТКП
            //var offerUnits = UnitOfWork.Repository<OfferUnit>().Find(x => projects.Contains(x.Offer.Project));
            //var offers = offerUnits.Select(x => x.Offer).Distinct().ToList();
            //var employies = offers.Select(x => x.RecipientEmployee).ToList();

            //foreach (var project in projects)
            //{
            //    var projectLookup = new ProjectLookup(project, salesUnits.Where(x => Equals(x.Project.Id, project.Id)),
            //                                                   tenders.Where(x => Equals(x.Project.Id, project.Id)),
            //                                                   offers.Where(x => Equals(x.Project.Id, project.Id)));
            //    foreach (var offer in projectLookup.Offers)
            //    {
            //        offer.OfferUnits.AddRange(offerUnits.Where(x => x.Offer.Id == offer.Id).Select(x => new OfferUnitLookup(x)));
            //    }
            //    Projects.Add(projectLookup);
            //}
        }

        public void Load()
        {
        }

        private void LoadGroups(ProjectLookup project)
        {
            Groups.Clear();
            if (project == null)
                return;

            //все юниты проекта
            var units = project.SalesUnits.Select(x => x.Entity);
            //группируем их
            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsGroup(x));
            //обновляем вид
            Groups.AddRange(groups.OrderByDescending(x => x.Total));
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
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
