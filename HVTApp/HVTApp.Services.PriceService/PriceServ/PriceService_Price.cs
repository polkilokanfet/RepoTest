using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.PriceService.PriceServ
{
    public partial class PriceService : IPriceService
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Все блоки
        /// </summary>
        private List<ProductBlock> Blocks { get; set; } = new List<ProductBlock>();

        /// <summary>
        /// Словарь калькуляций ПЗ
        /// </summary>
        private Dictionary<Guid, PriceItems> SalesUnitsCalculationsDictionary { get; } = new Dictionary<Guid, PriceItems>();

        /// <summary>
        /// Словарь блоков с прайсами
        /// </summary>
        private Dictionary<Guid, ProductBlock> AnalogsWithPrice { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnityContainer container)
        {
            _container = container;

            //синхронизация завершения расчетов
            container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Subscribe(
                calculation =>
                {
                    //добавляем только данные из завершенных расчетов
                    if (!calculation.TaskCloseMoment.HasValue) return;

                    foreach (var priceCalculationItem in calculation.PriceCalculationItems)
                    {
                        foreach (var salesUnit in priceCalculationItem.SalesUnits)
                        {
                            var priceItem = new PriceItem(calculation, priceCalculationItem);

                            if (SalesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id))
                            {
                                SalesUnitsCalculationsDictionary[salesUnit.Id].Add(priceItem);
                            }
                            else
                            {
                                SalesUnitsCalculationsDictionary.Add(salesUnit.Id, new PriceItems(new[] { priceItem }));
                            }
                        }
                    }
                });

            //синхронизация остановки расчетов
            container.Resolve<IEventAggregator>().GetEvent<AfterStopPriceCalculationEvent>().Subscribe(
                calculation =>
                {
                    if (calculation.TaskCloseMoment.HasValue) return;

                    foreach (var priceCalculationItem in calculation.PriceCalculationItems)
                    {
                        foreach (var salesUnit in priceCalculationItem.SalesUnits)
                        {
                            if (SalesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id))
                            {
                                var priceItems = SalesUnitsCalculationsDictionary[salesUnit.Id];

                                if (priceItems.Remove(priceCalculationItem) && priceItems.IsEmpty)
                                {
                                    SalesUnitsCalculationsDictionary.Remove(salesUnit.Id);
                                }
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

            SalesUnitsCalculationsDictionary.Clear();

            //завершенные расчеты ПЗ, упорядоченные по дате завершения
            var priceCalculationsFinished = unitOfWork.Repository<PriceCalculation>()
                .Find(priceCalculation => priceCalculation.TaskCloseMoment.HasValue)
                .OrderBy(priceCalculation => priceCalculation.TaskCloseMoment)
                .ToList();

            //завершенные айтемы расчетов ПЗ
            var priceCalculationItemsFinished = priceCalculationsFinished
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .Distinct()
                .ToList();

            //формирование словарей прайсов по калькуляции
            var salesUnits = priceCalculationsFinished
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                .Distinct();

            foreach (var salesUnit in salesUnits)
            {
                var priceItems = priceCalculationItemsFinished
                    .Where(calculationItem => calculationItem.SalesUnits.ContainsById(salesUnit))
                    .Select(item => new PriceItem(priceCalculationsFinished.Single(calculation => calculation.PriceCalculationItems.Contains(item)), item));
                SalesUnitsCalculationsDictionary.Add(salesUnit.Id, new PriceItems(priceItems));
            }

            LaborHoursList = unitOfWork.Repository<LaborHours>().GetAll();
            LaborHourCosts = unitOfWork.Repository<LaborHourCost>().GetAll();
        }

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            return this.SalesUnitsCalculationsDictionary.ContainsKey(unit.Id)
                ? SalesUnitsCalculationsDictionary[unit.Id].ActualPriceCalculationItem
                : null;
        }

        public double? GetPriceByCalculations(IUnit unit)
        {
            return SalesUnitsCalculationsDictionary.ContainsKey(unit.Id)
                ? SalesUnitsCalculationsDictionary[unit.Id].Price
                : null;
        }

        public Price GetPrice(IUnit unit, DateTime targetDate, bool checkCalculations)
        {
            return new Price(unit, targetDate, this, checkCalculations);
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

            var targetBlock = Blocks.SingleOrDefault(block => block.Id == blockTarget.Id) ?? blockTarget;
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