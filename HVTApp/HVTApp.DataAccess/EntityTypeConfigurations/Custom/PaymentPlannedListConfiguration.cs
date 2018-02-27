using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedListConfiguration : EntityTypeConfiguration<PaymentPlannedList>
    {
        public PaymentPlannedListConfiguration()
        {
            HasRequired(x => x.Condition).WithMany();
            HasMany(x => x.Payments).WithRequired().WillCascadeOnDelete(true);
        }
    }
}