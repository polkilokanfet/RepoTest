namespace HVTApp.DataAccess
{
    public partial class FacilityConfiguration
    {
        public FacilityConfiguration()
        {
            Property(facility => facility.Name).IsRequired();
            HasRequired(facility => facility.Type).WithMany().WillCascadeOnDelete(false);
            HasRequired(facility => facility.OwnerCompany).WithMany().WillCascadeOnDelete(false);
            HasRequired(facility => facility.Address).WithMany().WillCascadeOnDelete(false);
        }
    }
}