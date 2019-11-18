using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Structures;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<ProductBlock> _blocks;

        public PriceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _blocks = _unitOfWork.Repository<ProductBlock>().Find(x => true);
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

            return price.Sum;
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
        /// <param name="targetBlock">Целевой блок.</param>
        /// <returns></returns>
        private ProductBlock GetAnalogWithPrice(ProductBlock targetBlock)
        {
            targetBlock = _blocks.Single(x => x.Id == targetBlock.Id);
            _blocks.Remove(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var block in _blocks)
            {
                var difParams1 = block.Parameters.Except(targetBlock.Parameters).ToList();
                double dif = difParams1.Sum(param => param.GetWeight(block));

                var difParams2 = targetBlock.Parameters.Except(block.Parameters).ToList();
                dif += difParams2.Sum(param => param.GetWeight(targetBlock));

                dic.Add(block, dif);
            }

            return dic.OrderBy(x => x.Value).First(x => x.Key.Prices.Any()).Key;
        }

        public PriceStructure GetPriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            return  new PriceStructure(product, amount, targetPriceDate, priceTerm, _blocks);
        }

        public PriceStructures GetPriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm)
        {
            return new PriceStructures(unit, targetPriceDate, priceTerm, _blocks);
        }
    }
}