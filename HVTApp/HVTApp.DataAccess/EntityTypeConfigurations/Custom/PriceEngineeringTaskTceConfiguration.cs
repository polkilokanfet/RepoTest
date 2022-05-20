namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskTceConfiguration
    {
        public PriceEngineeringTaskTceConfiguration()
        {
            HasOptional(x => x.BackManager).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.PriceEngineeringTaskList).WithMany();
            HasMany(x => x.SccVersions).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskTceId).WillCascadeOnDelete(false);
            HasMany(x => x.StoryItems).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskTceId).WillCascadeOnDelete(true);
        }
    }
}