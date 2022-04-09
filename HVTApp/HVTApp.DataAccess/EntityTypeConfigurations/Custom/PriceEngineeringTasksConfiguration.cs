namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTasksConfiguration
    {
        public PriceEngineeringTasksConfiguration()
        {
            HasRequired(priceEngineeringTasks => priceEngineeringTasks.UserManager).WithMany().WillCascadeOnDelete(false);
            HasMany(priceEngineeringTasks => priceEngineeringTasks.ChildPriceEngineeringTasks).WithOptional().HasForeignKey(x => x.ParentPriceEngineeringTasksId).WillCascadeOnDelete(false);
            HasMany(priceEngineeringTasks => priceEngineeringTasks.FilesTechnicalRequirements).WithRequired().HasForeignKey(x => x.PrEngTasksId).WillCascadeOnDelete(false);
            HasMany(priceEngineeringTasks => priceEngineeringTasks.PriceCalculations).WithOptional().HasForeignKey(priceCalculation => priceCalculation.PriceEngineeringTasksId).WillCascadeOnDelete(false);
        }
    }
}