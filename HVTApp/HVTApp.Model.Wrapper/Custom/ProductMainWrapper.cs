namespace HVTApp.Model.Wrapper
{
    public partial class ProductMainWrapper
    {
        protected override void RunInConstructor()
        {
            //подписка на событие перезагрузки коллекции плановых платежей.
            Model.PaymentsInfo.PaymentsPlanned.CollectionReloaded += PaymentsPlanned_CollectionReloaded;
        }

        /// <summary>
        /// Реакция на перезагрузку коллекции плановых платежей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentsPlanned_CollectionReloaded(object sender, System.EventArgs e)
        {
            this.PaymentsInfo.PaymentsPlanned.Clear();
            foreach (var payment in Model.PaymentsInfo.PaymentsPlanned)
            {
                this.PaymentsInfo.PaymentsPlanned.Add(new PaymentPlannedWrapper(payment));
            }
        }
    }
}
