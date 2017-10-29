using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class PaymentConditionStandartConfiguration : EntityTypeConfiguration<StandartPaymentConditions>
    {
        public PaymentConditionStandartConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasMany(x => x.PaymentsConditions).WithMany();
        }
    }
}