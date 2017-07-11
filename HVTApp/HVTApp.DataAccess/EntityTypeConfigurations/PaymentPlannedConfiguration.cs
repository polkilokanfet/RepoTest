using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class PaymentPlannedConfiguration : EntityTypeConfiguration<PaymentPlanned>
    {
        public PaymentPlannedConfiguration()
        {
            HasRequired(x => x.SalesUnit).WithMany(x => x.PaymentsPlanned);
            Property(x => x.Date).IsRequired();
            Property(x => x.Cost).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}