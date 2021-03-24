using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitOrderItem : WrapperBase<SalesUnit>, ISalesUnitOrder
    {
        private readonly PriceCalculationItem _priceCalculationItem;

        public DateTime? SignalToStartProductionDone
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }
        public DateTime? SignalToStartProductionDoneOriginalValue => GetOriginalValue<DateTime?>(nameof(SignalToStartProductionDone));
        public bool SignalToStartProductionDoneIsChanged => GetIsChanged(nameof(SignalToStartProductionDone));

        public DateTime? EndProductionPlanDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }
        public DateTime? EndProductionPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(EndProductionPlanDate));
        public bool EndProductionPlanDateIsChanged => GetIsChanged(nameof(EndProductionPlanDate));

        public string OrderPosition
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string OrderPositionOriginalValue => GetOriginalValue<string>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        public OrderWrapper Order
        {
            get => GetWrapper<OrderWrapper>();
            set => SetComplexValue<Order, OrderWrapper>(Order, value);
        }

        public DateTime EndProductionDateExpected => Model.DeliveryDateExpected.AddDays(-Model.DeliveryPeriodCalculated);


        public string TceInfo => _priceCalculationItem?.ToString() ?? "–асчет переменных затрат еще не делалс€";


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