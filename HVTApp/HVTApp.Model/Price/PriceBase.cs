using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Price
{
    public abstract class PriceBase : IPrice
    {
        /// <summary>
        /// Коэффициент упаковки
        /// "гениальное изобретение фин.отдела"
        /// </summary>
        protected const double KUp = 1.03;

        public abstract bool ContainsAnyAnalog { get; }
        public abstract bool ContainsAnyBlockWithNoLaborHours { get; }
        public ProductBlock Analog { get; protected set; }
        public virtual string Comment { get; }
        public virtual string CommentLaborHours { get; }
        public string Name { get; protected set; }
        public double Amount { get; protected set; } = 1;

        /// <summary>
        /// ПЗ + ФЗ
        /// </summary>
        public double SumTotal => SumPriceTotal + SumFixedTotal;

        public double SumPriceOnUnit => SumPriceTotal / Amount;

        public abstract double SumPriceTotal { get; }
        public double SumFixedOnUnit => SumFixedTotal / Amount;

        public double? SumFixed { get; set; }

        public virtual double SumFixedTotal { get; } = 0;
        public virtual List<IPrice> Prices { get; protected set; }


        public virtual double? LaborHours { get; }
        public double? LaborHoursTotal => LaborHours * Amount;
        public virtual double? WageFund { get; }
        public double? WageFundTotal => WageFund * Amount;

        public override string ToString()
        {
            return $"[IPrice] {Name}";
        }
    }
}