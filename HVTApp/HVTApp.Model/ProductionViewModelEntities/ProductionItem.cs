using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.ProductionViewModelEntities
{
    public class ProductionItem
    {
        public SalesUnit Model { get; }

        public DateTime? SignalToStartProduction => Model.SignalToStartProduction;

        /// <summary>
        /// Ожидаемая дата производства
        /// </summary>
        public DateTime EndProductionDateExpected => Model.DeliveryDateExpected.AddDays(-Model.DeliveryPeriodCalculated);

        public bool IsProduced
        {
            get
            {
                if (Model.Product.ProductBlock.IsService)
                    return Model.RealizationDateCalculated < DateTime.Today;

                return Model.EndProductionDateCalculated < DateTime.Today;
            }
        }

        public int DifExpected => (Model.EndProductionDateCalculated - EndProductionDateExpected).Days;

        public int DifContract => (Model.EndProductionDateCalculated - Model.EndProductionDateByContractCalculated).Days;

        public ProductionItem(SalesUnit salesUnit)
        {
            this.Model = salesUnit;
        }
    }
}