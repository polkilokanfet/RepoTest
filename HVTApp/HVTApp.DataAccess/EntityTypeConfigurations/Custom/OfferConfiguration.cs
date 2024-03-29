namespace HVTApp.DataAccess
{
    public partial class OfferConfiguration
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Project).WithMany().WillCascadeOnDelete(true);
            Property(x => x.ValidityDate).IsRequired();
            Property(x => x.Vat).IsRequired();
        }
    }
}