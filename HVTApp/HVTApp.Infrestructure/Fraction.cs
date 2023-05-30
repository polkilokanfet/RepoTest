using System.Linq;

namespace HVTApp.Infrastructure
{
    public class Fraction
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public static Fraction Sum(Fraction fraction1, Fraction fraction2)
        {
            var numerator = fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator;
            var denominator = fraction1.Denominator * fraction2.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction Sum(params Fraction[] fractions)
        {
            var result = fractions.First();
            foreach (var fraction in fractions.Skip(1))
            {
                result = Fraction.Sum(result, fraction);
            }

            return result;
        }

    }
}