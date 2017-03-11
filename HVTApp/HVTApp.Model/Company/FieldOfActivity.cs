namespace HVTApp.Model
{
    /// <summary>
    /// Сфера деятельности компании.
    /// </summary>
    public class ActivityFild : BaseEntity
    {
        public FieldOfActivity FieldOfActivity { get; set; }
        public virtual Company Company { get; set; }
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
