using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Все блоки
        /// </summary>
        private List<ProductBlock> Blocks { get; set; } = new List<ProductBlock>();

        /// <summary>
        /// Словарь завершенных калькуляций
        /// Какому SalesUnit.Id соответствует какой Расчет ПЗ
        /// </summary>
        private Dictionary<Guid, PriceCalculationItem> PriceCalculationItemsFinished { get; } = new Dictionary<Guid, PriceCalculationItem>();

        /// <summary>
        /// Словарь прайсов по калькуляции
        /// SalesUnit.Id => стоимость
        /// </summary>
        private Dictionary<Guid, double?> PricesOfSalesUnitsFromCalculations { get; } = new Dictionary<Guid, double?>();

        /// <summary>
        /// Словарь блоков с прайсами
        /// </summary>
        private Dictionary<Guid, ProductBlock> AnalogsWithPrice { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnityContainer container)
        {
            _container = container;

            //синхронизация завершения новых расчетов
            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Subscribe(
                calculation =>
                {
                    foreach (var priceCalculationItem in calculation.PriceCalculationItems)
                    {
                        foreach (var salesUnit in priceCalculationItem.SalesUnits)
                        {
                            if (PriceCalculationItemsFinished.ContainsKey(salesUnit.Id))
                            {
                                PriceCalculationItemsFinished.Remove(salesUnit.Id);
                            }

                            if (PricesOfSalesUnitsFromCalculations.ContainsKey(salesUnit.Id))
                            {
                                PricesOfSalesUnitsFromCalculations.Remove(salesUnit.Id);
                            }

                            //добавляем только данные из завершенных расчетов
                            if (calculation.TaskCloseMoment.HasValue)
                            {
                                PriceCalculationItemsFinished.Add(salesUnit.Id, priceCalculationItem);
                                PricesOfSalesUnitsFromCalculations.Add(salesUnit.Id, priceCalculationItem.StructureCosts.Sum(structureCost => structureCost.Total));
                            }
                        }
                    }
                });

            _container.Resolve<IModelsStore>().IsRefreshed += Reload;

            Reload();
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            Blocks = unitOfWork.Repository<ProductBlock>().GetAll();

            PriceCalculationItemsFinished.Clear();
            PricesOfSalesUnitsFromCalculations.Clear();

            //завершенные расчеты ПЗ, упорядоченные по дате завершения
            var priceCalculationsFinished = unitOfWork.Repository<PriceCalculation>()
                .Find(priceCalculation => priceCalculation.TaskCloseMoment.HasValue)
                .OrderBy(priceCalculation => priceCalculation.TaskCloseMoment)
                .ToList();

            //формирование словарей прайсов по калькуляции
            var salesUnits = priceCalculationsFinished
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                .Distinct();
            foreach (var salesUnit in salesUnits)
            {
                var priceCalculationItem = priceCalculationsFinished
                    //.OrderBy(priceCalculation => priceCalculation.TaskCloseMoment)
                    .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                    .Last(calculationItem => calculationItem.SalesUnits.ContainsById(salesUnit));

                PriceCalculationItemsFinished.Add(salesUnit.Id, priceCalculationItem);
                PricesOfSalesUnitsFromCalculations.Add(salesUnit.Id, priceCalculationItem.StructureCosts.Sum(structureCost => structureCost.Total));
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
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="blockTarget">Целевой блок.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(ProductBlock blockTarget)
        {
            if (AnalogsWithPrice.ContainsKey(blockTarget.Id))
                return AnalogsWithPrice[blockTarget.Id];

            var targetBlock = Blocks.SingleOrDefault(x => x.Id == blockTarget.Id) ?? blockTarget;
            var blocksWithPrices = Blocks
                .Where(productBlock => productBlock.Id != targetBlock.Id)
                .Where(productBlock => productBlock.HasPrice).ToList();

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var blockWithPrice in blocksWithPrices)
            {
                double dif = 0;

                //общие параметры
                var intParams = blockWithPrice.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //различающиеся параметры
                var difParams1 = blockWithPrice.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(blockWithPrice.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
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