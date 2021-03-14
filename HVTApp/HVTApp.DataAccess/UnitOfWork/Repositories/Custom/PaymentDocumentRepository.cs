using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentRepository
    {
        protected override IQueryable<PaymentDocument> GetQuary()
        {
            return Context.Set<PaymentDocument>().AsQueryable()
                .Include(paymentDocument => paymentDocument.Payments);
        }
    }
}