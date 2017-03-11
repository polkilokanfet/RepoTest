using HVTApp.Model.PaymentsCollections;

namespace HVTApp.Model
{
    public class OfferProduct : BaseEntity
    {
        public virtual ProductMain ProductMain { get; set; }
        public virtual OfferUnit OfferUnit { get; set; }
        public virtual CostInfo CostInfo { get; set; }
        public virtual PaymentsConditionsCollection PaymentsConditions { get; set; }
        public virtual PlannedTermProduction PlannedTermProduction { get; set; }
    }
}