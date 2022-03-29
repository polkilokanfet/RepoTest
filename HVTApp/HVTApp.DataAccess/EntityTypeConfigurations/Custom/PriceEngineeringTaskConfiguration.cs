namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskConfiguration
    {
        public PriceEngineeringTaskConfiguration()
        {
            HasOptional(x => x.UserConstructor).WithMany().WillCascadeOnDelete(false);

            HasRequired(x => x.ProductBlockManager).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ProductBlockEngineer).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.ProductBlocksAdded).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskId).WillCascadeOnDelete(false);

            HasMany(x => x.FilesTechnicalRequirements).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskId).WillCascadeOnDelete(false);
            HasMany(x => x.FilesAnswers).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskId).WillCascadeOnDelete(false);

            HasMany(x => x.Messages).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskId).WillCascadeOnDelete(false);
            HasMany(x => x.ChildPriceEngineeringTasks).WithOptional().HasForeignKey(x => x.ParentPriceEngineeringTaskId).WillCascadeOnDelete(false);

            HasMany(x => x.Statuses).WithRequired().HasForeignKey(x => x.PriceEngineeringTaskId).WillCascadeOnDelete(false);

            HasMany(x => x.SalesUnits).WithMany();
        }
    }
}