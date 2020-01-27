using System;

namespace HVTApp.Infrastructure.Extansions
{
    public static class DateExtansions
    {
        /// <summary>
        /// Возвращает сегодня, если проверяемая дата раньше сегодняшней.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns></returns>
        public static DateTime SkipPast(this DateTime date)
        {
            if (date >= DateTime.Today) return date;
            return DateTime.Today;
        }

        /// <summary>
        /// Возвращает понедельник, если проверяемая дата суббота или воскресенье.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns></returns>
        public static DateTime SkipWeekend(this DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        /// <summary>
        /// Вернуть сегодня, если текущая дата из прошлого и исключить выходные.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime SkipPastAndWeekend(this DateTime date)
        {
            return SkipPast(SkipWeekend(date));
        }

        public static string MonthName(this DateTime date)
        {
            switch (date.Month)
            {
                case 1: return "01 - Январь";
                case 2: return "02 - Февраль";
                case 3: return "03 - Март";
                case 4: return "04 - Апрель";
                case 5: return "05 - Май";
                case 6: return "06 - Июнь";
                case 7: return "07 - Июль";
                case 8: return "08 - Август";
                case 9: return "09 - Сентябрь";
                case 10: return "10 - Октябрь";
                case 11: return "11 - Ноябрь";
                case 12: return "12 - Декабрь";
            }

            return "-";
        }

        public static int WeekNumber(this DateTime date)
        {
            var firstDayOfTheYear = new DateTime(date.Year, 1, 1);
            var firstMondayOfTheYear = firstDayOfTheYear;
            while (firstMondayOfTheYear.DayOfWeek != DayOfWeek.Monday)
            {
                firstMondayOfTheYear = firstMondayOfTheYear.AddDays(1);
            }
            var daysDif = (date - firstMondayOfTheYear).Days + 1;
            var week = (int)Math.Ceiling(daysDif / 7.0);
            if (Equals(firstDayOfTheYear, firstMondayOfTheYear))
                return week;
            return week + 1;
        }
    }
}
