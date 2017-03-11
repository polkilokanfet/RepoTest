namespace HVTApp.Model
{
    public class OrderInfo : BaseEntity
    {
        public ProductBase Product { get; set; }

        #region OrderInfo
        /// <summary>
        /// ���������� ����� � ������.
        /// </summary>
        public int OrderPosition { get; set; }

        /// <summary>
        /// ��������� ����� �������.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// ��������� ����� �������.
        /// </summary>
        public virtual Order Order { get; set; }
        #endregion
    }
}