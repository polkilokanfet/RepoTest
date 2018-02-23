using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Cost : BaseEntity
    {
        public virtual Currency Currency { get; set; }
        public double Sum { get; set; }
    }
}