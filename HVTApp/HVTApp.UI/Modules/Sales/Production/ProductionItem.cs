using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionItem : WrapperBase<SalesUnit>
    {
        private readonly PriceCalculationItem _priceCalculationItem;

        public DateTime? SignalToStartProduction
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }


        public DateTime? SignalToStartProductionDone
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public DateTime DeliveryDateExpected
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }


        public DateTime EndProductionDateExpected
        {
            get { return Model.DeliveryDateExpected.AddDays(-Model.DeliveryPeriodCalculated); }
            set
            {
                this.DeliveryDateExpected = value.AddDays(Model.DeliveryPeriodCalculated);
                OnPropertyChanged();
            }
        }

        public string TceInfo => _priceCalculationItem?.ToString() ?? "no information";

        public bool IsProduced => Model.EndProductionDateCalculated < DateTime.Today;

        public int DifExpected => (Model.EndProductionDateCalculated - EndProductionDateExpected).Days;

        public int DifContract=> (Model.EndProductionDateCalculated - Model.EndProductionDateByContractCalculated).Days;

        public ProductionItem(SalesUnit model, PriceCalculationItem priceCalculationItem) : base(model)
        {
            _priceCalculationItem = priceCalculationItem;
        }
    }
}