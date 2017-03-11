namespace HVTApp.Model
{
    public class TermsInfo : BaseEntity
    {
        #region Terms
        /// <summary>
        /// �������� ���� ������������ �������.
        /// </summary>
        public int TermProductionPlan { get; set; }

        /// <summary>
        /// �������� ���� �� ������������ �� ��������� ������������.
        /// </summary>
        public int TermFromCompleteToProductionPlan { get; set; }

        /// <summary>
        /// �������� ���� �� �������� �� ��������.
        /// </summary>
        public int TermFromShipmentToDeliveryPlan { get; set; }
        #endregion

    }
}