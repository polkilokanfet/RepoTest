using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using OfferView = HVTApp.Modules.Sales.Views.OfferView;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ViewModelBase
    {
        private ProjectLookup _selectedProjectLookup;
        private bool _shownAllProjects = false;

        public List<ProjectLookup> ProjectLookups { get; } = new List<ProjectLookup>();
        public ObservableCollection<ProjectLookup> ProjectLookupsInView { get; } = new ObservableCollection<ProjectLookup>();

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();

        public ProjectLookup SelectedProjectLookup
        {
            get { return _selectedProjectLookup; }
            set
            {
                if (Equals(_selectedProjectLookup, value)) return;
                _selectedProjectLookup = value;
                Groups.Clear();

                if (value != null)
                {
                    //обновляем данные всех таблиц
                    OfferListViewModel.Load(value.Offers);
                    TenderListViewModel.Load(value.Tenders);
                    LoadGroups(value);
                }
                else
                {
                    OfferListViewModel.Load(new List<Offer>());
                    TenderListViewModel.Load(new List<Tender>());
                    Groups.Clear();
                }

                //проверяем актуальность команд
                ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewTenderCommand).RaiseCanExecuteChanged();

                OnPropertyChanged();
            }
        }

        #region ViewModels

        public OfferLookupListViewModel OfferListViewModel { get; }
        public TenderLookupListViewModel TenderListViewModel { get; }

        #endregion

        #region ICommand

        public ICommand ShowAllProjectsCommand { get; set; }
        public ICommand ShowWorkProjectsCommand { get; set; }

        public ICommand NewProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand RemoveProjectCommand { get; }

        public ICommand NewSpecificationCommand { get; }


        public ICommand NewOfferByProjectCommand { get; }
        public ICommand NewOfferByOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }


        #endregion

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            //контексты
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            TenderListViewModel = container.Resolve<TenderLookupListViewModel>();

            //команды
            ShowAllProjectsCommand = new DelegateCommand(ShowAllProjectsCommand_Execute);
            ShowWorkProjectsCommand = new DelegateCommand(ShowWorkProjectsCommand_Execute);

            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedProjectLookup != null);
            RemoveProjectCommand = new DelegateCommand(RemoveProjectCommand_Execute, () => SelectedProjectLookup != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = OfferListViewModel.PrintOfferCommand;
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedProjectLookup != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => SelectedProjectLookup != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => TenderListViewModel.EditItemCommand.CanExecute(null));
            RemoveTenderCommand = TenderListViewModel.RemoveItemCommand;

            //реакция на смену выбранного ТКП
            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            };

            //реакция на смену конкурса
            TenderListViewModel.SelectedLookupChanged += tender =>
            {
                ((DelegateCommand)EditTenderCommand).RaiseCanExecuteChanged();
            };

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
        }

        public void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //достаем все проектные юниты
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id);

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
                var projectLookup = new ProjectLookup(project, salesUnits.Where(x => Equals(x.Project.Id, project.Id)).ToList(),
                                                               tenders.Where(x => Equals(x.Project.Id, project.Id)).ToList(),
                                                               offers.Where(x => Equals(x.Project.Id, project.Id)).ToList());
                foreach (var offer in projectLookup.Offers)
                {
                    offer.OfferUnits.AddRange(offerUnits.Where(x => x.Offer.Id == offer.Id).Select(x => new OfferUnitLookup(x)));
                }
                projectsLookups.Add(projectLookup);
            }
            ProjectLookups.AddRange(projectsLookups.OrderBy(x => x.RealizationDate));

            //показываем все рабочие проекты
            ShowWorkProjectsCommand_Execute();
        }

        private void LoadGroups(ProjectLookup project)
        {
            var units = project.SalesUnits.Select(x => x.Entity);
            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsGroup(x));
            Groups.Clear();
            Groups.AddRange(groups);
        }

        #region Commands

        private async void RemoveProjectCommand_Execute()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var messageService = Container.Resolve<IMessageService>();
            var eventAggregator = Container.Resolve<IEventAggregator>();

            var dr = messageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedProjectLookup.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;

            var entity = await unitOfWork.Repository<Project>().GetByIdAsync(SelectedProjectLookup.Id);
            if (entity == null) return;

            try
            {
                unitOfWork.Repository<Project>().Delete(entity);
                await unitOfWork.SaveChangesAsync();
                ProjectLookups.Remove(SelectedProjectLookup);
                ProjectLookupsInView.Remove(SelectedProjectLookup);
                eventAggregator.GetEvent<AfterRemoveProjectEvent>().Publish(entity);
            }
            catch (DbUpdateException e)
            {
                Exception ex = e;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                messageService.ShowOkMessageDialog("DbUpdateException", ex.Message);
            }
        }

        private void ShowWorkProjectsCommand_Execute()
        {
            _shownAllProjects = false;

            ProjectLookupsInView.Clear();
            var workProjects = ProjectLookups.Where(x => x.Entity.InWork && x.SalesUnits.Any(u => !u.IsDone && !u.IsLoosen)).ToList();
            ProjectLookupsInView.AddRange(workProjects);

            //выбор проекта
            if (SelectedProjectLookup == null && ProjectLookupsInView.Any())
                SelectedProjectLookup = ProjectLookupsInView.First();
        }

        private void ShowAllProjectsCommand_Execute()
        {
            ProjectLookupsInView.Clear();
            ProjectLookupsInView.AddRange(ProjectLookups);
            _shownAllProjects = true;
        }

        private void EditTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, TenderListViewModel.SelectedItem);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);            
        }

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, SelectedProjectLookup.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", SelectedProjectLookup.Entity } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { { "prj", SelectedProjectLookup.Entity } });
        }

        private void NewProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        #region OfferCommands

        /// <summary>
        /// ТКП по существующему ТКП
        /// </summary>
        private void NewOfferByOfferCommand_Execute()
        {
            var prms = new NavigationParameters {{"offer", OfferListViewModel.SelectedItem}};
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// ТКП по проекту
        /// </summary>
        private void NewOfferByProjectCommand_Execute()
        {
            var prms = new NavigationParameters {{"project", SelectedProjectLookup.Entity}};
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// Изменить ТКП
        /// </summary>
        private void EditOfferCommand_Execute()
        {
            var prms = new NavigationParameters {{"offer", OfferListViewModel.SelectedItem}, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }


        #endregion

        #endregion

        #region AfterSaveEvents

        private void AfterSaveProjectEventExecute(Project project)
        {
            //если необходимо обновить существующий проект
            if (ProjectLookups.Select(x => x.Id).Contains(project.Id))
            {
                var projectLookup = ProjectLookups.Single(x => x.Id == project.Id);
                projectLookup.Refresh(project);

                if (_shownAllProjects || projectLookup.InWork)
                {
                    LoadGroups(projectLookup);
                }
                else
                {
                    //удяляем нерабочие проекты
                    if (ProjectLookupsInView.Contains(projectLookup))
                    {
                        ProjectLookupsInView.Remove(projectLookup);
                    }
                }
            }
            else
            {
                var lookup = new ProjectLookup(project);
                ProjectLookups.Add(lookup);

                if(_shownAllProjects || lookup.InWork)
                    ProjectLookupsInView.Add(lookup);
            }
        }

        private void AfterSaveTenderEventExecute(Tender tender)
        {
            var tenders = ProjectLookups.SelectMany(x => x.Tenders).ToList();
            //если необходимо обновить существующий тендер
            if (tenders.Select(x => x.Id).Contains(tender.Id))
            {
                tenders.SingleOrDefault(x => x.Id == tender.Id)?.Refresh(tender);
            }
            //если необходимо добавть созданный тендер
            else
            {
                ProjectLookups.SingleOrDefault(x => x.Id == tender.Project.Id)?.Tenders.Add(new TenderLookup(tender));                
            }

            //обновляем проект, содержащий тендер
            ProjectLookups.SingleOrDefault(x => x.Tenders.Select(t => t.Id).Contains(tender.Id))?.Refresh();
        }

        private async void AfterSaveOfferEventExecute(Offer offer)
        {
            var offers = ProjectLookups.SelectMany(x => x.Offers).ToList();

            //если необходимо обновить существующее ТКП
            if (offers.Select(x => x.Id).Contains(offer.Id))
            {
                offers.SingleOrDefault(x => x.Id == offer.Id)?.Refresh(offer);
                return;
            }

            var units = (await UnitOfWork.Repository<OfferUnit>().GetAllAsNoTrackingAsync()).Where(x => x.Offer.Id == offer.Id);
            //если необходимо добавить созданное ТКП
            var lookupNew = new OfferLookup(offer, units);
            ProjectLookups.SingleOrDefault(x => x.Id == offer.Project.Id)?.Offers.Add(lookupNew);
        }

        private void AfterSaveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            //целевой проект
            var project = ProjectLookups.SingleOrDefault(x => x.Id == salesUnit.Project.Id);

            //костыль - нужно добавить предварительно проект
            if (project == null)
            {
                AfterSaveProjectEventExecute(salesUnit.Project);
                project = ProjectLookups.SingleOrDefault(x => x.Id == salesUnit.Project.Id);
            }

            //обновляем или добавляем
            if (project.SalesUnits.Select(x => x.Id).Contains(salesUnit.Id))
            {
                project.SalesUnits.Single(x => x.Id == salesUnit.Id).Refresh(salesUnit);
            }
            else
            {
                project.SalesUnits.Add(new SalesUnitLookup(salesUnit));
            }

            //обновляем целевой проект
            project.Refresh();
        }

        private void AfterSaveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            //целевое ТКП
            var offer = ProjectLookups.SelectMany(x => x.Offers).SingleOrDefault(x => x.Id == offerUnit.Offer.Id);
            if (offer == null) return;

            //обновляем или добавляем
            if (offer.OfferUnits.Select(x => x.Id).Contains(offerUnit.Id))
            {
                offer.OfferUnits.Single(x => x.Id == offerUnit.Id).Refresh(offerUnit);
            }
            else
            {
                offer.OfferUnits.Add(new OfferUnitLookup(offerUnit));
            }

            //обновляем целевое ТКП
            offer.Refresh();
        }



        #endregion

        #region AfterRemoveEvent

        private void AfterRemoveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            var offer = ProjectLookups.SelectMany(x => x.Offers).SingleOrDefault(x => x.Id == offerUnit.Offer.Id);
            if (offer == null) return;
            var lookup = offer.OfferUnits.Single(x => x.Id == offerUnit.Id);
            offer.OfferUnits.Remove(lookup);
            offer.Refresh();
        }

        private void AfterRemoveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            var project = ProjectLookups.SingleOrDefault(x => x.Id == salesUnit.Project.Id);
            if (project == null) return;
            var lookup = project.SalesUnits.Single(x => x.Id == salesUnit.Id);
            project.SalesUnits.Remove(lookup);
            project.Refresh();
        }

        private void AfterRemoveProjectEventExecute(Project project)
        {
        }

        private void AfterRemoveTenderEventExecute(Tender tender)
        {
            var project = ProjectLookups.SingleOrDefault(x => x.Tenders.Select(t => t.Id).Contains(tender.Id));
            if(project == null) return;
            var lookup = project.Tenders.Single(x => x.Id == tender.Id);
            project.Tenders.Remove(lookup);
            lookup.Refresh();
        }

        private void AfterRemoveOfferEventExecute(Offer offer)
        {
            var project = ProjectLookups.SingleOrDefault(x => x.Offers.Select(t => t.Id).Contains(offer.Id));
            if(project == null) return;
            var lookup = project.Offers.Single(x => x.Id == offer.Id);
            project.Offers.Remove(lookup);
        }

        #endregion

    }
}
