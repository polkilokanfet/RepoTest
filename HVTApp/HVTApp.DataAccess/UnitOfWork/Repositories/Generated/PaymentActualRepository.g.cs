using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentActualRepository : BaseRepository<PaymentActual>, IPaymentActualRepository
    {
        public PaymentActualRepository(DbContext context) : base(context)
        {
        }
    }
}
