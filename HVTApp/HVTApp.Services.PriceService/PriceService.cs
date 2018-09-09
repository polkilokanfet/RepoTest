using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
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
            //поиск какой-либо себестоимости
            if (!block.Prices.Any())
            {
                var analog = await GetAnalog(block);
                errors?.AddError(block, PriceErrorType.PriceOfAnalog, analog);
                return 0;
            }

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

        private List<ProductBlock> _blocks;
        /// <summary>
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="targetBlock">Целевой блок.</param>
        /// <returns></returns>
        private async Task<ProductBlock> GetAnalog(ProductBlock targetBlock)
        {
            await LoadAsync();

            var blocks = new List<ProductBlock>(_blocks);
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

            return dic.OrderBy(x => x.Value).First().Key;
        }

        private async Task LoadAsync()
        {
            if (_blocks != null) return;
            _blocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();
            var pars = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            var parsr = await _unitOfWork.GetRepository<ParameterRelation>().GetAllAsync();
        }
    }

    public class PriceErrors
    {
        private readonly Dictionary<ProductBlock, PriceErrorType> _errors = new Dictionary<ProductBlock, PriceErrorType>();
        private readonly Dictionary<ProductBlock, ProductBlock> _analogs = new Dictionary<ProductBlock, ProductBlock>();

        public void AddError(ProductBlock block, PriceErrorType errorType, ProductBlock analog = null)
        {
            if (_errors.ContainsKey(block)) return;
            _errors.Add(block, errorType);
            if (_analogs.ContainsKey(block)) return;
            _analogs.Add(block, analog);
        }

        public string Print()
        {
            return Print(_errors.Select(x => x.Key));
        }

        public string Print(IEnumerable<ProductBlock> blocks)
        {
            var sb = new StringBuilder();
            var errors = _errors.Where(x => blocks.Contains(x.Key)).GroupBy(x => x.Value).ToList();
            foreach (var error in errors)
            {
                switch (error.Key)
                {
                    case PriceErrorType.NoPrice:
                        sb.AppendLine("Блоки без прайса:");
                        break;
                    case PriceErrorType.NoActualPrice:
                        sb.AppendLine("Блоки без актуального прайса:");
                        break;
                    case PriceErrorType.PriceOfAnalog:
                        sb.AppendLine("Блоки с прайсом аналога:");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (error.Key)
                {
                    case PriceErrorType.NoPrice:
                    case PriceErrorType.NoActualPrice:
                        foreach (var er in error)
                        {
                            sb.AppendLine($"{er.Key}");
                        }
                        break;
                    case PriceErrorType.PriceOfAnalog:
                        foreach (var er in error)
                        {
                            sb.AppendLine($"{er.Key} <=> {_analogs[er.Key]}");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendLine(string.Empty);
            return sb.ToString();
        }
    }

    public enum PriceErrorType
    {
        NoPrice,
        NoActualPrice,
        PriceOfAnalog
    }
}