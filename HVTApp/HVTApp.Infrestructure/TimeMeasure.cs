using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HVTApp.Infrastructure
{
    /// <summary>
    /// ����� ��� ��������� �������� ���������� ������ ����.
    /// </summary>
    public static class TimeMeasure
    {
        private static List<TimeDistance> _timeDistances = new List<TimeDistance>();

        public static void Start()
        {
            _timeDistances.Clear();
            _timeDistances.Add(new TimeDistance("����� ���������"));
        }

        /// <summary>
        /// ��������� �������. ����������� ������� ��������� ������� � �������� �����.
        /// </summary>
        public static void StopDistance(string stopComment)
        {
            _timeDistances.Last().Stop(stopComment);
            _timeDistances.Add(new TimeDistance(stopComment));
        }

        public static void Stop()
        {
            _timeDistances.Last().Stop("��������� ���������");
        }

        public static void PrintResults()
        {
            Debug.Print("���������� ��������� �������� ������ ������ ����:");

            var ticksAll = _timeDistances.Sum(distance => distance.Ticks);
            int i = 1;
            foreach (var distance in _timeDistances)
            {
                Debug.Print($"{i}. {distance.Seconds:F2} ���. ({(distance.Ticks / ticksAll):P2}): {distance.StartComment} <=> {distance.StopComment}");
            }
        }

        /// <summary>
        /// ��������� �������.
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