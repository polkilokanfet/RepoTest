namespace HVTApp.DataAccess
{
    public partial class LocalityConfiguration
    {
        public LocalityConfiguration()
        {
            HasRequired(x => x.LocalityType).WithMany();
            HasRequired(x => x.Region).WithMany();
        }
    }
}