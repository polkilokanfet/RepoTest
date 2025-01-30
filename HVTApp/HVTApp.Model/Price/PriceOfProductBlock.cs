using System;
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
                var q = $"{_averageQuarterSum.Date.Quarter()} кв. {_averageQuarterSum.Date.Year} г.";
                return ContainsAnyAnalog
                    ? $" ѕ« аналога ({q}): {Analog}"
                    : $" ѕ« оригинала ({q})";
            }
        }

        public override string CommentLaborHours =>
            ContainsAnyBlockWithNoLaborHours
                ? "Ѕлок без н/ч."
                : string.Empty;

        public override double SumFixedTotal => this.SumFixed * Amount ?? 0;

        #region Profitability

        /// <summary>
        ///  оличество нормо-часов на изготовление всего блока продукта
        /// </summary>
        public override double? LaborHours => _priceService.GetLaborHoursAmount(_productBlock);

        /// <summary>
        /// ‘онд оплаты труда
        /// </summary>
        public override double? WageFund => _priceService.GetWageFund(_productBlock, _targetDate);
        
        #endregion

        public PriceOfProductBlock(ProductBlock productBlock, DateTime targetDate, IPriceService priceService, double amount)
        {
            _productBlock = productBlock;
            _targetDate = targetDate;
            _priceService = priceService;

            Amount = amount;

            if (productBlock.HasPrice || productBlock.HasFixedPrice)
            {
                //инициализаци€ по прайсу/фиксированной цене
                Init(productBlock, targetDate);
            }
            else
            {
                //инициализаци€ по аналогу
                Init(priceService.GetAnalogWithPrice(productBlock), targetDate, productBlock);
            }
        }

        /// <summary>
        /// инициализаци€ по аналогу
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        /// <param name="originalBlock"></param>
        private void Init(ProductBlock productBlock, DateTime targetDate, ProductBlock originalBlock)
        {
            Name = $"{originalBlock} (по аналогу: {productBlock})";
            Analog = productBlock;
            Init(productBlock, targetDate);
        }

        private ISumOnDate _averageQuarterSum;
        /// <summary>
        /// инициализаци€ по прайсу/фиксированной цене
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