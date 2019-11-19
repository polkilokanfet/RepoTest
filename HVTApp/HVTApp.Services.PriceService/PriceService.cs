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
        private readonly List<ProductBlock> _blocks;

        private readonly Dictionary<Guid, ProductBlock> _analogsWithPrice = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnitOfWork unitOfWork)
        {
            _blocks = unitOfWork.Repository<ProductBlock>().Find(x => true);
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
            //���� ��� �������� ������
            if (!block.Prices.Any())
            {
                //���� ������
                var analog = GetAnalogWithPrice(block.Id);
                if (analog == null)
                {
                    errors?.AddError(block, PriceErrorType.NoPrice);
                    return 0;
                }
                errors?.AddError(block, PriceErrorType.PriceOfAnalog, analog);
                block = analog;
            }

            //����� ��������� � ���� �����
            var price = GetClosedSumOnDate(block.Prices, date);

            if (price.Date < date.AddDays(-actualTerm) || price.Date > date.AddDays(actualTerm))
                errors?.AddError(block, PriceErrorType.NoActualPrice);

            return price.Sum;
        }

        /// <summary>
        /// ���������� ��������� � ���� �����.
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
        /// ����� ������� ��� �����.
        /// </summary>
        /// <param name="blockId">Id �������� �����.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(Guid blockId)
        {
            if (_analogsWithPrice.ContainsKey(blockId))
                return _analogsWithPrice[blockId];

            var targetBlock = _blocks.Single(x => x.Id == blockId);
            var blocks = _blocks.Where(x => x.Prices.Any()).ToList();
            blocks.Remove(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var block in blocks)
            {
                double dif = 0;

                //����� ���������
                var intParams = block.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //������������� ���������
                var difParams1 = block.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(block.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                dic.Add(block, dif);
            }

            var blockAnalog = dic.OrderByDescending(x => x.Value).First().Key;
            _analogsWithPrice.Add(blockId, blockAnalog);
            return blockAnalog;
        }

        public PriceStructure GetPriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            return  new PriceStructure(product, amount, targetPriceDate, priceTerm, this.GetAnalogWithPrice);
        }

        public PriceStructures GetPriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm)
        {
            return new PriceStructures(unit, targetPriceDate, priceTerm, this.GetAnalogWithPrice);
        }


    }
}