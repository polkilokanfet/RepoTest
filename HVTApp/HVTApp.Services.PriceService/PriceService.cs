using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<double> GetPrice(Product product, DateTime date, int actualTerm, PriceErrors errors = null)
        {
            double result = 0;
            foreach (var block in product.GetBlocks())
            {
                result += await GetPrice(block, date, actualTerm, errors);
            }
            return result;
        }

        public async Task<double> GetPrice(ProductBlock block, DateTime date, int actualTerm, PriceErrors errors = null)
        {
            //если нет никакого прайса
            if (!block.Prices.Any())
            {
                //ищем аналог
                var analog = await GetAnalogWithPrice(block);
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
        private async Task<ProductBlock> GetAnalogWithPrice(ProductBlock targetBlock)
        {
            var blocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();
            targetBlock = blocks.Single(x => x.Id == targetBlock.Id);
            blocks.Remove(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var block in blocks)
            {
                var difParams1 = block.Parameters.Except(targetBlock.Parameters).ToList();
                double dif = difParams1.Sum(param => block.GetWeight(param));

                var difParams2 = targetBlock.Parameters.Except(block.Parameters).ToList();
                dif += difParams2.Sum(param => targetBlock.GetWeight(param));

                dic.Add(block, dif);
            }

            return dic.OrderBy(x => x.Value).First(x => x.Key.Prices.Any()).Key;
        }
    }
}