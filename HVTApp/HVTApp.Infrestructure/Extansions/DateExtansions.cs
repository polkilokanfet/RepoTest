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
    }
}
