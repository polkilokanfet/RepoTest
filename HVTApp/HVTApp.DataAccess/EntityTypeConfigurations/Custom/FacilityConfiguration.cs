namespace HVTApp.DataAccess
{
    public partial class FacilityConfiguration
    {
        public FacilityConfiguration()
        {
            Property(x => x.Name).IsRequired();
            HasRequired(x => x.Type).WithMany();
            HasRequired(x => x.OwnerCompany).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.Address).WithOptionalDependent();
        }
    }
}