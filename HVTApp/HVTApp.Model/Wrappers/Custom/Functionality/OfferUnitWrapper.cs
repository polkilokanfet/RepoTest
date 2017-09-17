namespace HVTApp.Model.Wrappers
{
    public partial class OfferUnitWrapper : IProductUnit
    {
        public FacilityWrapper Facility => ProjectUnit.Facility;
    }
}