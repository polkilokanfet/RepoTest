using System;

namespace HVTApp.Model.Services
{
    public static class DateServices
    {
        /// <summary>
        /// Возвращает сегодня, если проверяемая дата раньше сегодняшней.
        /// </summary>
        /// <param name="date">Проверяемая дата.</param>
        /// <returns></returns>
        public static DateTime GetTodayIfDateToEarly(this DateTime date)
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
        public static DateTime GetTodayIfDateFromPastAndSkipWeekend(this DateTime date)
        {
            return GetTodayIfDateToEarly(SkipWeekend(date));
        }
    }
}
