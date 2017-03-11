namespace HVTApp.Model
{
    /// <summary>
    /// Точка отсчета условия платежа.
    /// </summary>
    public enum PaymentConditionPoint
    {
        /// <summary>
        /// Начало производства.
        /// </summary>
        ProductionStart,
        /// <summary>
        /// Окончание производства.
        /// </summary>
        ProductionEnd,
        /// <summary>
        /// Отгрузка.
        /// </summary>
        Shipment,
        /// <summary>
        /// Доставка.
        /// </summary>
        Delivery
    }
}