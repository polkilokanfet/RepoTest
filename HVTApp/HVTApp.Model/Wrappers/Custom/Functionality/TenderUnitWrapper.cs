namespace HVTApp.Model.Wrappers
{
    public partial class TenderUnitWrapper : IProductUnit
    {
        public FacilityWrapper Facility => ProjectUnit.Facility;
    }
}