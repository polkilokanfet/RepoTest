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
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; }

        public List<SccVersionWrapper> SccVersions { get; }

        public SccVersionWrapper TargetSccVersion
        {
            get
            {
                var result = this.StructureCostVersions.SingleOrDefault(x => x.OriginalStructureCostNumber == this.Model.ProductBlock.StructureCostNumber);
                if (result == null)
                {
                    result = new SccVersionWrapper(new StructureCostVersion { OriginalStructureCostNumber = this.Model.ProductBlock.StructureCostNumber }, this.Model.ProductBlock.ToString());
                    StructureCostVersions.Add(result);
                }

                return result;
            }
        }

        public ProductBlockAddedWrapper(PriceEngineeringTaskProductBlockAdded model) : base(model)
        {
            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");
            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(Model.StructureCostVersions.Select(e => new SccVersionWrapper(e, Model.ProductBlock.ToString())));
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);

            SccVersions = new List<SccVersionWrapper> { this.TargetSccVersion };
        }
    }
}