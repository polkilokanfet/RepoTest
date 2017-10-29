using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentConditionRepository : BaseRepository<PaymentCondition>, IPaymentConditionRepository
    {
        public PaymentConditionRepository(DbContext context) : base(context)
        {
        }
    }
}
