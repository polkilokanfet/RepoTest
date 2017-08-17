using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class OfferConfiguration : EntityTypeConfiguration<Offer>
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Document).WithOptional();
            Property(x => x.ValidityDate).IsRequired();
            HasMany(x => x.OfferUnits).WithRequired(x => x.Offer);
            Property(x => x.Vat).IsRequired();
        }
    }
}