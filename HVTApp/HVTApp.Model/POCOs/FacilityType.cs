using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class FacilityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }
}