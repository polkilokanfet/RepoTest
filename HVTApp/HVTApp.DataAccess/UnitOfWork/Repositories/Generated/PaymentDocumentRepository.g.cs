using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentRepository : BaseRepository<PaymentDocument>, IPaymentDocumentRepository
    {
        public PaymentDocumentRepository(DbContext context) : base(context)
        {
        }
    }
}
