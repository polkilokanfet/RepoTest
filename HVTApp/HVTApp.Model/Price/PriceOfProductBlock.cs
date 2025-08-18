using System;
using System.Linq;
using System.Text;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model.Price
{
    public class PriceOfProductBlock : PriceOfUnit
    {
        private readonly ProductBlock _productBlock;
        private readonly DateTime _targetDate;
        private readonly IPriceService _priceService;

        public override bool ContainsAnyAnalog => Analog != null;
        public override bool ContainsAnyBlockWithNoLaborHours
        {
            get
            {
                if (_productBlock.HasFixedPrice)
                    return false;

                return LaborHours == null;
            }
        }

        public override string Comment
        {
            get
            {
                if (_productBlock.StructureCostNumberIsRequired == false)
                    return "Блок без ПЗ";

                if (_averageQuarterSum == null) return string.Empty;
                var q = $"{_averageQuarterSum.Date.Quarter()} кв. {_averageQuarterSum.Date.Year} г.";
                return ContainsAnyAnalog
                    ? $" ПЗ аналога ({q}): {Analog} (Различия:: {GetParameterDifference()})"
                    : $" ПЗ оригинала ({q})";
            }
        }

        string GetParameterDifference()
        {
            if (Analog == null || OriginalBlock == null) return string.Empty;
            var sb = new StringBuilder();
            var parametersAnalog = Analog.Parameters.ToList();
            var parametersOriginal = OriginalBlock.Parameters.ToList();

            foreach (var parameter in parametersAnalog.ToList())
            {
                if (parametersOriginal.Contains(parameter))
                {
                    parametersAnalog.Remove(parameter);
                    parametersOriginal.Remove(parameter);
                    continue;
                }

                var targetParameter = parametersOriginal.SingleOrDefault(x => x.ParameterGroup.Equals(parameter.ParameterGroup));
                if (targetParameter != null)
                {
                    sb.Append($"< {parameter.ParameterGroup.Name}: {parameter.Value} +- {targetParameter.Value} >, ");
                    parametersAnalog.Remove(parameter);
                    parametersOriginal.Remove(targetParameter);
                    continue;
                }
            }

            foreach (var parameter in parametersAnalog)
            {
                sb.Append($"< + {parameter.ParameterGroup.Name}: {parameter.Value} >, ");
            }

            foreach (var parameter in parametersOriginal)
            {
                sb.Append($"< - {parameter.ParameterGroup.Name}: {parameter.Value} >, ");
            }

            return sb.ToString();
        }

        public override string CommentLaborHours =>
            ContainsAnyBlockWithNoLaborHours
                ? "Блок без н/ч."
                : string.Empty;

        public override double SumFixedTotal => this.SumFixed * Amount ?? 0;

        #region Profitability

        /// <summary>
        /// Количество нормо-часов на изготовление всего блока продукта
        /// </summary>
        public override double? LaborHours => _priceService.GetLaborHoursAmount(_productBlock);

        /// <summary>
        /// Фонд оплаты труда
        /// </summary>
        public override double? WageFund => _priceService.GetWageFund(_productBlock, _targetDate);
        
        #endregion

        public PriceOfProductBlock(ProductBlock productBlock, DateTime targetDate, IPriceService priceService, double amount)
        {
            _productBlock = productBlock;
            _targetDate = targetDate;
            _priceService = priceService;

            Amount = amount;

            if (productBlock.StructureCostNumberIsRequired)
            {
                if (productBlock.HasPrice || productBlock.HasFixedPrice)
                {
                    //инициализация по прайсу/фиксированной цене
                    Init(productBlock, targetDate);
                }
                else
                {
                    //инициализация по аналогу
                    Init(priceService.GetAnalogWithPrice(productBlock), targetDate, productBlock);
                }
            }
            else
            {
                Init(targetDate);
            }
        }

        /// <summary>
        /// инициализация без ПЗ
        /// </summary>
        private void Init(DateTime targetDate)
        {
            Name = _productBlock.ToString();
            UnitPrice = 0;

            //по фиксированной цене
            if (_productBlock.HasFixedPrice)
            {
                SumFixed = _productBlock.FixedCosts.GetClosedSumOnDate(targetDate).Sum;
            }

        }

        /// <summary>
        /// инициализация по аналогу
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        /// <param name="originalBlock"></param>
        private void Init(ProductBlock productBlock, DateTime targetDate, ProductBlock originalBlock)
        {
            Name = $"{originalBlock} (по аналогу: {productBlock})";
            OriginalBlock = originalBlock;
            Analog = productBlock;
            Init(productBlock, targetDate);
        }

        private ISumOnDate _averageQuarterSum;
        /// <summary>
        /// инициализация по прайсу/фиксированной цене
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        private void Init(ProductBlock productBlock, DateTime targetDate)
        {
            Name = productBlock.ToString();

            //по фиксированной цене
            if (productBlock.HasFixedPrice)
            {
                SumFixed = productBlock.FixedCosts.GetClosedSumOnDate(targetDate).Sum;
                return;
            }

            //по прайсу
            if (productBlock.HasPrice)
            {
                _averageQuarterSum = productBlock.Prices.GetAverageQuarterSum(targetDate);
                UnitPrice = _averageQuarterSum.Sum;
            }
        }
    }
}