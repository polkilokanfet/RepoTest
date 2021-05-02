using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Production
{
    public class ProductionItem : WrapperBase<SalesUnit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DateTime? SignalToStartProduction
        {
            get => Model.SignalToStartProduction;
            set => SetValue(value);
        }

        public DateTime? SignalToStartProductionDone
        {
            get => Model.SignalToStartProductionDone;
            set => SetValue(value);
        }

        public DateTime DeliveryDateExpected
        {
            get => Model.DeliveryDateExpected;
            set => SetValue(value);
        }


        public DateTime EndProductionDateExpected
        {
            get => Model.DeliveryDateExpected.AddDays(-Model.DeliveryPeriodCalculated);
            set
            {
                this.DeliveryDateExpected = value.AddDays(Model.DeliveryPeriodCalculated);
                OnPropertyChanged();
            }
        }

        public string TceInfo => Model.ActualPriceCalculationItem(_unitOfWork)?.ToString() ?? "no information";

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

        public ProductionItem(SalesUnit model, IUnitOfWork unitOfWork) : base(model)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Очистка дат при удалении юнита из производства
        /// </summary>
        public void CleanDatesOnRemoveFromProduction()
        {
            this.SignalToStartProduction = null;
            this.SignalToStartProductionDone = null;

            Model.StartProductionDate = null;
            Model.PickingDate = null;
            Model.EndProductionPlanDate = null;
            Model.EndProductionDate = null;
            Model.RealizationDate = null;
            Model.DeliveryDate = null;
        }
    }
}