using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HVTApp.Infrastructure
{
    /// <summary>
    ///  ласс дл€ измерени€ скорости выполнени€ кусков кода.
    /// </summary>
    public static class TimeMeasure
    {
        private static List<TimeDistance> _timeDistances = new List<TimeDistance>();

        public static void Start()
        {
            _timeDistances.Clear();
            _timeDistances.Add(new TimeDistance("старт измерений"));
        }

        /// <summary>
        /// ¬ременна€ отсечка. «аканчивает текущий временной отрезок и начинает новый.
        /// </summary>
        public static void StopDistance(string stopComment)
        {
            _timeDistances.Last().Stop(stopComment);
            _timeDistances.Add(new TimeDistance(stopComment));
        }

        public static void Stop()
        {
            _timeDistances.Last().Stop("окончание измерений");
        }

        public static void PrintResults()
        {
            Debug.Print("–езультаты измерений скорости работы кусков кода:");

            var ticksAll = _timeDistances.Sum(distance => distance.Ticks);
            int i = 1;
            foreach (var distance in _timeDistances)
            {
                Debug.Print($"{i}. {distance.Seconds:F2} сек. ({(distance.Ticks / ticksAll):P2}): {distance.StartComment} <=> {distance.StopComment}");
            }
        }

        /// <summary>
        /// ¬ременной отрезок.
        /// </summary>
        private class TimeDistance
        {
            public string StartComment { get; }
            public string StopComment { get; private set; }


            public long StartTicks { get; }
            public long StopTicks { get; private set; }

            public double Ticks => (StopTicks - StartTicks);
            public double Seconds => (StopTicks - StartTicks) / TimeSpan.TicksPerSecond;

            public TimeDistance(string startComment)
            {
                StartComment = startComment;
                StartTicks = DateTime.Now.Ticks;
            }

            public void Stop(string stopComment)
            {
                StopComment = stopComment;
                StopTicks = DateTime.Now.Ticks;
            }
        }
    }
}