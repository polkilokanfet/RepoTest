using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedConfiguration : EntityTypeConfiguration<PaymentPlanned>
    {
        public PaymentPlannedConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Sum).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}