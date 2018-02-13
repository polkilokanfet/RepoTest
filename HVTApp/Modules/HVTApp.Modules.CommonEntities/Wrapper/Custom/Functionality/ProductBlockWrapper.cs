using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductBlockWrapper
    {
        public bool HasActualPriceOnDate(DateTime date)
        {
            return Prices.Any(x => x.Date >= date.AddDays(-90));
        }

        public double GetPrice(DateTime date)
        {
            //ближайшая актуальная цена
            var price = Prices.Where(x => x.Date >= date.AddDays(-90)).OrderBy(x => x.Date).LastOrDefault();
            if (price != null) return price.Cost;

            //ближайшая цена
            price = Prices.FirstOrDefault();
            foreach (var costOnDate in Prices)
            {
                if (Math.Abs(date.Ticks - costOnDate.Date.Ticks) < Math.Abs(date.Ticks - price.Date.Ticks))
                    price = costOnDate;
            }

            return price?.Cost ?? 0;
        }
    }
}