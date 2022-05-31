using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public abstract class TasksTceViewModel : BaseDetailsViewModel<TasksTceWrapper, PriceEngineeringTasks, AfterSavePriceEngineeringTasksEvent>
    {
        private TasksTceItem _selectedItem;
        public virtual bool AllowEdit => GlobalAppProperties.User.RoleCurrent == Role.BackManager;

        public DelegateLogCommand LoadFilesCommand { get; }

        public TasksTceItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                LoadFilesCommand.RaiseCanExecuteChanged();
            }
        }


        protected TasksTceViewModel(IUnityContainer container) : base(container)
        {
            LoadFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = SelectedItem.Model.FilesTechnicalRequirements;
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                },
                () => SelectedItem != null);
            this.ViewModelIsLoaded += () =>
            {
                RaisePropertyChanged(nameof(AllowEdit));
                this.Item.PropertyChanged += (sender, args) => RaisePropertyChanged(nameof(AllowEdit));
            };
        }
    }
}