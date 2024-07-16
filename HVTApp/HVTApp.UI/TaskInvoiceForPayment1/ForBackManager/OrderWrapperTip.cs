using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class OrderWrapperTip : WrapperBase<Order>
    {
        /// <summary>
        /// Номер
        /// </summary>
        public string Number
        {
            get => Model.Number;
            set => SetValue(value);
        }
        public string NumberOriginalValue => GetOriginalValue<string>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public OrderWrapperTip(Order model) : base(model) { }
    }
}