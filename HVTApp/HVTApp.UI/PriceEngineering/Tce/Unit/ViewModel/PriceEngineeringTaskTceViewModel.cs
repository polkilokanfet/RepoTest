using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel
{
    public abstract class PriceEngineeringTaskTceViewModel : BaseDetailsViewModel<PriceEngineeringTaskTceWrapper1, PriceEngineeringTaskTce, AfterSavePriceEngineeringTaskTceEvent>
    {
        private PriceEngineeringTaskTceItem _selectedPriceEngineeringTaskTceItem;

        /// <summary>
        /// Может ли бэк-менеджер редактировать заявку
        /// </summary>
        public bool AllowEdit => GlobalAppProperties.User.RoleCurrent == Role.BackManager &&
                                 Item != null &&
                                 Item.Model.LastAction != PriceEngineeringTaskTceStoryItemStoryAction.Finish;

        public PriceEngineeringTaskTceItem SelectedPriceEngineeringTaskTceItem
        {
            get => _selectedPriceEngineeringTaskTceItem;
            set
            {
                _selectedPriceEngineeringTaskTceItem = value;
                LoadFilesCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand LoadFilesCommand { get; }

        protected PriceEngineeringTaskTceViewModel(IUnityContainer container) : base(container)
        {
            LoadFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = SelectedPriceEngineeringTaskTceItem.TceStructureCostVersions.First().ParentPriceEngineeringTask.FilesTechnicalRequirements;
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                },
                () => SelectedPriceEngineeringTaskTceItem != null);
            this.ViewModelIsLoaded += () =>
            {
                RaisePropertyChanged(nameof(AllowEdit));
                this.Item.PropertyChanged += (sender, args) => RaisePropertyChanged(nameof(AllowEdit));
            };
        }

        public void Create(IEnumerable<PriceEngineeringTask> tasks)
        {
            var priceEngineeringTasks = tasks.Select(x => this.UnitOfWork.Repository<PriceEngineeringTask>().GetById(x.Id)).ToList();
            var wrapper = new PriceEngineeringTaskTceWrapper1(new PriceEngineeringTaskTce());
            wrapper.Story.Add(new PriceEngineeringTaskTceStoryItemWrapper(new PriceEngineeringTaskTceStoryItem
            {
                StoryAction = PriceEngineeringTaskTceStoryItemStoryAction.Start,
                PriceEngineeringTaskTceId = wrapper.Model.Id
            }));

            foreach (var priceEngineeringTask in priceEngineeringTasks)
            {
                wrapper.PriceEngineeringTaskList.Add(new PriceEngineeringTaskWrapper1(priceEngineeringTask));
                foreach (var task in priceEngineeringTask.GetAllPriceEngineeringTasks())
                {
                    var structureCostVersion = new PriceEngineeringTaskTceStructureCostVersion
                    {
                        ParentUnitId = task.Id,
                        PriceEngineeringTaskTceId = wrapper.Model.Id
                    };
                    wrapper.SccVersions.Add(new TceStructureCostVersion(structureCostVersion, priceEngineeringTasks));

                    foreach (var blockAdded in task.ProductBlocksAdded)
                    {
                        var structureCostVersion1 = new PriceEngineeringTaskTceStructureCostVersion
                        {
                            ParentUnitId = blockAdded.Id,
                            PriceEngineeringTaskTceId = wrapper.Model.Id
                        };
                        wrapper.SccVersions.Add(new TceStructureCostVersion(structureCostVersion1, priceEngineeringTasks));
                    }
                }
            }

            this.Load(wrapper, this.UnitOfWork);
        }

    }
}