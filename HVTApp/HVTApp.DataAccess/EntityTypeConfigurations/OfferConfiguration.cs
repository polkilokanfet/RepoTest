using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class OfferConfiguration : EntityTypeConfiguration<Offer>
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Document).WithOptional();
            HasRequired(x => x.Project).WithMany(x => x.Offers);
            HasRequired(x => x.Tender).WithMany(x => x.Offers);
            Property(x => x.ValidityDate).IsRequired();
            HasMany(x => x.OfferUnits).WithRequired(x => x.Offer);
            Property(x => x.Vat).IsRequired();
        }
    }
}