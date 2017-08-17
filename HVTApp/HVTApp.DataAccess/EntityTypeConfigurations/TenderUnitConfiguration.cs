using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderUnitConfiguration : EntityTypeConfiguration<TenderUnit>
    {
        public TenderUnitConfiguration()
        {
            HasRequired(x => x.ProjectUnit).WithMany().HasForeignKey(x => x.ProjectUnitId).WillCascadeOnDelete(false);
            HasRequired(x => x.Tender).WithMany(x => x.TenderUnits);
            HasRequired(x => x.Product).WithMany();
            HasOptional(x => x.ProducerWinner).WithMany().HasForeignKey(x => x.ProducerWinnerId);

            HasMany(x => x.OfferUnits).WithOptional(x => x.TenderUnit).HasForeignKey(x => x.TenderUnitId);
            HasMany(x => x.PaymentsConditions).WithMany();

            Property(x => x.Cost).IsRequired();
            Property(x => x.DeliveryDate).IsRequired();
        }
    }
}