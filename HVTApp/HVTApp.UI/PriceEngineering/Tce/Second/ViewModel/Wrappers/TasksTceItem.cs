using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
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
        public IValidatableChangeTrackingCollection<TasksTceItem> ChildPriceEngineeringTasks { get; private set; }

        /// <summary>
        /// Добавленные блоки продукта от инженера-конструктора
        /// </summary>
        public IValidatableChangeTrackingCollection<ProductBlockAddedWrapper> BlockAddedList { get; private set; }

        /// <summary>
        /// Версии SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; private set; }

        /// <summary>
        /// Список scc для отображения во View
        /// </summary>
        public List<SccVersionWrapper> SccVersions { get; } = new List<SccVersionWrapper>();

        public ICommand LoadFilesCommand { get; private set; }

        /// <summary>
        /// актуальная версия scc
        /// </summary>
        public SccVersionWrapper TargetSccVersion { get; private set; }

        public event Action<PriceEngineeringTask> LoadFilesRequest; 

        public TasksTceItem(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
        {
        }

        protected override void InitializeCollectionProperties()
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
        }

        public override void InitializeOther()
        {
            //актуальная версия scc
            TargetSccVersion = this.StructureCostVersions.SingleOrDefault(x => x.OriginalStructureCostNumber == this.Model.ProductBlockEngineer.StructureCostNumber);
            if (TargetSccVersion == null)
            {
                TargetSccVersion = new SccVersionWrapper(new StructureCostVersion { OriginalStructureCostNumber = this.Model.ProductBlockEngineer.StructureCostNumber }, Model.ProductBlockEngineer.ToString());
                StructureCostVersions.Add(TargetSccVersion);
            }
            TargetSccVersion.IsTarget = true;

            SccVersions.Add(this.TargetSccVersion);
            SccVersions.AddRange(ChildPriceEngineeringTasks.SelectMany(x => x.SccVersions));
            SccVersions.AddRange(BlockAddedList.SelectMany(x => x.SccVersions));

            LoadFilesCommand = new DelegateCommand(() =>
            {
                LoadFilesRequest?.Invoke(this.Model);
            });
        }
    }
}