using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public abstract class TasksWrapperBackOfficeBase<T> : TasksWrapper<T>
        where T : TaskViewModelBackOfficeBase
    {
        public IValidatableChangeTrackingCollection<TasksTceItem> Items { get; private set; }

        public event Action<PriceEngineeringTask> LoadFilesRequest;

        protected TasksWrapperBackOfficeBase(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
            foreach (var item in Items)
            {
                item.LoadFilesRequest += task => this.LoadFilesRequest?.Invoke(task);
            }
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");

            var tasksTceItems = Model
                .ChildPriceEngineeringTasks
                .Where(priceEngineeringTask => priceEngineeringTask.IsSuitableForLoadInTce())
                .Select(priceEngineeringTask => new TasksTceItem(priceEngineeringTask));

            Items = new ValidatableChangeTrackingCollection<TasksTceItem>(tasksTceItems);

            RegisterCollection(Items, Model.ChildPriceEngineeringTasks);
        }
    }
}