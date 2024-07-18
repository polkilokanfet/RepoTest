using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class SalesUnitWrapperTip : WrapperBase<SalesUnit>
    {
        /// <summary>
        /// Позиция заказа
        /// </summary>
        public string OrderPosition
        {
            get => Model.OrderPosition;
            set
            {
                if (this.Order != null && this.Order.AllowEdit)
                    SetValue(value);
            }
        }

        public string OrderPositionOriginalValue => GetOriginalValue<string>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        /// <summary>
        /// Заказ
        /// </summary>
        public OrderWrapperTip Order
        {
            get => GetWrapper<OrderWrapperTip>();
            set
            {
                SetComplexValue<Order, OrderWrapperTip>(Order, value);
                OrderChangedEvent?.Invoke(value);
            }
        }

        public event Action<OrderWrapperTip> OrderChangedEvent;

        public SalesUnitWrapperTip(SalesUnit model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapperTip(Model.Order));
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.Order == null)
                yield return new ValidationResult("Не назначен заказ", new[] { nameof(Order) });
            //else if (string.IsNullOrWhiteSpace(this.Order.Number))
            //    yield return new ValidationResult("Не назначен номер заказа", new[] { nameof(Order.Number) });


            if (string.IsNullOrWhiteSpace(this.OrderPosition))
                yield return new ValidationResult("Не назначена позиция заказа", new[] {nameof(OrderPosition)});
        }
    }
}