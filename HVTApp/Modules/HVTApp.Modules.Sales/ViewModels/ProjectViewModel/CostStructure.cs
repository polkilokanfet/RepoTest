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
                //��������� �������� � ������� ������
                _targetItem.Cost = value;
                //������ �� ��������� �����
                OnPropertyChanged(nameof(MarginalIncome));
                //������ �� ��������� ���������
                OnPropertyChanged();
            }
        }

        public PriceStructures PriceStructures
        {
            get
            {
                //���� ������������
                var priceTerm = GlobalAppProperties.Actual.ActualPriceTerm;
                return GlobalAppProperties.PriceService.GetPriceStructures(_targetItem.GetIUnit(), _targetItem.PriceDate, priceTerm);
            }
        }

        public double CostMin { get; set; }

        /// <summary>
        /// ����� � %
        /// </summary>
        public double? MarginalIncome
        {
            get { return Cost - CostMin <= 0 ? default(double?) : (1.0 - PriceStructures.TotalPriceFixedCostLess / (Cost - CostMin)) * 100.0; }
            set
            {
                //���� ��������� ����� �� �� 0 �� 100
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