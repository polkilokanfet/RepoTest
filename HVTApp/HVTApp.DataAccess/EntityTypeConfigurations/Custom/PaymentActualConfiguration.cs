namespace HVTApp.DataAccess
{
    public partial class PaymentActualConfiguration
    {
        public PaymentActualConfiguration()
        {
            Property(paymentActual => paymentActual.Date).IsRequired();
            Property(paymentActual => paymentActual.Sum).IsRequired();
        }
    }
}