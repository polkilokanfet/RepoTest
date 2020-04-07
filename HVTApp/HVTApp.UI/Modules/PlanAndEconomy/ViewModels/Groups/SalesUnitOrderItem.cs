using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitOrderItem : WrapperBase<SalesUnit>, ISalesUnitOrder
    {
        private readonly PriceCalculationItem _priceCalculationItem;

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

        public DateTime EndProductionDateExpected => Model.DeliveryDateExpected.AddDays(-Model.DeliveryPeriodCalculated);


        public string TceInfo => _priceCalculationItem?.ToString() ?? string.Empty;


        public SalesUnitOrderItem(SalesUnit model, PriceCalculationItem priceCalculationItem) : base(model)
        {
            _priceCalculationItem = priceCalculationItem;
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
        }
    }
}