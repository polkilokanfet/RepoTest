namespace HVTApp.Model
{
    public class TermsInfo : BaseEntity
    {
        #region Terms
        /// <summary>
        /// Плановый срок производства изделия.
        /// </summary>
        public int TermProductionPlan { get; set; }

        /// <summary>
        /// Плановый срок от комплектации до окончания производства.
        /// </summary>
        public int TermFromCompleteToProductionPlan { get; set; }

        /// <summary>
        /// Плановый срок от отгрузки до доставки.
        /// </summary>
        public int TermFromShipmentToDeliveryPlan { get; set; }
        #endregion

    }
}