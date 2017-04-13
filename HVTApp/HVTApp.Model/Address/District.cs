using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public int StandartDeliveryPeriod { get; set; }
        public string Name { get; set; }
        public virtual Country Country { get; set; }
    }
}