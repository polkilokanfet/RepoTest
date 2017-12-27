using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentConfiguration : EntityTypeConfiguration<PaymentDocument>
    {
        public PaymentDocumentConfiguration()
        {
            Property(x => x.Number).IsOptional().HasMaxLength(25);
            Property(x => x.Date).IsRequired();
            HasMany(x => x.Payments).WithRequired().HasForeignKey(x => x.DocumentId);
        }
    }
}