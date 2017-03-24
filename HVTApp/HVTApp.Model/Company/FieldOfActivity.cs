using System.Collections.Generic;

namespace HVTApp.Model
{
    /// <summary>
    /// Сфера деятельности компании.
    /// </summary>
    public class ActivityField : BaseEntity
    {
        public FieldOfActivity FieldOfActivity { get; set; }
        public virtual List<Company> Companies { get; set; }
    }

    public enum FieldOfActivity
    {
        ElectricityGeneration,
        ElectricityTransmission,
        IndustrialEnterprise,
        ProducerOfHighVoltageEquipment,
        Builder,
        Supplier
    }
}
