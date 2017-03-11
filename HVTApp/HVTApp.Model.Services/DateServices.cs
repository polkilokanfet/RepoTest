using System;

namespace HVTApp.Model.Services
{
    public class DateServices
    {
        /// <summary>
        /// Возвращает сегодня, если проверяемая дата раньше сегодняшней.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns></returns>
        public static DateTime GetTodayIfDateToEarly(DateTime date)
        {
            if (date >= DateTime.Today) return date;
            return DateTime.Today;
        }

        /// <summary>
        /// Возвращает понедельник, если проверяемая дата суббота или воскресенье.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns></returns>
        public static DateTime SkipWeekend(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        public static DateTime GetTodayIfDateToEarlyAndSkipWeekend(DateTime date)
        {
            return GetTodayIfDateToEarly(SkipWeekend(date));
        }
    }
}
