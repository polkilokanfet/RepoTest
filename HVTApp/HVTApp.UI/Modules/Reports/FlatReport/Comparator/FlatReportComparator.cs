using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using HVTApp.Infrastructure.Extensions;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Reports.FlatReport.Comparator
{
    public class FlatReportComparator
    {
        /// <summary>
        /// ��������� ���������� �� ������
        /// </summary>
        /// <param name="containersEnum"></param>
        /// <returns></returns>
        public static IEnumerable<ContainerMonth> Align(IEnumerable<ContainerMonth> containersEnum)
        {
            var containers = containersEnum.ToList();
            var difList = new List<double>();
            double difCurrent;
            string ds;
            var dictionary = new Dictionary<ContainerMonth, List<FlatReportItem>>();

            //������������ ��� �����������
            var pairs = GetPairs(containers).ToList();

            while (!pairs.All(x => x.IsOk))
            {
                foreach (var containersPair in pairs.Where(x => x.CanMove).OrderBy(x => x.Difference))
                {
                    if (containersPair.CanMove)
                        containersPair.MoveItem();
                }

                difCurrent = containers.Sum(x => Math.Abs(x.Difference));
                difList.Add(difCurrent);

                var difMin = difList.Min();
                if (Math.Abs(difMin - difCurrent) < 0.001)
                {
                    dictionary.Clear();
                    foreach (var container in containers)
                    {
                        dictionary.Add(container, container.Items.ToList());
                    }
                }

                ds = $"{difMin:N}";

                //��������� �� ������� ����
                if (difList.Count(x => Math.Abs(difCurrent - x) < 0.001) > 100)
                {
                    foreach (var kvp in dictionary)
                    {
                        var container = kvp.Key;
                        container.Items.Clear();
                        container.Items.AddRange(kvp.Value);
                    }
                    break;
                }
            }

            return containers;
        }

        /// <summary>
        /// ������������ ��� �����������
        /// </summary>
        /// <param name="containersEnum"></param>
        /// <returns></returns>
        private static IEnumerable<MonthContainersPair> GetPairs(IEnumerable<ContainerMonth> containersEnum)
        {
            var containers = containersEnum.OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();
            for (int i = 1; i < containers.Count; i++)
            {
                yield return new MonthContainersPair(containers[i-1], containers[i]);
            }
        }
    }
}