namespace HVTApp.DataAccess
{
    public partial class LocalityConfiguration
    {
        public LocalityConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.StandartDeliveryPeriod).IsOptional();
            HasRequired(x => x.LocalityType).WithMany();
            HasRequired(x => x.Region).WithMany();
        }
    }
}