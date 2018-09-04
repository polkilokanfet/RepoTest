using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сфера деятельности компании.
    /// </summary>
    [Designation("Сфера деятельности")]
    public partial class ActivityField : BaseEntity
    {
        public string Name { get; set; }
        public ActivityFieldEnum ActivityFieldEnum { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum ActivityFieldEnum
    {
        ElectricityGeneration,
        ElectricityTransmission,
        IndustrialEnterprise,
        ProducerOfHighVoltageEquipment,
        Builder,
        Supplier
    }
}
