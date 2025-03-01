using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitCalculatedParts : BindableBase
    {
        private readonly IProjectUnit _projectUnit;


        private double CostDelivery
        {
            get
            {
                double result = 0;
                if (_projectUnit.CostDelivery.HasValue)
                    result = _projectUnit.CostDelivery.Value;

                if (_projectUnit is ProjectUnitGroup projectUnitGroup)
                    result /= projectUnitGroup.Amount;

                return result;
            }
        }

        /// <summary>
        /// ���������� ��������� ���� ������� ������������ (�������� � ������������� ����� + ��������� ��������)
        /// </summary>
        private double CostMin => _projectUnit.Price.SumFixedTotal + CostDelivery;

        public double? MarginalIncome
        {
            get => _projectUnit.Cost - CostMin <= 0
                ? default(double?)
                : (1.0 - _projectUnit.Price.SumPriceTotal / (_projectUnit.Cost - CostMin)) * 100.0;
            set
            {
                if (value.HasValue == false || value >= 100) 
                    return;

                var marginalIncome = value.Value;
                _projectUnit.Cost = _projectUnit.Price.SumPriceTotal / (1.0 - marginalIncome / 100.0) + CostMin;
                RaisePropertyChanged();
            }
        }

        public ProjectUnitCalculatedParts(IProjectUnit projectUnit)
        {
            _projectUnit = projectUnit;
        }
    }
}