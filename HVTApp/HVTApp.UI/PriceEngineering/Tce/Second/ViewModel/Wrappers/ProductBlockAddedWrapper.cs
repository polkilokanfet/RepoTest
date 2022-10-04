using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class ProductBlockAddedWrapper : WrapperBase<PriceEngineeringTaskProductBlockAdded>
    {
        /// <summary>
        /// Версии SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; private set; }

        public SccVersionWrapper TargetSccVersion { get; private set; }

        public ProductBlockAddedWrapper(PriceEngineeringTaskProductBlockAdded model) : base(model)
        {
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");

            var sccVersions = new List<SccVersionWrapper>();
            foreach (var structureCostVersion in Model.StructureCostVersions)
            {
                var sccVersionWrapper =
                    structureCostVersion.OriginalStructureCostNumber == this.Model.ProductBlock.StructureCostNumber
                        ? new SccVersionWrapperTarget(structureCostVersion, Model.ProductBlock.ToString())
                        : new SccVersionWrapper(structureCostVersion, Model.ProductBlock.ToString());
                sccVersions.Add(sccVersionWrapper);
            }

            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(sccVersions);
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);
        }

        public override void InitializeOther()
        {
            TargetSccVersion = this.StructureCostVersions.SingleOrDefault(x => x.OriginalStructureCostNumber == this.Model.ProductBlock.StructureCostNumber);
            if (TargetSccVersion == null)
            {
                TargetSccVersion = new SccVersionWrapperTarget(new StructureCostVersion { OriginalStructureCostNumber = this.Model.ProductBlock.StructureCostNumber }, this.Model.ProductBlock.ToString());
                StructureCostVersions.Add(TargetSccVersion);
                StructureCostVersions.AcceptChanges();
            }
        }
    }
}