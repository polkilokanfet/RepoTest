namespace HVTApp.DataAccess
{
    public partial class ParameterGroupConfiguration
    {
        public ParameterGroupConfiguration()
        {
            HasOptional(x => x.Measure).WithMany();
        }
    }
}