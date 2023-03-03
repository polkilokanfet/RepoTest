using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelManagerBack : TaskViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => true;

        public TaskViewModelManagerBack(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
            var originalStructureCostNumber = this.Model.ProductBlock.StructureCostNumber;

            //���� ��� ����������� scc, ��������� ���
            if (this.StructureCostVersions.Any(x => x.OriginalStructureCostNumber == originalStructureCostNumber) == false)
            {
                var scc = new SccVersionWrapper(new StructureCostVersion(), this.Model.ProductBlock.ToString(), true);
                this.StructureCostVersions.Add(scc);
                scc.OriginalStructureCostNumber = originalStructureCostNumber;
                StructureCostVersions.AcceptChanges();
            }

            this.StructureCostVersions.ForEach(x => x.Constructor = this.Model.UserConstructor.Employee.Person.ToString());

            LoadFilesCommand = new DelegateCommand(() =>
            {
                LoadFilesRequest?.Invoke(this.Model);
            });
        }


        /// <summary>
        /// �������� ������
        /// </summary>
        public IValidatableChangeTrackingCollection<TasksTceItem> ChildPriceEngineeringTasks { get; private set; }

        /// <summary>
        /// ����������� ����� �������� �� ��������-������������
        /// </summary>
        public IValidatableChangeTrackingCollection<ProductBlockAddedWrapper> BlockAddedList { get; private set; }

        /// <summary>
        /// ������ SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; private set; }

        /// <summary>
        /// ������ scc ��� ����������� �� View
        /// </summary>
        public IEnumerable<SccVersionWrapper> SccVersions =>
            StructureCostVersions
                .Union(this.ChildPriceEngineeringTasks.SelectMany(x => x.SccVersions))
                .Union(this.BlockAddedList.SelectMany(x => x.StructureCostVersions))
                .OrderBy(x => x.Name);

        public ICommand LoadFilesCommand { get; }

        public event Action<PriceEngineeringTask> LoadFilesRequest;

        protected override void InitializeCollectionProperties()
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TasksTceItem>(Model.ChildPriceEngineeringTasks.Select(e => new TasksTceItem(e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            BlockAddedList = new ValidatableChangeTrackingCollection<ProductBlockAddedWrapper>(Model.ProductBlocksAdded.Select(e => new ProductBlockAddedWrapper(e, this.Model.UserConstructor.Employee.Person.ToString())));
            RegisterCollection(BlockAddedList, Model.ProductBlocksAdded);

            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");

            var originalStructureCostNumber = this.Model.ProductBlock.StructureCostNumber;
            var structureCostName = this.Model.ProductBlock.ToString();
            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(Model.StructureCostVersions.Select(x => new SccVersionWrapper(x, structureCostName, originalStructureCostNumber == x.OriginalStructureCostNumber)));
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);
        }

    }
}