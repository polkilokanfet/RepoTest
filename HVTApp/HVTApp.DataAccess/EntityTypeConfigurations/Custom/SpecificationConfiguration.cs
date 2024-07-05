namespace HVTApp.DataAccess
{
    public partial class SpecificationConfiguration 
    {
        public SpecificationConfiguration()
        {
            Property(specification => specification.Date).IsRequired();
            Property(specification => specification.Vat).IsRequired();
            HasRequired(specification => specification.Contract).WithMany().WillCascadeOnDelete(false);
            HasMany(specification => specification.PriceEngineeringTasks).WithOptional(x => x.Specification).WillCascadeOnDelete(false);
            HasMany(specification => specification.TechnicalRequrements).WithOptional(x => x.Specification).WillCascadeOnDelete(false);
        }
    }
}