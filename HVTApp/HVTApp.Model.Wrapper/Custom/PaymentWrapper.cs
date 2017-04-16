namespace HVTApp.Model.Wrapper
{
    public partial class PaymentWrapper
    {
        public PaymentType Type
        {
            get
            {
                if (Date < SalesUnit.RealizationDateCalculated) return PaymentType.AccountsPayable;
                return PaymentType.Receivables;
            }
        }
    }
}
