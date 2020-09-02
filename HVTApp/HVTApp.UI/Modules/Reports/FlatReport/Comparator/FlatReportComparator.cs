using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;

namespace HVTApp.UI.Modules.Reports.FlatReport.Comparator
{
    public class FlatReportComparator
    {
        /// <summary>
        /// ¬ыровн€ть контейнеры по суммам
        /// </summary>
        /// <param name="containersEnum"></param>
        /// <returns></returns>
        public static IEnumerable<FlatReportItemMonthContainer> Align(IEnumerable<FlatReportItemMonthContainer> containersEnum)
        {
            var containers = containersEnum.ToList();
            var difList = new List<double>();
            double dif;

            //‘ормирование пар контейнеров
            var pairs = GetPairs(containers).ToList();

            while (!pairs.All(x => x.IsOk))
            {
                var pair = pairs.Where(x => x.CanMove).OrderBy(x => x.Difference).LastOrDefault();
                pair?.MoveItem();

                //pairs.Where(x => x.CanMove).OrderByDescending(x => x.Difference).ForEach(x =>
                //{
                //    if (x.CanMove)
                //        x.MoveItem();
                //});

                //провер€ем на мертвый цикл
                dif = containers.Sum(x => Math.Abs(x.Difference));
                difList.Add(dif);
                if (difList.Count(x => Math.Abs(dif - x) < 0.001) > 200)
                    break;
            }


            //while (!containers.All(x => x.IsOk))
            //{
            //    var pairs = GetPairs(containers).Where(x => x.CanMove).ToList();
            //    pairs.Where(x => x.CanMove).ForEach(x => x.MoveItem());

            //    //провер€ем на мертвый цикл
            //    dif = containers.Sum(x => Math.Abs(x.Difference));
            //    difList.Add(dif);
            //    if (difList.Count(x => Math.Abs(dif - x) < 0.001) > 200)
            //        break;
            //}

            return containers;
        }

        /// <summary>
        /// ‘ормирование пар контейнеров
        /// </summary>
        /// <param name="containersEnum"></param>
        /// <returns></returns>
        private static IEnumerable<MonthContainersPair> GetPairs(IEnumerable<FlatReportItemMonthContainer> containersEnum)
        {
            var containers = containersEnum.OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();
            for (int i = 1; i < containers.Count; i++)
            {
                yield return new MonthContainersPair(containers[i-1], containers[i]);
            }
        }
    }
}