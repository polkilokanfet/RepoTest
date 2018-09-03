using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        public double GetPrice(Product product, DateTime date, int actualTerm, Dictionary<ProductBlock,string> errorDictionary = null)
        {
            double result = 0;
            foreach (var block in product.GetBlocks())
            {
                string errorMsg = null;
                result += GetPrice(block, date, actualTerm, ref errorMsg);
                if (errorMsg != null && errorDictionary != null && !errorDictionary.ContainsKey(block))
                {
                    errorDictionary.Add(block, errorMsg);
                }
            }
            return result;
        }

        public double GetPrice(ProductBlock block, DateTime date, int actualTerm, ref string errorMsg)
        {
            //поиск какой-либо себестоимости
            if (!block.Prices.Any())
            {
                errorMsg = $"Для '{block}' нет какого-либо прайса).";
                return 0;
            }

            var price = GetClosedSumOnDate(block.Prices, date);

            if (price.Date < date.AddDays(-actualTerm) || price.Date > date.AddDays(actualTerm))
                errorMsg = $"Для '{block}' нет актуального прайса ({date.ToShortDateString()} +- {actualTerm}).";

            return price.Sum;
        }

        private SumOnDate GetClosedSumOnDate(IEnumerable<SumOnDate> sumsOnDates, DateTime date)
        {
            SumOnDate result = null;
            double? currentDif = null;
            foreach (var sumOnDate in sumsOnDates)
            {
                var dif = Math.Abs((sumOnDate.Date - date).TotalDays);
                if (currentDif == null || dif < currentDif)
                {
                    currentDif = dif;
                    result = sumOnDate;
                }
            }
            return result;
        }
    }
}