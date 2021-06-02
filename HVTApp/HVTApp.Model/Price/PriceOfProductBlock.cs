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

        public override string Comment => 
            ContainsAnyAnalog 
                ? $"аналог: {Analog}" 
                : null;

        public override double SumFixedTotal => this.SumFixed * Amount ?? 0;

        #region Profitability

        /// <summary>
        /// Количество нормо-часов на изготовление всего продукта
        /// </summary>
        public override double? LaborHours => _priceService.GetLaborHoursAmount(_productBlock);

        /// <summary>
        /// Количество нормо-часов на изготовление всего продукта * количество
        /// </summary>
        public override double? LaborHoursOnAmount => LaborHours * Amount;

        /// <summary>
        /// Фонд оплаты труда
        /// </summary>
        public override double? WageFund => _priceService.GetWageFund(_productBlock, _targetDate);

        /// <summary>
        /// Фонд оплаты труда * количество
        /// </summary>
        public override double? WageFundOnAmount => WageFund * Amount;


        #endregion

        public PriceOfProductBlock(ProductBlock productBlock, DateTime targetDate, IPriceService priceService, double amount)
        {
            _productBlock = productBlock;
            _targetDate = targetDate;
            _priceService = priceService;

            Amount = amount;

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

            Name = $"PriceOfProductBlock: {Name}";
        }

        private void Init(ProductBlock productBlock, DateTime targetDate, ProductBlock originalBlock)
        {
            Init(productBlock, targetDate);
            Name = $"{originalBlock} (по аналогу: {productBlock})";
            Analog = productBlock;
        }

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
                UnitPrice = productBlock.Prices.GetClosedSumOnDate(targetDate).Sum;
            }
        }
    }
}