using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentActualConfiguration : EntityTypeConfiguration<PaymentActual>
    {
        public PaymentActualConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Sum).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}