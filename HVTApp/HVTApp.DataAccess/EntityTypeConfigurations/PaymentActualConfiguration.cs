using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class PaymentActualConfiguration : EntityTypeConfiguration<PaymentActual>
    {
        public PaymentActualConfiguration()
        {
            HasRequired(x => x.SalesUnit).WithMany(x => x.PaymentsActual);
            Property(x => x.Date).IsRequired();
            Property(x => x.Cost).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
            HasRequired(x => x.Document).WithMany(x => x.Payments);
        }
    }
}