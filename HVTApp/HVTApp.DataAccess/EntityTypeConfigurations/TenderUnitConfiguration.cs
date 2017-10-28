using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderUnitConfiguration : EntityTypeConfiguration<TenderUnit>
    {
        public TenderUnitConfiguration()
        {
            HasRequired(x => x.ProjectUnit).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Product).WithMany();
            HasOptional(x => x.ProducerWinner).WithMany();

            HasMany(x => x.PaymentsConditions).WithMany();

            Property(x => x.Cost).IsRequired();
            Property(x => x.DeliveryDate).IsRequired();
        }
    }
}