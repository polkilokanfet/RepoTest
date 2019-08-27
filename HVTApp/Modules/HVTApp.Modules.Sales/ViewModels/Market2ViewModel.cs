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
        private OfferLookup _selectedOffer;
        private TenderLookup _selectedTender;

        public Projects Projects { get; } = new Projects();

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        public Offers Offers { get; } = new Offers();

        public Tenders Tenders { get; } = new Tenders();

        public ProjectLookup SelectedProjectLookup
        {
            get { return _selectedProjectLookup; }
            set
            {
                if (Equals(_selectedProjectLookup, value))
                    return;

                _selectedProjectLookup = value;

                LoadGroups(_selectedProjectLookup);
                Offers.ShowByProject(_selectedProjectLookup);
                Tenders.ShowByProject(_selectedProjectLookup);

                ((DelegateCommand) RemoveProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) EditProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) NewOfferByProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) NewOfferByOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) NewTenderCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) EditTenderCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) RemoveTenderCommand).RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        public OfferLookup SelectedOffer
        {
            get { return _selectedOffer; }
            set
            {
                if (Equals(_selectedOffer, value)) return;
                _selectedOffer = value;
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            }
        }

        public TenderLookup SelectedTender
        {
            get { return _selectedTender; }
            set
            {
                if (Equals(_selectedTender, value)) return;
                _selectedTender = value;
                ((DelegateCommand)EditTenderCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveTenderCommand).RaiseCanExecuteChanged();
            }
        }

        public bool ShownAllProjects
        {
            get { return Projects.ShownAllProjects; }
            set
            {
                if (value)
                {
                    Projects.ShowAll();
                    return;
                }

                Projects.ShowWork();
            }
        }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectLookup != null);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute, () => SelectedProjectLookup != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => SelectedOffer != null);
            RemoveOfferCommand = new DelegateCommand(RemoveOfferCommand_Execute, () => SelectedOffer != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => SelectedOffer != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectLookup != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => SelectedOffer != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectLookup != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => SelectedTender != null);
            RemoveTenderCommand = new DelegateCommand(RemoveTenderCommand_Execute, () => SelectedTender != null);

            #region Subscribe to Events

            //подписка на создание, изменение и удаление сущностей
            var eventAggregator = container.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<AfterSaveOfferEvent>().Subscribe(AfterSaveOfferEventExecute);
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(AfterSaveTenderEventExecute);
            eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(AfterSaveProjectEventExecute);
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(AfterSaveSalesUnitEventExecute);
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(AfterSaveOfferUnitEventExecute);

            eventAggregator.GetEvent<AfterRemoveOfferEvent>().Subscribe(AfterRemoveOfferEventExecute);
            eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(AfterRemoveTenderEventExecute);
            eventAggregator.GetEvent<AfterRemoveProjectEvent>().Subscribe(AfterRemoveProjectEventExecute);
            eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(AfterRemoveSalesUnitEventExecute);
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(AfterRemoveOfferUnitEventExecute);

            #endregion
        }

        public void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //достаем все проектные юниты
            var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetUsersSalesUnits().ToList();

            //формируем список проектов
            var projects = salesUnits.Select(x => x.Project).Distinct().ToList();
            //чтобы не дергать по выбору
            var notes = UnitOfWork.Repository<Note>().Find(x => projects.SelectMany(p => p.Notes).Contains(x));

            //формируем список конкурсов
            var tenders = UnitOfWork.Repository<Tender>().Find(x => projects.Contains(x.Project)).ToList();
            var companies = tenders.SelectMany(x => x.Participants).ToList();

            //формируем список ТКП
            var offerUnits = UnitOfWork.Repository<OfferUnit>().Find(x => projects.Contains(x.Offer.Project));
            var offers = offerUnits.Select(x => x.Offer).Distinct().ToList();
            var employies = offers.Select(x => x.RecipientEmployee).ToList();

            var projectsLookups = new List<ProjectLookup>();
            foreach (var project in projects)
            {
                var projectLookup = new ProjectLookup(project, salesUnits.Where(x => Equals(x.Project.Id, project.Id)),
                                                               tenders.Where(x => Equals(x.Project.Id, project.Id)),
                                                               offers.Where(x => Equals(x.Project.Id, project.Id)));
                foreach (var offer in projectLookup.Offers)
                {
                    offer.OfferUnits.AddRange(offerUnits.Where(x => x.Offer.Id == offer.Id).Select(x => new OfferUnitLookup(x)));
                }
                Projects.Add(projectLookup);
            }

            //показываем все рабочие проекты
            Projects.ShowWork();
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

        private void ShowWorkProjects()
        {
            //выбор проекта
            //if (SelectedProjectLookup == null && ProjectLookupsInView.Any())
            //    SelectedProjectLookup = ProjectLookupsInView.First();
        }
    }
}
