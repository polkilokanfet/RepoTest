using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferConfiguration : EntityTypeConfiguration<Offer>
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
            Property(x => x.ValidityDate).IsRequired();
            Property(x => x.Vat).IsRequired();
            HasMany(x => x.SalesUnits).WithOptional().WillCascadeOnDelete(true);
        }
    }
}