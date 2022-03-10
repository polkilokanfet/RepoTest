using System;
using System.Globalization;

namespace HVTApp.Model.Wrapper
{
    public partial class StructureCostWrapper
    {
        private double _amountNumerator;
        private double _amountDenomerator;

        /// <summary>
        /// Количество (числитель)
        /// </summary>
        public double AmountNumerator
        {
            get => _amountNumerator;
            set
            {
                if (Equals(_amountNumerator, value)) return;
                if (value <= 0) return;

                _amountNumerator = value;
                Amount = AmountNumerator / AmountDenomerator;
            }
        }

        /// <summary>
        /// Количество (знаменатель)
        /// </summary>
        public double AmountDenomerator
        {
            get => _amountDenomerator;
            set
            {
                if (Equals(_amountDenomerator, value)) return;
                if (value <= 0) return;

                _amountDenomerator = value;
                Amount = AmountNumerator / AmountDenomerator;
            }
        }

        public override void InitializeOther()
        {
            //if (GetDecimalDigitsCount(Amount) < 5)
            //{
            //    NormalFraction fraction = DoubleToNormalFraction(Amount);
            //    _amountNumerator = fraction.Numerator;
            //    _amountDenomerator = fraction.Denominator;
            //}
            //else
            //{
            //    _amountNumerator = Amount;
            //    _amountDenomerator = 1;
            //}


            _amountNumerator = Amount;
            _amountDenomerator = 1;

            this.PropertyChanged += (sender, args) =>
            {
                if(args.PropertyName == nameof(UnitPrice))
                    OnPropertyChanged(nameof(Total));
            };
        }

        /// <summary>
        /// Количество знаков после запятой
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static int GetDecimalDigitsCount(double number)
        {
            string str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." });
            return str.Contains(".") ? str.Remove(0, Math.Truncate(number).ToString().Length + 1).Length : 0;
        }

        /// <summary>
        /// Приведение к дроби
        /// </summary>
        /// <param name="numeric"></param>
        /// <returns></returns>
        private static NormalFraction DoubleToNormalFraction(double numeric)
        {
            //Разбиваем число на целую и дробную часть
            var numericArray = numeric.ToString().Split(new[] { CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator }, StringSplitOptions.None);
            var wholeStr = numericArray[0];
            var fractionStr = "0";
            if (numericArray.Length > 1)
                fractionStr = numericArray[1];

            //Получаем степень десятки, на которую нужно умножить число, чтобы дробь стала целым 
            var power = fractionStr.Length;

            //Получаем целую часть числителя и знаменатель
            long whole = long.Parse(wholeStr) * 10;
            long denominator = 10;
            for (int i = 1; i < power; i++)
            {
                denominator = denominator * 10;
                whole = whole * 10;
            }

            //получаем числитель
            var numerator = long.Parse(fractionStr);
            numerator = numerator + whole;
            
            //Ищем общий знаменатель и делим на него
            var index = 2;
            while (index < denominator / 2) //Если дошли до половины, то там его нет. Тут вообще можно брать наименьшее из числителя и знаменателя
            {
                if (numerator % index == 0 && denominator % index == 0)
                {
                    numerator = numerator / index;
                    denominator = denominator / index;
                    index = 1; //При i++ будет увеличен до 2х
                }
                index++;
            }

            return new NormalFraction(numerator, denominator);
        }

        class NormalFraction
        {
            public long Numerator { get; }
            public long Denominator { get; }

            public NormalFraction(long numerator, long denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }
        }
    }
}