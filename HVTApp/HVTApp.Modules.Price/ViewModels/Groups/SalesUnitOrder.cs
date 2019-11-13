using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitOrder : WrapperBase<SalesUnit>, ISalesUnitOrder
    {
        public DateTime? SignalToStartProductionDone
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? SignalToStartProductionDoneOriginalValue => GetOriginalValue<DateTime?>(nameof(SignalToStartProductionDone));
        public bool SignalToStartProductionDoneIsChanged => GetIsChanged(nameof(SignalToStartProductionDone));

        public DateTime? EndProductionPlanDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? EndProductionPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(EndProductionPlanDate));
        public bool EndProductionPlanDateIsChanged => GetIsChanged(nameof(EndProductionPlanDate));

        public string OrderPosition
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string OrderPositionOriginalValue => GetOriginalValue<string>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        public OrderWrapper Order
        {
            get { return GetWrapper<OrderWrapper>(); }
            set { SetComplexValue<Order, OrderWrapper>(Order, value); }
        }

        public SalesUnitOrder(SalesUnit model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (EndProductionPlanDate == null)
            {
                yield return new ValidationResult("EndProductionPlanDate is required", new[] { nameof(EndProductionPlanDate) });
            }
        }

    }
}