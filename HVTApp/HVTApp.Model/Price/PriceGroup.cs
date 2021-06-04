using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Price
{
    public class PriceGroup : PriceBase
    {
        public override bool ContainsAnyAnalog => Prices.Any(price => price.ContainsAnyAnalog);

        public override string Comment => 
            ContainsAnyAnalog
                ? "������������ �������"
                : null;

        /// <summary>
        /// ������������� � ������ ������������ ��������
        /// </summary>
        public override double SumPriceTotal => Prices.Sum(price => price.SumPriceTotal);

        /// <summary>
        /// ��������� ������ � ������������� �����
        /// </summary>
        public override double SumFixedTotal => Prices.Sum(price => price.SumFixedTotal);

        public override List<IPrice> Prices { get; protected set; }

        public PriceGroup(string name, IEnumerable<IPrice> prices)
        {
            Name = name;
            Prices = prices.OrderByDescending(price => price.SumTotal).ToList();
        }
    }
}