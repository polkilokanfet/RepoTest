using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductBlockWrapper
    {
        public double GetPrice(ref List<Price> prices, DateTime date)
        {
            var price = Prices.Where(x => x.Date <= date).OrderBy(x => x.Date).LastOrDefault()?.Model;
            prices.Add(new Price(this.Model, price, date));
            return price?.Cost ?? 0;
        }
        
    }
}