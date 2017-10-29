using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedRepository : BaseRepository<PaymentPlanned>, IPaymentPlannedRepository
    {
        public PaymentPlannedRepository(DbContext context) : base(context)
        {
        }
    }
}
