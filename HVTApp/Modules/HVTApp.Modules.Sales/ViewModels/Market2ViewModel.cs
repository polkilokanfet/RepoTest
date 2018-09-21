using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
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

        #region ViewModels

        public ProjectLookupListViewModel ProjectListViewModel { get; set; }
        public OfferLookupListViewModel OfferListViewModel { get; }
        public TenderLookupListViewModel TenderListViewModel { get; }

        #endregion

        public ObservableCollection<SalesUnitsGroup> Groups { get; } = new ObservableCollection<SalesUnitsGroup>();


        #region ICommand

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
            ProjectListViewModel = container.Resolve<ProjectLookupListViewModel>();
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            TenderListViewModel = container.Resolve<TenderLookupListViewModel>();

            //привязываем команды к соответствующим моделям
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => ProjectListViewModel.SelectedItem != null);
            RemoveProjectCommand = new DelegateCommand(() => ProjectListViewModel.RemoveItemCommand.Execute(null), () => ProjectListViewModel.SelectedItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = OfferListViewModel.PrintOfferCommand;
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => ProjectListViewModel.SelectedItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => ProjectListViewModel.SelectedItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => TenderListViewModel.EditItemCommand.CanExecute(null));
            RemoveTenderCommand = TenderListViewModel.RemoveItemCommand;

            //реакция на смену выбранного проекта
            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                Groups.Clear();

                if (project != null)
                {
                    //обновляем данные всех таблиц
                    OfferListViewModel.Load(project.Offers);
                    TenderListViewModel.Load(project.Tenders);

                    var units = project.SalesUnits.Select(x => x.Entity);
                    var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer())
                                      .OrderByDescending(x => x.Key.Cost)
                                      .Select(x => new SalesUnitsGroup(x));
                    Groups.AddRange(groups);
                }

                //проверяем актуальность команд
                ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewTenderCommand).RaiseCanExecuteChanged();
            };

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

        private void EditTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, TenderListViewModel.SelectedItem);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);            
        }

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, ProjectListViewModel.SelectedItem);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        public async Task LoadAsync()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //достаем все проектные актуальные юниты
            var salesUnits = await UnitOfWork.Repository<SalesUnit>().GetAllAsync();
            salesUnits = salesUnits.Where(x => !x.IsDone && 
                                               !x.IsLoosen && 
                                               x.Project.HighProbability &&
                                               x.Project.Manager.Id == CommonOptions.User.Id).ToList();

            //формируем список проектов
            var projects = salesUnits.Select(x => x.Project).Distinct();

            //формируем список конкурсов
            var tenders = (await UnitOfWork.Repository<Tender>().GetAllAsync()).Where(x => projects.Contains(x.Project)).ToList();

            //формируем список ТКП
            var offerUnits = (await UnitOfWork.Repository<OfferUnit>().GetAllAsync()).Where(x => projects.Contains(x.Offer.Project)).ToList();
            var offers = offerUnits.Select(x => x.Offer).Distinct().ToList();

            //подтягиваем проекты во вью
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
                projectsLookups.Add(projectLookup);
            }
            ProjectListViewModel.Load(projectsLookups);
        }

        #region Commands

        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", ProjectListViewModel.SelectedItem } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { {"prj", ProjectListViewModel.SelectedItem} });
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
            var prms = new NavigationParameters {{"project", ProjectListViewModel.SelectedItem}};
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
            if (ProjectListViewModel.Lookups.Select(x => x.Id).Contains(project.Id))
            {
                var lookup = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Id == project.Id);
                lookup?.Refresh(project);
            }
        }

        private void AfterSaveTenderEventExecute(Tender tender)
        {
            var tenders = ProjectListViewModel.Lookups.SelectMany(x => x.Tenders).ToList();
            //если необходимо обновить существующий тендер
            if (tenders.Select(x => x.Id).Contains(tender.Id))
            {
                tenders.SingleOrDefault(x => x.Id == tender.Id)?.Refresh(tender);
            }
            else
            {
                //если необходимо добавть созданный тендер
                var lookupNew = new TenderLookup(tender);
                ProjectListViewModel.Lookups.SingleOrDefault(x => x.Id == tender.Project.Id)?.Tenders.Add(lookupNew);                
            }

            //обновляем проект, содержащий тендер
            ProjectListViewModel.Lookups.SingleOrDefault(x => x.Tenders.Select(t => t.Id).Contains(tender.Id))?.Refresh();
        }

        private void AfterSaveOfferEventExecute(Offer offer)
        {
            var offers = ProjectListViewModel.Lookups.SelectMany(x => x.Offers).ToList();

            //если необходимо обновить существующее ТКП
            if (offers.Select(x => x.Id).Contains(offer.Id))
            {
                offers.SingleOrDefault(x => x.Id == offer.Id)?.Refresh(offer);
                return;
            }

            //если необходимо добавить созданное ТКП
            var lookupNew = new OfferLookup(offer);
            ProjectListViewModel.Lookups.SingleOrDefault(x => x.Id == offer.Project.Id)?.Offers.Add(lookupNew);
        }

        private void AfterSaveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            //целевой проект
            var project = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Id == salesUnit.Project.Id);
            if (project == null) return;

            //обновляем или добавляем
            if (project.SalesUnits.Select(x => x.Id).Contains(salesUnit.Id))
            {
                project.SalesUnits.Single(x => x.Id == salesUnit.Id).Refresh(salesUnit);
            }
            else
            {
                project.SalesUnits.Add(new SalesUnitLookup(salesUnit));
            }
            //обновляем целевой поект
            project.Refresh();
        }

        private void AfterSaveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            //целевое ТКП
            var offer = ProjectListViewModel.Lookups.SelectMany(x => x.Offers).SingleOrDefault(x => x.Id == offerUnit.Offer.Id);
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
            var offer = ProjectListViewModel.Lookups.SelectMany(x => x.Offers).SingleOrDefault(x => x.Id == offerUnit.Offer.Id);
            if (offer == null) return;
            var lookup = offer.OfferUnits.Single(x => x.Id == offerUnit.Id);
            offer.OfferUnits.Remove(lookup);
            offer.Refresh();
        }

        private void AfterRemoveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            var project = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Id == salesUnit.Project.Id);
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
            var project = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Tenders.Select(t => t.Id).Contains(tender.Id));
            if(project == null) return;
            var lookup = project.Tenders.Single(x => x.Id == tender.Id);
            project.Tenders.Remove(lookup);
            lookup.Refresh();
        }

        private void AfterRemoveOfferEventExecute(Offer offer)
        {
            var project = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Offers.Select(t => t.Id).Contains(offer.Id));
            if(project == null) return;
            var lookup = project.Offers.Single(x => x.Id == offer.Id);
            project.Offers.Remove(lookup);
        }

        #endregion

    }
}
