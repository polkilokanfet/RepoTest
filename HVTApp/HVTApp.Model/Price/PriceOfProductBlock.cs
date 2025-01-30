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
                var q = $"{_averageQuarterSum.Date.Quarter()} ��. {_averageQuarterSum.Date.Year} �.";
                return ContainsAnyAnalog
                    ? $" �� ������� ({q}): {Analog}"
                    : $" �� ��������� ({q})";
            }
        }

        public override string CommentLaborHours =>
            ContainsAnyBlockWithNoLaborHours
                ? "���� ��� �/�."
                : string.Empty;

        public override double SumFixedTotal => this.SumFixed * Amount ?? 0;

        #region Profitability

        /// <summary>
        /// ���������� �����-����� �� ������������ ����� ����� ��������
        /// </summary>
        public override double? LaborHours => _priceService.GetLaborHoursAmount(_productBlock);

        /// <summary>
        /// ���� ������ �����
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
                //������������� �� ������/������������� ����
                Init(productBlock, targetDate);
            }
            else
            {
                //������������� �� �������
                Init(priceService.GetAnalogWithPrice(productBlock), targetDate, productBlock);
            }
        }

        /// <summary>
        /// ������������� �� �������
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        /// <param name="originalBlock"></param>
        private void Init(ProductBlock productBlock, DateTime targetDate, ProductBlock originalBlock)
        {
            Name = $"{originalBlock} (�� �������: {productBlock})";
            Analog = productBlock;
            Init(productBlock, targetDate);
        }

        private ISumOnDate _averageQuarterSum;
        /// <summary>
        /// ������������� �� ������/������������� ����
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        private void Init(ProductBlock productBlock, DateTime targetDate)
        {
            Name = productBlock.ToString();

            //�� ������������� ����
            if (productBlock.HasFixedPrice)
            {
                SumFixed = productBlock.FixedCosts.GetClosedSumOnDate(targetDate).Sum;
                return;
            }

            //�� ������
            if (productBlock.HasPrice)
            {
                _averageQuarterSum = productBlock.Prices.GetAverageQuarterSum(targetDate);
                UnitPrice = _averageQuarterSum.Sum;
            }
        }
    }
}