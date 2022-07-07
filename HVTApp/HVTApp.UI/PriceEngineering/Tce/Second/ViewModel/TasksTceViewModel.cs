using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public abstract class TasksTceViewModel : BaseDetailsViewModel<TasksTceWrapper, PriceEngineeringTasks, AfterSavePriceEngineeringTasksEvent>
    {
        public virtual bool AllowEdit => GlobalAppProperties.User.RoleCurrent == Role.BackManager;

        protected TasksTceViewModel(IUnityContainer container) : base(container)
        {
            this.ViewModelIsLoaded += () =>
            {
                RaisePropertyChanged(nameof(AllowEdit));
                this.Item.PropertyChanged += (sender, args) => RaisePropertyChanged(nameof(AllowEdit));
            };
        }

        protected override void AfterLoading()
        {
            this.Item.LoadFilesRequest +=
                task =>
                {
                    var files = task.FilesTechnicalRequirements.Where(x => x.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                };

            base.AfterLoading();
        }
    }
}