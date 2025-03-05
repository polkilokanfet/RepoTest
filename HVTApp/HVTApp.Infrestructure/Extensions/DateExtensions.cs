using System;
using System.Globalization;

namespace HVTApp.Infrastructure.Extensions
{
    public static class DateExtensions
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

        /// <summary>
        /// Номер недели в году (первая неделя та, в которой от 4-х дней)
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Номер недели</returns>
        public static int WeekNumber(this DateTime date)
        {
            return new GregorianCalendar().GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static string WeekNumberString(this DateTime date)
        {
            var firstDayOfWeek = date;
            while (firstDayOfWeek.DayOfWeek != DayOfWeek.Monday && firstDayOfWeek.AddDays(-1).Year == date.Year)
            {
                firstDayOfWeek = firstDayOfWeek.AddDays(-1);
            }

            var lastDayOfWeek = date;
            while (lastDayOfWeek.DayOfWeek != DayOfWeek.Sunday && lastDayOfWeek.AddDays(1).Year == date.Year)
            {
                lastDayOfWeek = lastDayOfWeek.AddDays(1);
            }

            return $"{date.WeekNumber():D2} нед. (с {firstDayOfWeek.ToShortDateString()} по {lastDayOfWeek.ToShortDateString()})";
        }

        /// <summary>
        /// Количество месяцов между датами (может быть отрицательным)
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int MonthsBetween(this DateTime date1, DateTime date2)
        {
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

        /// <summary>
        /// Дата находится внутри диапазона дат (включая края).
        /// </summary>
        /// <param name="date">Проверяемая дата</param>
        /// <param name="startDate">Начало диапазона дат</param>
        /// <param name="finishDate">Конец диапазона дат</param>
        /// <returns></returns>
        public static bool BetweenDates(this DateTime date, DateTime startDate, DateTime finishDate)
        {
            return date >= startDate && date <= finishDate;
        }

        /// <summary>
        /// Дата из текущего месяца
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsFromCurrentMonth(this DateTime date)
        {
            return date.Year == DateTime.Today.Year && date.Month == DateTime.Today.Month;
        }
    }
}
