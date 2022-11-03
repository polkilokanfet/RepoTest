using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnityContainer _container;
        private Dictionary<Guid, ProductBlock> _blocks = null;
        private Dictionary<Guid, PriceItems> _salesUnitsCalculationsDictionary = null;

        /// <summary>
        /// ��� �����
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
        /// ��� ����� � ��
        /// </summary>
        private Dictionary<Guid, ProductBlock> ProductBlocksWithPriceDictionary { get; set; }

        /// <summary>
        /// ������� ����������� ��
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
        /// ������� ������-�������� � ��������
        /// </summary>
        private Dictionary<Guid, ProductBlock> ProductBlocksAnalogsDictionary { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnityContainer container)
        {
            _container = container;

            //������������� ���������� ��������
            container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Subscribe(
                calculation =>
                {
                    if (this.SalesUnitsCalculationsDictionary == null) return;

                    //��������� ������ ������ �� ����������� ��������
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

            //������������� ��������� ��������
            container.Resolve<IEventAggregator>().GetEvent<AfterStopPriceCalculationEvent>().Subscribe(
                calculation =>
                {
                    if (this.SalesUnitsCalculationsDictionary == null) return;

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

#if DEBUG
#else
            //���� ������������ - ��������, ������ ������ �����
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
                Reload();
#endif
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            LaborHoursList = unitOfWork.Repository<LaborHours>().GetAll();
            LaborHourCosts = unitOfWork.Repository<LaborHourCost>().GetAll();

            var blocksAll = unitOfWork.Repository<ProductBlock>().GetAll();
            AllProductBlocksDictionary = blocksAll.ToDictionary(block => block.Id);
            ProductBlocksWithPriceDictionary = blocksAll.Where(x => x.HasPrice).ToDictionary(block => block.Id);

            SalesUnitsCalculationsDictionary = new Dictionary<Guid, PriceItems>();

            //����������� ������� ��, ������������� �� ���� ����������
            var priceCalculationsFinished = unitOfWork.Repository<PriceCalculation>()
                .Find(priceCalculation => priceCalculation.TaskCloseMoment.HasValue)
                .OrderBy(priceCalculation => priceCalculation.TaskCloseMoment)
                .ToList();

            //����������� ������ �������� ��
            var priceCalculationItemsFinished = priceCalculationsFinished
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .Distinct()
                .ToList();

            //������������ �������� ������� �� �����������
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
        /// ����� ������� ��� �����.
        /// </summary>
        /// <param name="blockTarget">������� ����.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(ProductBlock blockTarget)
        {
            if (ProductBlocksAnalogsDictionary.ContainsKey(blockTarget.Id))
                return ProductBlocksAnalogsDictionary[blockTarget.Id];

            var targetBlock = AllProductBlocksDictionary.ContainsKey(blockTarget.Id)
                ? AllProductBlocksDictionary[blockTarget.Id]
                : blockTarget;

            //��� ����� � �������
            var blocksWithPrices = new Dictionary<Guid, ProductBlock>(ProductBlocksWithPriceDictionary);
            if (blocksWithPrices.ContainsKey(targetBlock.Id))
            {
                blocksWithPrices.Remove(targetBlock.Id);
            }

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var blockWithPrice in blocksWithPrices.Select(x => x.Value))
            {
                double dif = 0;

                //����� ���������
                var intParams = blockWithPrice.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //������������� ���������
                var difParams1 = blockWithPrice.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(blockWithPrice.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
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