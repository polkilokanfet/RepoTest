using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public virtual Country Country { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}