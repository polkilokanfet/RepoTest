using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Structures;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        /// <summary>
        /// Коэффициент упаковки
        /// "гениальное изобретение фин.отдела"
        /// </summary>
        const double _kUp = 1.03;

        private readonly List<ProductBlock> _blocks;
        private readonly List<PriceCalculation> _priceCalculations;

        /// <summary>
        /// Словарь блоков с прайсами
        /// </summary>
        private readonly Dictionary<Guid, ProductBlock> _analogsWithPrice = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnitOfWork unitOfWork)
        {
            _blocks = unitOfWork.Repository<ProductBlock>().GetAll();
            _priceCalculations = unitOfWork.Repository<PriceCalculation>().Find(x => x.TaskCloseMoment.HasValue);
        }

        public double GetPrice(Product product, DateTime date, int actualTerm, PriceErrors errors = null)
        {
            double result = 0;
            foreach (var block in product.GetBlocks())
            {
                result += GetPrice(block, date, actualTerm, errors);
            }
            return result;
        }

        public double GetPrice(ProductBlock block, DateTime date, int actualTerm, PriceErrors errors = null)
        {
            //если нет никакого прайса
            if (!block.Prices.Any())
            {
                //ищем аналог
                var analog = GetAnalogWithPrice(block);
                if (analog == null)
                {
                    errors?.AddError(block, PriceErrorType.NoPrice);
                    return 0;
                }
                errors?.AddError(block, PriceErrorType.PriceOfAnalog, analog);
                block = analog;
            }

            //поиск ближайшей к дате суммы
            var price = GetClosedSumOnDate(block.Prices, date);

            if (price.Date < date.AddDays(-actualTerm) || price.Date > date.AddDays(actualTerm))
                errors?.AddError(block, PriceErrorType.NoActualPrice);

            return price.Sum * _kUp;
        }

        /// <summary>
        /// Возвращает ближайшую к дате сумму.
        /// </summary>
        /// <param name="sumsOnDates"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private SumOnDate GetClosedSumOnDate(IEnumerable<SumOnDate> sumsOnDates, DateTime date)
        {
            var dif = sumsOnDates.Select(x => Math.Abs((x.Date - date).Days)).Min();
            return sumsOnDates.First(x => x.Date == date.AddDays(-dif) || x.Date == date.AddDays(dif));
            //SumOnDate result = null;
            //double? currentDif = null;
            //foreach (var sumOnDate in sumsOnDates)
            //{
            //    var dif = Math.Abs((sumOnDate.Date - date).TotalDays);
            //    if (currentDif == null || dif < currentDif)
            //    {
            //        currentDif = dif;
            //        result = sumOnDate;
            //    }
            //}
            //return result;
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

        public PriceStructure GetPriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm)
        {
            var priceStructure = new PriceStructure(product, amount, targetPriceDate, priceTerm);
            if (priceStructure.IsAnalogPrice)
                priceStructure.Analog = GetAnalogWithPrice(product.ProductBlock);

            //добавляем дочерние структуры
            foreach (var dependentProduct in product.DependentProducts)
            {
                var dePriceStructure = GetPriceStructure(dependentProduct.Product, dependentProduct.Amount, targetPriceDate, priceTerm);
                priceStructure.DependentProductsPriceStructures.Add(dePriceStructure);
            }

            return priceStructure;
        }

        public PriceStructures GetPriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm)
        {
            //структура себестоимости продукта
            var priceStructures = new PriceStructures
            {
                this.GetPriceStructure(unit.Product, 1, targetPriceDate, priceTerm)
            };

            //структура себестоимости включенных продуктов
            foreach (var productIncluded in unit.ProductsIncluded)
            {
                double count = (double)productIncluded.Amount / productIncluded.ParentsCount;
                priceStructures.Add(GetPriceStructure(productIncluded.Product, count, targetPriceDate, priceTerm));
            }

            return priceStructures;
        }

        public double? GetPrice(SalesUnit salesUnit)
        {
            if(salesUnit == null)
                throw new ArgumentNullException(nameof(salesUnit));

            //по проставленному прайсу
            if (salesUnit.Price.HasValue) 
                return salesUnit.Price.Value * _kUp;

            //по расчетам себестоимости
            var priceCalculationItem =
                _priceCalculations
                    .OrderByDescending(x => x.TaskCloseMoment)
                    .SelectMany(x => x.PriceCalculationItems)
                    .FirstOrDefault(x => x.SalesUnits.ContainsById(salesUnit));

            return priceCalculationItem?.StructureCosts.Sum(x => x.Total) * _kUp;
        }

    }
}