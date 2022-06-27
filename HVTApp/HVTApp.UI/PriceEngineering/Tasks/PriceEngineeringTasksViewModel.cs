using System;
using System.Diagnostics;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    /// <summary>
    /// PriceEngineeringTasksViewModel для ОГК
    /// </summary>
    public class PriceEngineeringTasksViewModel : ViewModelBase, IDisposable
    {
        private PriceEngineeringTaskViewModel _selectedPriceEngineeringTaskViewModel;
        private PriceEngineeringTasksWrapper1 _priceEngineeringTasksWrapper;
        private PriceEngineeringTasksFileTechnicalRequirementsWrapper _selectedFileTechnicalRequirements;

        public virtual bool IsNew { get; protected set; } = false;

        public PriceEngineeringTasksWrapper1 PriceEngineeringTasksWrapper
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
        
        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set
            {
                if (Equals(value, _selectedPriceEngineeringTaskViewModel)) return;

                _selectedPriceEngineeringTaskViewModel = value;
                SelectedPriceEngineeringTaskViewModelChanged?.Invoke();
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

        public event Action<PriceEngineeringTasksWrapper1, PriceEngineeringTasksWrapper1> PriceEngineeringTasksWrapperChanged;
        public event Action SelectedPriceEngineeringTaskViewModelChanged;
        public event Action SelectedFileTechnicalRequirementsChanged;

        #endregion

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public virtual bool AllowEditProps => false;

        public PriceEngineeringTasksViewModel(IUnityContainer container) : base(container)
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

        public void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            var tasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            this.PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(tasks, Container);
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            this.Load(priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork));
        }

        public virtual void Dispose()
        {
            UnitOfWork?.Dispose();
            this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(viewModel => viewModel.Dispose());
            this.PriceEngineeringTasksWrapper = null;
        }
    }
}