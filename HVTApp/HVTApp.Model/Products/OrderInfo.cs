namespace HVTApp.Model
{
    public class OrderInfo : BaseEntity
    {
        public ProductBase Product { get; set; }

        #region OrderInfo
        /// <summary>
        /// Порядковый номер в заказе.
        /// </summary>
        public int OrderPosition { get; set; }

        /// <summary>
        /// Заводской номер изделия.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Заводской заказ изделия.
        /// </summary>
        public virtual Order Order { get; set; }
        #endregion
    }
}