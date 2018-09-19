using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using OfferView = HVTApp.Modules.Sales.Views.OfferView;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        private IUnitOfWork _unitOfWork;
        private List<SalesUnit> _salesUnits;
        private List<Tender> _tenders;

        #region ViewModels

        public ProjectLookupListViewModel ProjectListViewModel { get; set; }
        public OfferLookupListViewModel OfferListViewModel { get; }
        public TenderLookupListViewModel TenderListViewModel { get; }
        public UnitLookupListViewModel UnitListViewModel { get; }
        public NoteLookupListViewModel NoteListViewModel { get; }

        #endregion

        public Project SelectedProject { get; set; }

        #region ICommand

        public ICommand NewProjectCommand { get; }
        public ICommand EditProjectCommand { get; }

        public ICommand NewSpecificationCommand { get; }


        public ICommand NewOfferByProjectCommand { get; }
        public ICommand NewOfferByOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }


        #endregion

        #region Ctor

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            //контексты
            ProjectListViewModel = container.Resolve<ProjectLookupListViewModel>();
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            TenderListViewModel = container.Resolve<TenderLookupListViewModel>();
            UnitListViewModel = container.Resolve<UnitLookupListViewModel>();
            NoteListViewModel = container.Resolve<NoteLookupListViewModel>();

            //привязываем команды к соответствующим моделям
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => SelectedItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = OfferListViewModel.PrintOfferCommand;
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => SelectedItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => OfferListViewModel?.SelectedItem != null);

            NewTenderCommand = TenderListViewModel.NewItemCommand;
            EditTenderCommand = TenderListViewModel.EditItemCommand;
            RemoveTenderCommand = TenderListViewModel.RemoveItemCommand;

            NewNoteCommand = NoteListViewModel.NewItemCommand;
            EditNoteCommand = NoteListViewModel.EditItemCommand;
            RemoveNoteCommand = NoteListViewModel.RemoveItemCommand;

            //реакция на смену выбранного проекта
            SelectedLookupChanged += project =>
            {
                UnitListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                TenderListViewModel.Load(project.Tenders);
                NoteListViewModel.Load(project.Notes);

                ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitListViewModel.Load(offer.OfferUnits);
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            };
        }

        public async Task Load()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            //достаем все проектные актуальные юниты
            _salesUnits = await _unitOfWork.Repository<SalesUnit>().GetAllAsync();
            _salesUnits = _salesUnits.Where(x => !x.IsDone && 
                                                 !x.IsLoosen && 
                                                 x.Project.HighProbability &&
                                                 x.Project.Manager.Id == CommonOptions.User.Id).ToList();

            //формируем список проектов
            var projects = _salesUnits.Select(x => x.Project).Distinct();

            //формируем список конкурсов
            _tenders = (await _unitOfWork.Repository<Tender>().GetAllAsync()).Where(x => projects.Contains(x.Project)).ToList();

            //подтягиваем проекты во вью
            var projectsLookups = new List<ProjectLookup>();
            foreach (var project in projects)
            {
                projectsLookups.Add(new ProjectLookup(project, 
                                                      _salesUnits.Where(x => Equals(x.Project.Id, project.Id)),
                                                      _tenders.Where(x => Equals(x.Project.Id, project.Id))));
            }
            ProjectListViewModel.Load(projectsLookups);
        }

        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", SelectedItem } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { {"prj", SelectedItem} });
        }

        private void NewProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        #endregion

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
            var prms = new NavigationParameters {{"project", SelectedItem}};
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

    }
}
