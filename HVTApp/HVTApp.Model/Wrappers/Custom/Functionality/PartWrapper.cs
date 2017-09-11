using System;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class PartWrapper
    {
        public bool HasSameParameters(PartWrapper partItem)
        {
            if (partItem == null) throw new ArgumentNullException();
            return !this.Parameters.Except(partItem.Parameters).Any();
        }

        //—ебестоимость по дате
        public double GetPrice(DateTime? date = null)
        {
            DateTime targetDate = date ?? DateTime.Today;
            var prices = Prices.Where(x => x.Date <= targetDate).OrderBy(x => x.Date);
            if (!prices.Any()) throw new ArgumentException("Ќет себистоимости дл€ этой даты (или дл€ более ранней даты)");
            return prices.Last().Cost;
        }
    }
}