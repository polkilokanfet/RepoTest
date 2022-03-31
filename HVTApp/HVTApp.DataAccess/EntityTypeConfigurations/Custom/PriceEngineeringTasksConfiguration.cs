namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTasksConfiguration
    {
        public PriceEngineeringTasksConfiguration()
        {
            HasRequired(x => x.UserManager).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.ChildPriceEngineeringTasks).WithOptional().HasForeignKey(x => x.ParentPriceEngineeringTasksId).WillCascadeOnDelete(false);
            HasMany(x => x.FilesTechnicalRequirements).WithRequired().HasForeignKey(x => x.PriceEngineeringTasksId).WillCascadeOnDelete(false);
        }
    }
}