using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public abstract class TasksViewModel<TTasksWrapper, TTaskViewModel> : ViewModelBase, IPriceEngineeringTasksViewModel
        where TTasksWrapper : TasksWrapper<TTaskViewModel>
        where TTaskViewModel : TaskViewModel
    {
        private TTasksWrapper _tasksWrapper;
        private PriceEngineeringTasksFileTechnicalRequirementsWrapper _selectedFileTechnicalRequirements;
        private bool _isNew = false;

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public virtual bool AllowEditProps => false;

        public bool IsNew
        {
            get => _isNew;
            protected set => this.SetProperty(ref _isNew, value);
        }

        public TTasksWrapper TasksWrapper
        {
            get => _tasksWrapper;
            protected set
            {
                var originValue = _tasksWrapper;
                if (this.SetProperty(ref _tasksWrapper, value))
                {
                    if (originValue != null)
                        originValue.PropertyChanged -= CheckCommands;

                    if (_tasksWrapper != null)
                        _tasksWrapper.PropertyChanged += CheckCommands;

                    RaisePropertyChanged(nameof(AllowEditProps));
                    this.PriceEngineeringTasksWrapperChanged?.Invoke(originValue, value);
                }
            }
        }

        /// <summary>
        /// Проверка всех команд модели на возможность исполнения
        /// </summary>
        private void CheckCommands(object sender, PropertyChangedEventArgs e)
        {
            this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => typeof(ICommandRaiseCanExecuteChanged).IsAssignableFrom(x.PropertyType))
                .Select(x => x.GetValue(this))
                .Cast<ICommandRaiseCanExecuteChanged>()
                .ForEach(x => x.RaiseCanExecuteChanged());
        }

        public PriceEngineeringTasksFileTechnicalRequirementsWrapper SelectedFileTechnicalRequirements
        {
            get => _selectedFileTechnicalRequirements;
            set
            {
                if (this.SetProperty(ref _selectedFileTechnicalRequirements, value))
                {
                    this.SelectedFileTechnicalRequirementsChanged?.Invoke();
                }
            }
        }
        
        #region Commands

        public DelegateLogCommand OpenFileTechnicalRequirementsCommand { get; protected set; }

        /// <summary>
        /// Загрузка истории проработки всего оборудования из сборки задач
        /// </summary>
        public LoadHistoryTasksCommand LoadHistoryTasksCommand { get; }

        #endregion

        #region Events

        /// <summary>
        /// Событие замены списка задач
        /// </summary>
        public event Action<TTasksWrapper, TTasksWrapper> PriceEngineeringTasksWrapperChanged;

        public event Action SelectedFileTechnicalRequirementsChanged;

        #endregion

        protected TasksViewModel(IUnityContainer container) : base(container)
        {
            OpenFileTechnicalRequirementsCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если файл уже в хранилище
                        if (string.IsNullOrEmpty(SelectedFileTechnicalRequirements.Path))
                        {
                            container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedFileTechnicalRequirements.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, SelectedFileTechnicalRequirements.Name);
                        }
                        //если файл еще не загружен в хранилище
                        else
                        {
                            Process.Start(SelectedFileTechnicalRequirements.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        container.Resolve<IMessageService>().Message("Ошибка при открытии файла ТЗ", e.PrintAllExceptions());
                    }
                });

            LoadHistoryTasksCommand = new LoadHistoryTasksCommand(
                () => this.TasksWrapper.Model,
                container.Resolve<IFilesStorageService>(),
                container.Resolve<IPrintPriceEngineering>(),
                container.Resolve<IMessageService>());
        }

        #region Load

        public virtual void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            var tasks = ((PriceEngineeringTasksRepository)UnitOfWork.Repository<PriceEngineeringTasks>()).GetForPriceEngineering(priceEngineeringTasks.Id);
            this.TasksWrapper = GetPriceEngineeringTasksWrapper(tasks, Container);
        }

        public virtual void Load(PriceEngineeringTask priceEngineeringTask)
        {
            this.Load(priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork));
        }

        #endregion

        protected abstract TTasksWrapper GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container);

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
            this.TasksWrapper.ChildTasks.ForEach(viewModel => viewModel.Dispose());
            this.TasksWrapper = null;
        }
    }
}