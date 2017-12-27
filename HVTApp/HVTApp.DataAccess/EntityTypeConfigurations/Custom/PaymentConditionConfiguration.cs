using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentConditionConfiguration : EntityTypeConfiguration<PaymentCondition>
    {
        public PaymentConditionConfiguration()
        {
            Property(x => x.Part).IsRequired();
            Property(x => x.DaysToPoint).IsRequired();
            Property(x => x.PaymentConditionPoint).IsRequired();
        }
    }
}