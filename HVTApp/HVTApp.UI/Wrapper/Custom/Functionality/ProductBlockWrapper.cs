using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductBlockWrapper
    {
        public bool HasActualPriceOnDate(DateTime date)
        {
            var actualTerm = CommonOptions.ActualPriceTerm;
            return Prices.Any(x => x.Date >= date.AddDays(-actualTerm));
        }

        public double GetPrice(DateTime date)
        {
            //ближайшая актуальная цена
            var actualTerm = CommonOptions.ActualPriceTerm;
            var price = Prices.Where(x => x.Date >= date.AddDays(-actualTerm)).OrderBy(x => x.Date).LastOrDefault();
            //if (price != null) return price.Cost;

            //ближайшая цена
            price = Prices.FirstOrDefault();
            foreach (var costOnDate in Prices)
            {
                if (Math.Abs(date.Ticks - costOnDate.Date.Ticks) < Math.Abs(date.Ticks - price.Date.Ticks))
                    price = costOnDate;
            }

            //return price?.Cost ?? 0;
            return 0;
        }
    }
}