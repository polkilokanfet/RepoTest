using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceItem : WrapperBase<PriceEngineeringTask>
    {
        #region TcePosition

        /// <summary>
        /// ������� � ���
        /// </summary>
        public string TcePosition
        {
            get => GetValue<string>();
            set
            {
                foreach (var childTask in ChildPriceEngineeringTasks)
                {
                    childTask.TcePosition = value;
                }
                SetValue(value);
            }
        }

        public string TcePositionOriginalValue => GetOriginalValue<string>(nameof(TcePosition));
        public bool TcePositionIsChanged => GetIsChanged(nameof(TcePosition));

        #endregion

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

        public TasksTceItem(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
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

            this.StructureCostVersions.ForEach(sccVersionWrapper => sccVersionWrapper.Constructor = this.Model.UserConstructor?.Employee.Person.ToString());

            LoadFilesCommand = new DelegateCommand(() =>
            {
                LoadFilesRequest?.Invoke(this.Model);
            });
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            var sccVersionWrappers = this.SccVersions
                .Where(x => x.IsActual)
                .Where(x => x.IsValid == false);
            foreach (var sccVersionWrapper in sccVersionWrappers)
            {
                yield return new ValidationResult($"{nameof(SccVersions)} has not valid items", new[] {nameof(SccVersions)});
            }

            if (string.IsNullOrWhiteSpace(this.TcePosition))
            {
                yield return new ValidationResult("������� � ��� �� ����� ���� ������", new[] {nameof(TcePosition)});
            }
        }

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