namespace HVTApp.DataAccess
{
    public partial class ParameterGroupConfiguration
    {
        public ParameterGroupConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasOptional(x => x.Measure).WithMany();
        }
    }
}