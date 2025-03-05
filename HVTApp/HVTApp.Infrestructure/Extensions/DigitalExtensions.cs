using System;
using System.Globalization;

namespace HVTApp.Infrastructure.Extensions
{
    public static class DigitalExtensions
    {
        /// <summary>
        /// Возвращает число прописью
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToWords(this long number)
        {

            if (number == 0)
                return "ноль";

            if (number < 0)
                return "минус " + ToWords(Math.Abs(number));

            string words = "";

            string[] units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять",
                "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
                "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };

            string[] tens = { "", "", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят",
                "семьдесят", "восемьдесят", "девяносто" };

            string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот",
                "семьсот", "восемьсот", "девятьсот" };

            string[,] thousands = {
                { "", "", "" },
                { "тысяча", "тысячи", "тысяч" },
                { "миллион", "миллиона", "миллионов" },
                { "миллиард", "миллиарда", "миллиардов" }
            };

            int unitIndex = 0;

            while (number > 0)
            {
                int chunk = (int)(number % 1000);
                number /= 1000;

                if (chunk > 0)
                {
                    string chunkWords = "";

                    int h = chunk / 100;
                    int t = (chunk % 100) / 10;
                    int u = chunk % 10;

                    if (h > 0)
                        chunkWords += hundreds[h] + " ";

                    if (t == 1)
                    {
                        chunkWords += units[10 + u] + " ";
                    }
                    else
                    {
                        if (t > 1)
                            chunkWords += tens[t] + " ";

                        if (u > 0)
                            chunkWords += (unitIndex == 1 && (u == 1 || u == 2) ?
                                new string[] { "одна", "две" }[u - 1] : units[u]) + " ";
                    }

                    if (unitIndex > 0)
                    {
                        int form = (u == 1 && t != 1) ? 0 : (u >= 2 && u <= 4 && t != 1 ? 1 : 2);
                        chunkWords += thousands[unitIndex, form] + " ";
                    }

                    words = chunkWords + words;
                }

                unitIndex++;
            }

            return CultureInfo.CurrentCulture.TextInfo.ToLower(words.Trim());
        }

        /// <summary>
        /// Сумма прописью в руб.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToSumWordCurrency(this double number)
        {
            var n1 = (long)Math.Truncate(number);
            var n2 = (int)((number - n1) * 100);
            return $"{n1.ToWords()} руб. {n2:D2} коп.";
        }

    }
}