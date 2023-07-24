using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
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
        /// <summary>
        /// Сервис загружен хотя бы раз
        /// </summary>
        bool _isLoaded = false;

        private readonly IUnityContainer _container;
        private Dictionary<Guid, ProductBlock> _blocks = null;
        private Dictionary<Guid, PriceItems> _salesUnitsCalculationsDictionary = null;

        /// <summary>
        /// Все блоки
        /// </summary>
        private Dictionary<Guid, ProductBlock> AllProductBlocksDictionary
        {
            get
            {
                if (_blocks == null) Reload();
                return _blocks;
            }
            set => _blocks = value;
        }

        /// <summary>
        /// Все блоки с ПЗ
        /// </summary>
        private Dictionary<Guid, ProductBlock> ProductBlocksWithPriceDictionary { get; set; }

        /// <summary>
        /// Словарь калькуляций ПЗ
        /// </summary>
        private Dictionary<Guid, PriceItems> SalesUnitsCalculationsDictionary
        {
            get
            {
                if (_salesUnitsCalculationsDictionary == null) Reload();
                return _salesUnitsCalculationsDictionary;
            }
            set => _salesUnitsCalculationsDictionary = value;
        }

        /// <summary>
        /// Словарь блоков-аналогов с прайсами
        /// </summary>
        private Dictionary<Guid, ProductBlock> ProductBlocksAnalogsDictionary { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnityContainer container)
        {
            _container = container;
            _container.Resolve<IModelsStore>().IsRefreshed += Reload;

#if DEBUG
#else
            //если пользователь - менеджер, грузим сервис сразу
            if (GlobalAppProperties.UserIsManager) Reload();
#endif
        }

        private void AddPriceItemInSalesUnitsCalculationsDictionary(
            PriceCalculationItem priceCalculationItem, 
            SalesUnit salesUnit)
        {
            if (SalesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id))
            {
                SalesUnitsCalculationsDictionary[salesUnit.Id].Add(priceCalculationItem);
            }
            else
            {
                SalesUnitsCalculationsDictionary.Add(salesUnit.Id, new PriceItems(new[] {priceCalculationItem}));
            }
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            LaborHoursList = unitOfWork.Repository<LaborHours>().GetAll();
            LaborHourCosts = unitOfWork.Repository<LaborHourCost>().GetAll();

            var blocksAll = unitOfWork.Repository<ProductBlock>().GetAll();
            AllProductBlocksDictionary = blocksAll.ToDictionary(block => block.Id);
            ProductBlocksWithPriceDictionary = blocksAll.Where(block => block.HasPrice).ToDictionary(block => block.Id);

            SalesUnitsCalculationsDictionary = new Dictionary<Guid, PriceItems>();

            //завершенные айтемы расчетов ПЗ
            var user = GlobalAppProperties.UserIsManager ? GlobalAppProperties.User : null;
            var priceCalculationItemsFinished = ((PriceCalculationRepository)unitOfWork.Repository<PriceCalculation>()).GetCalculationsForPriceService(user).ToList();

            foreach (var priceCalculationItem in priceCalculationItemsFinished)
            {
                foreach (var salesUnit in priceCalculationItem.SalesUnits)
                {
                    if (SalesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id))
                    {
                        SalesUnitsCalculationsDictionary[salesUnit.Id].Add(priceCalculationItem);
                    }
                    else
                    {
                        SalesUnitsCalculationsDictionary.Add(salesUnit.Id, new PriceItems(new []{priceCalculationItem}));
                    }
                }
            }

            //если сервис грузится в первый раз, подписываемся на изменения в расчётах
            if (_isLoaded == false)
            {
                //синхронизация завершения расчетов
                _container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Subscribe(
                    priceCalculation =>
                    {
                        //добавляем только данные из завершенных расчетов
                        if (priceCalculation.IsFinished == false) return;

                        foreach (var priceCalculationItem in priceCalculation.PriceCalculationItems)
                        {
                            foreach (var salesUnit in priceCalculationItem.SalesUnits)
                            {
                                AddPriceItemInSalesUnitsCalculationsDictionary(priceCalculationItem, salesUnit);
                            }
                        }
                    });

                //синхронизация остановки расчетов
                _container.Resolve<IEventAggregator>().GetEvent<AfterStopPriceCalculationEvent>().Subscribe(
                    calculation =>
                    {
                        if (calculation.IsFinished) return;

                        foreach (var priceCalculationItem in calculation.PriceCalculationItems)
                        {
                            foreach (var salesUnit in priceCalculationItem.SalesUnits)
                            {
                                if (SalesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id) == false) continue;

                                var priceItems = SalesUnitsCalculationsDictionary[salesUnit.Id];
                                priceItems.Remove(priceCalculationItem);
                                if (priceItems.IsEmpty) SalesUnitsCalculationsDictionary.Remove(salesUnit.Id);
                            }
                        }
                    });
            }

            this._isLoaded = true;
        }

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            return this.SalesUnitsCalculationsDictionary.ContainsKey(unit.Id)
                ? SalesUnitsCalculationsDictionary[unit.Id].ActualPriceCalculationItem
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
            if (ProductBlocksAnalogsDictionary.ContainsKey(blockTarget.Id))
                return ProductBlocksAnalogsDictionary[blockTarget.Id];

            var targetBlock = AllProductBlocksDictionary.ContainsKey(blockTarget.Id)
                ? AllProductBlocksDictionary[blockTarget.Id]
                : blockTarget;

            //все блоки с прайсом
            var blocksWithPrices = new Dictionary<Guid, ProductBlock>(ProductBlocksWithPriceDictionary);
            blocksWithPrices.RemoveIfContainsById(targetBlock.Id);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var blockWithPrice in blocksWithPrices.Select(x => x.Value))
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
            ProductBlocksAnalogsDictionary.Add(blockTarget.Id, blockAnalog);
            return blockAnalog;
        }
    }
}