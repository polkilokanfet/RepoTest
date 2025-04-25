using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model.Price
{
    public class Price : PriceBase
    {
        private PriceCalculationItem _priceCalculationItem;

        /// <summary>
        /// ������� �����������
        /// </summary>
        public bool HasCalculation { get; protected set; } = false;

        public override bool ContainsAnyAnalog => Prices.Any(price => price.ContainsAnyAnalog);
        public override bool ContainsAnyBlockWithNoLaborHours => Prices.Any(price => price.ContainsAnyBlockWithNoLaborHours);

        public override string Comment
        {
            get
            {
                if (HasCalculation) return $"�� ����������� �� {_priceCalculationItem?.FinishDate?.ToShortDateString()} (���������� {_priceCalculationItem?.RealizationDate.ToShortDateString()})";
                if (ContainsAnyAnalog) return "�������� �� �� ��������";
                return "�� ������������ ������";
            }
        }

        public override string CommentLaborHours =>
            ContainsAnyBlockWithNoLaborHours
                ? "�������� ����� ��� �/�."
                : string.Empty;

        /// <summary>
        /// ������������� � ������ ������������ ��������
        /// </summary>
        public override double SumPriceTotal
        {
            get
            {
                var sumPriceMainBlock = PriceMainBlock?.SumPriceTotal ?? 0;
                if (HasCalculation)
                {
                    return sumPriceMainBlock
                           + PricesOfDependentBlocks.Sum(price => price.SumPriceTotal) //��� ��������, ����� ���� �����������
                           + PricesProductsIncluded.Sum(price => price.SumPriceTotal);
                }
                return sumPriceMainBlock
                       + PricesProductsIncluded.Sum(price => price.SumPriceTotal);
            }
        }

        /// <summary>
        /// ��������� ������ � ������������� �����
        /// </summary>
        public override double SumFixedTotal
        {
            get
            {
                if (SumFixed.HasValue)
                    return SumFixed.Value * Amount;

                var sumPriceMainBlock = PriceMainBlock?.SumFixedTotal ?? 0;

                return sumPriceMainBlock 
                    + PricesOfDependentBlocks.Sum(price => price.SumFixedTotal) 
                    + PricesProductsIncluded.Sum(price => price.SumFixedTotal);
            }
        }

        public override double? LaborHours
        {
            get
            {
                if (HasCalculation)
                {
                    return PriceMainBlock.LaborHoursTotal
                           + PricesOfDependentBlocks.Sum(price => price.LaborHoursTotal)
                           + PricesProductsIncluded.Sum(price => price.LaborHoursTotal);
                }

                return PriceMainBlock.LaborHoursTotal
                       + PricesProductsIncluded.Sum(price => price.LaborHoursTotal);
            }
        }

        /// <summary>
        /// ����� �������� �����
        /// </summary>
        public IPrice PriceMainBlock { get; }

        /// <summary>
        /// ������ ��������� ������
        /// </summary>
        public List<IPrice> PricesOfDependentBlocks { get; } = new List<IPrice>();

        /// <summary>
        /// ������ ����������� ������������
        /// </summary>
        public List<IPrice> PricesProductsIncluded { get; } = new List<IPrice>();

        public override List<IPrice> Prices
        {
            get
            {
                var result = PricesProductsIncluded.ToList();
                
                //���� ���� �����������
                if (HasCalculation)
                {
                    result.Add(new PriceGroup(PriceMainBlock.Name, PricesOfDependentBlocks));
                }
                //���� � ���
                else
                {
                    if (PriceMainBlock != null)
                        result.Add(PriceMainBlock);
                }

                return result.OrderByDescending(price => price.SumTotal).ToList();
            }
        }

        public Price(IEnumerable<IUnit> units, DateTime targetDate, IPriceService priceService, bool checkCalculations)
        {
            var unitsArray = units as IUnit[] ?? units.ToArray();
            var unit = unitsArray.First();
            var unitsAmount = unitsArray.Length;

            Name = unit.Product.ToString();
            var productsIncludedGroups = unitsArray
                .SelectMany(unit1 => unit1.ProductsIncluded)
                .Distinct()
                .GroupBy(productIncluded => new { productIncluded.Product.Id, productIncluded.Amount });

            //���� ���� �����������
            _priceCalculationItem = checkCalculations
                ? priceService.GetPriceCalculationItem(unit)
                : null;
            if (_priceCalculationItem != null)
            {
                HasCalculation = true;

                //�������� �� ����� ��������� �����
                PriceMainBlock = new PriceStub(unit.Product.ToString(), 1, laborHours: priceService.GetLaborHoursAmount(unit));

                //�������� �� ������ ��������� ������
                PricesOfDependentBlocks =
                    _priceCalculationItem.StructureCosts
                        .Where(structureCost => structureCost.UnitPrice.HasValue)
                        .Select(structureCost => new PriceStub($"{structureCost.Comment}", structureCost.Amount, structureCost.UnitPrice.Value, structureCost.Number))
                        .Cast<IPrice>()
                        .ToList();

                //��������� ���������� ������������ ������ � ������������� ����� (����. ���-������)
                productsIncludedGroups = productsIncludedGroups.Where(productIncluded => productIncluded.First().CustomFixedPrice.HasValue || productIncluded.First().Product.HasBlockWithFixedCost);
            }
            //���� ����������� ���, ����� ������������ �� ��������
            else
            {
                Amount = 1;
                PriceMainBlock = new PriceOfProduct(unit.Product, targetDate, priceService, Amount);
                foreach (var dependentProduct in unit.Product.DependentProducts)
                {
                    PricesOfDependentBlocks.Add(new PriceOfProduct(dependentProduct.Product, targetDate, priceService, dependentProduct.Amount));
                }
            }

            //���������� ������������
            foreach (var productIncludedGroup in productsIncludedGroups)
            {
                var productIncluded = productIncludedGroup.First();
                var price = new PriceOfProduct(productIncluded.Product, targetDate, priceService, productIncludedGroup.Sum(x => (double)x.Amount) * productIncludedGroup.Count() / unitsAmount, productIncluded.CustomFixedPrice);
                PricesProductsIncluded.Add(price);
            }
        }

        public Price(IUnit unit, DateTime targetDate, IPriceService priceService, bool checkCalculations) : 
            this(new []{unit}, targetDate, priceService, checkCalculations)
        {
        }
    }
}