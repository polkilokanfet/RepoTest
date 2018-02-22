using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ќбласть, край, республика и т.д.
    /// </summary>
    public partial class Region : BaseEntity
    {
        public string Name { get; set; }
        public virtual District District { get; set; }

        public override string ToString()
        {
            return $"{District}, {Name}";
        }
    }
}