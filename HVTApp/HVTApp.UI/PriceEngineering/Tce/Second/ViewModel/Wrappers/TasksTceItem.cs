using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Prism.Commands;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceItem : WrapperBase<PriceEngineeringTask>
    {
        /// <summary>
        /// Дочерние задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<TasksTceItem> ChildPriceEngineeringTasks { get; }

        public IValidatableChangeTrackingCollection<ProductBlockAddedWrapper> BlockAddedList { get; }

        /// <summary>
        /// Версии SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; }


        public List<SccVersionWrapper> SccVersions { get; }

        public ICommand LoadFilesCommand { get; }

        public SccVersionWrapper TargetSccVersion
        {
            get
            {
                var result = this.StructureCostVersions.SingleOrDefault(x => x.OriginalStructureCostNumber == this.Model.ProductBlockEngineer.StructureCostNumber);
                if (result == null)
                {
                    result = new SccVersionWrapper(new StructureCostVersion { OriginalStructureCostNumber = this.Model.ProductBlockEngineer.StructureCostNumber }, Model.ProductBlockEngineer.ToString());
                    StructureCostVersions.Add(result);
                }

                return result;
            }
        }

        public event Action<PriceEngineeringTask> LoadFilesRequest; 

        public TasksTceItem(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TasksTceItem>(Model.ChildPriceEngineeringTasks.Select(e => new TasksTceItem(e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            BlockAddedList = new ValidatableChangeTrackingCollection<ProductBlockAddedWrapper>(Model.ProductBlocksAdded.Select(e => new ProductBlockAddedWrapper(e)));
            RegisterCollection(BlockAddedList, Model.ProductBlocksAdded);

            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");
            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(Model.StructureCostVersions.Select(e => new SccVersionWrapper(e, Model.ProductBlockEngineer.ToString())));
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);

            SccVersions = new List<SccVersionWrapper> { this.TargetSccVersion };
            SccVersions.AddRange(ChildPriceEngineeringTasks.SelectMany(x => x.SccVersions));
            SccVersions.AddRange(BlockAddedList.SelectMany(x => x.SccVersions));

            LoadFilesCommand = new DelegateCommand(() =>
            {
                LoadFilesRequest?.Invoke(this.Model);
            });
        }
    }
}