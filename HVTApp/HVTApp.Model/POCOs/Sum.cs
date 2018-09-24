using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Сумма (фэйк)")]
    public partial class Sum : BaseEntity
    {
        private decimal _value;
        public SumType Type { get; set; }
        public Currency Currency { get; set; } = Currency.RUB;

        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                if (value < 0)
                {
                    Type = Type.Another();
                    _value = -value;
                }
                _value = value;
            }
        }
    }

    public partial class Sum : BaseEntity
    {
        public static Sum operator +(Sum sum1, Sum sum2)
        {
            if (!Equals(sum1.Currency, sum2.Currency))
                throw new ArgumentException("В оперируемых суммах различные валюты.");

            var result = new Sum { Currency = sum1.Currency, Value = sum1.Value };

            result.Value = sum1.Type != sum2.Type 
                ? sum1.Value - sum2.Value 
                : sum1.Value + sum2.Value;

            return result;
        }

        public static Sum operator -(Sum sum1, Sum sum2)
        {
            if (!Equals(sum1.Currency, sum2.Currency))
                throw new ArgumentException("В оперируемых суммах различные валюты.");
            return (sum1 + (-sum2));
        }

        public static Sum operator -(Sum sum)
        {
            return new Sum { Currency = sum.Currency, Value = sum.Value, Type = sum.Type.Another()};
        }
    }

    public enum SumType
    {
        Debet,
        Credit
    }

    public static class SumTypeExt
    {
        public static SumType Another(this SumType type)
        {
            return type == SumType.Credit
                ? SumType.Debet
                : SumType.Credit;
        }
    }
}