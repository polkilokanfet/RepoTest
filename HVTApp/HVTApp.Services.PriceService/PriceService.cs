using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        /// <summary>
        /// ¬се блоки
        /// </summary>
        private List<ProductBlock> Blocks { get; }

        /// <summary>
        /// —ловарь завершенных калькул€ций
        /// </summary>
        private Dictionary<Guid, PriceCalculationItem> PriceCalculationItemsFinished { get; } = new Dictionary<Guid, PriceCalculationItem>();

        /// <summary>
        /// —ловарь прайсов по калькул€ции
        /// SalesUnit.Id => стоимость
        /// </summary>
        private Dictionary<Guid, double?> PricesOfSalesUnitsFromCalculations { get; } = new Dictionary<Guid, double?>();

        /// <summary>
        /// —ловарь блоков с прайсами
        /// </summary>
        private Dictionary<Guid, ProductBlock> AnalogsWithPrice { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnitOfWork unitOfWork)
        {
            Blocks = unitOfWork.Repository<ProductBlock>().GetAll();

            var priceCalculationsFinished = unitOfWork.Repository<PriceCalculation>()
                .Find(x => x.TaskCloseMoment.HasValue)
                .OrderBy(x => x.TaskCloseMoment).ToList();

            //формирование словарей прайсов по калькул€ции
            foreach (var salesUnit in priceCalculationsFinished.SelectMany(x => x.PriceCalculationItems).SelectMany(x => x.SalesUnits).Distinct())
            {
                var priceCalculationItem =
                    priceCalculationsFinished
                        .OrderBy(x => x.TaskCloseMoment)
                        .SelectMany(x => x.PriceCalculationItems)
                        .LastOrDefault(x => x.SalesUnits.ContainsById(salesUnit));

                PriceCalculationItemsFinished.Add(salesUnit.Id, priceCalculationItem);
                PricesOfSalesUnitsFromCalculations.Add(salesUnit.Id, priceCalculationItem.StructureCosts.Sum(x => x.Total));
            }
        }

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            return PriceCalculationItemsFinished.ContainsKey(unit.Id)
                ? PriceCalculationItemsFinished[unit.Id]
                : null;
        }

        public double? GetPriceByCalculations(IUnit unit)
        {
            return PricesOfSalesUnitsFromCalculations.ContainsKey(unit.Id) 
                ? PricesOfSalesUnitsFromCalculations[unit.Id] 
                : null;
        }

        public Price GetPrice(IUnit unit, DateTime targetDate)
        {
            return new Price(unit, targetDate, this);
        }

        /// <summary>
        /// ѕоиск аналога дл€ блока.
        /// </summary>
        /// <param name="blockTarget">÷елевой блок.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(ProductBlock blockTarget)
        {
            if (AnalogsWithPrice.ContainsKey(blockTarget.Id))
                return AnalogsWithPrice[blockTarget.Id];

            var targetBlock = Blocks.SingleOrDefault(x => x.Id == blockTarget.Id) ?? blockTarget;
            var blocksWithPrices = Blocks
                .Where(x => x.Id != targetBlock.Id)
                .Where(x => x.HasPrice).ToList();

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var blockWithPrice in blocksWithPrices)
            {
                double dif = 0;

                //общие параметры
                var intParams = blockWithPrice.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //различающиес€ параметры
                var difParams1 = blockWithPrice.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(blockWithPrice.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                dic.Add(blockWithPrice, dif);
            }

            var blockAnalog = dic.OrderByDescending(x => x.Value).First().Key;
            AnalogsWithPrice.Add(blockTarget.Id, blockAnalog);
            return blockAnalog;
        }
    }
}