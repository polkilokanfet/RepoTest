using System;
using System.Collections.Generic;
using System.Diagnostics;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
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
    public abstract class PriceEngineeringTasksViewModel<TPriceEngineeringTasksWrapper, TPriceEngineeringTaskViewModel> : ViewModelBase, IPriceEngineeringTasksViewModel
        where TPriceEngineeringTasksWrapper : PriceEngineeringTasksContainerWrapper<TPriceEngineeringTaskViewModel>
        where TPriceEngineeringTaskViewModel : TaskViewModel<>
    {
        private TPriceEngineeringTasksWrapper _priceEngineeringTasksWrapper;
        private PriceEngineeringTasksFileTechnicalRequirementsWrapper _selectedFileTechnicalRequirements;

        public bool IsNew { get; protected set; } = false;

        public TPriceEngineeringTasksWrapper PriceEngineeringTasksWrapper
        {
            get => _priceEngineeringTasksWrapper;
            protected set
            {
                var originValue = _priceEngineeringTasksWrapper;
                if (Equals(_priceEngineeringTasksWrapper, value)) return;

                _priceEngineeringTasksWrapper = value;

                RaisePropertyChanged(nameof(AllowEditProps));
                this.PriceEngineeringTasksWrapperChanged?.Invoke(originValue, value);
            }
        }
        
        public PriceEngineeringTasksFileTechnicalRequirementsWrapper SelectedFileTechnicalRequirements
        {
            get => _selectedFileTechnicalRequirements;
            set
            {
                if (Equals(_selectedFileTechnicalRequirements, value)) return;
                _selectedFileTechnicalRequirements = value;
                this.SelectedFileTechnicalRequirementsChanged?.Invoke();
            }
        }
        
        #region Commands

        public DelegateLogCommand OpenFileTechnicalRequirementsCommand { get; protected set; }

        #endregion

        #region Events

        /// <summary>
        /// Событие замены списка задач
        /// </summary>
        public event Action<TPriceEngineeringTasksWrapper, TPriceEngineeringTasksWrapper> PriceEngineeringTasksWrapperChanged;

        public event Action SelectedFileTechnicalRequirementsChanged;

        #endregion

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public virtual bool AllowEditProps => false;

        protected PriceEngineeringTasksViewModel(IUnityContainer container) : base(container)
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
                        container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при открытии файла ТЗ", e.PrintAllExceptions());
                    }
                });
        }

        public virtual void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            var tasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            this.PriceEngineeringTasksWrapper = GetPriceEngineeringTasksWrapper(tasks, Container);
        }

        public virtual void Load(PriceEngineeringTask priceEngineeringTask)
        {
            this.Load(priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork));
        }

        protected abstract TPriceEngineeringTasksWrapper GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container);

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
            this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(viewModel => viewModel.Dispose());
            this.PriceEngineeringTasksWrapper = null;
        }
    }
}