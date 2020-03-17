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
        private readonly List<ProductBlock> _blocks;
        private readonly List<PriceCalculation> _priceCalculations;

        /// <summary>
        /// Словарь блоков с прайсами
        /// </summary>
        private readonly Dictionary<Guid, ProductBlock> _analogsWithPrice = new Dictionary<Guid, ProductBlock>();

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit)
        {
            if (unit == null) throw new ArgumentNullException(nameof(unit));

            //по расчетам себестоимости
            return _priceCalculations
                .OrderByDescending(x => x.TaskCloseMoment)
                .SelectMany(x => x.PriceCalculationItems)
                .FirstOrDefault(x => x.SalesUnits.ContainsById(unit));
        }

        public double? GetPriceByCalculations(IUnit unit)
        {
            //по проставленному прайсу
            //if (unit.Price.HasValue) 
            //    return unit.Price.Value * _kUp;

            return GetPriceCalculationItem(unit)?.StructureCosts.Sum(x => x.Total);
        }

        public Price GetPrice(IUnit unit, DateTime targetDate)
        {
            return new Price(unit, targetDate, this);
        }

        public PriceService(IUnitOfWork unitOfWork)
        {
            _blocks = unitOfWork.Repository<ProductBlock>().GetAll();
            _priceCalculations = unitOfWork.Repository<PriceCalculation>().Find(x => x.TaskCloseMoment.HasValue);
        }

        /// <summary>
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="blockTarget">Целевой блок.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(ProductBlock blockTarget)
        {
            if (_analogsWithPrice.ContainsKey(blockTarget.Id))
                return _analogsWithPrice[blockTarget.Id];

            var targetBlock = _blocks.SingleOrDefault(x => x.Id == blockTarget.Id) ?? blockTarget;
            var blocks = _blocks.Where(x => x.Prices.Any()).ToList();
            blocks.Remove(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var block in blocks)
            {
                double dif = 0;

                //общие параметры
                var intParams = block.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //различающиеся параметры
                var difParams1 = block.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(block.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                dic.Add(block, dif);
            }

            var blockAnalog = dic.OrderByDescending(x => x.Value).First().Key;
            _analogsWithPrice.Add(blockTarget.Id, blockAnalog);
            return blockAnalog;
        }
    }
}