using HVTApp.Model;
using HVTApp.Model.Structures;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class CostStructure : BindableBase
    {
        private readonly ICostStructureItem _targetItem;

        public double Cost
        {
            get { return _targetItem.Cost; }
            set
            {
                if (Equals(Cost, value)) return;
                //обновляем значение в целевом айтеме
                _targetItem.Cost = value;
                //сигнал об изменении маржи
                OnPropertyChanged(nameof(MarginalIncome));
                //сигнал об изменении стоимости
                OnPropertyChanged();
            }
        }

        public PriceStructures PriceStructures
        {
            get
            {
                //срок актуальности
                var priceTerm = GlobalAppProperties.Actual.ActualPriceTerm;
                return GlobalAppProperties.PriceService.GetPriceStructures(_targetItem.GetIUnit(), _targetItem.PriceDate, priceTerm);
            }
        }

        public double CostMin { get; set; }

        /// <summary>
        /// Маржа в %
        /// </summary>
        public double? MarginalIncome
        {
            get { return Cost - CostMin <= 0 ? default(double?) : (1.0 - PriceStructures.TotalPriceFixedCostLess / (Cost - CostMin)) * 100.0; }
            set
            {
                //если прилетела маржа не от 0 до 100
                if (!value.HasValue || value >= 100) return;

                var marginalIncome = value.Value;
                Cost = PriceStructures.TotalPriceFixedCostLess / (1.0 - marginalIncome / 100.0) + CostMin;
                OnPropertyChanged();
            }
        }


        public CostStructure(ICostStructureItem costStructureItem)
        {
            _targetItem = costStructureItem;
        }
    }
}