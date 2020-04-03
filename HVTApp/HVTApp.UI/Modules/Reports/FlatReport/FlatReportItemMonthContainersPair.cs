using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportItemMonthContainersPair
    {
        private readonly List<FlatReportItemMonthContainer> _containers;

        public FlatReportItemMonthContainer RichContainer => _containers.OrderBy(x => x.CurrentSum).Last();
        public FlatReportItemMonthContainer PoorContainer => _containers.OrderBy(x => x.CurrentSum).First();

        /// <summary>
        /// ќба контейнера в допуске
        /// </summary>
        public bool IsOk => _containers.All(x => x.IsOk);

        /// <summary>
        /// ћожно ли перекидывать айтемы из богатого контейнера в бедный
        /// </summary>
        public bool CanMove => !IsOk || !PoorContainer.IsPast;

        public bool HasFatMember => _containers.Any(x => x.CurrentSum > x.TargetSum);
        public bool HasThinMember => _containers.Any(x => x.CurrentSum < x.TargetSum);
        public bool BothIsThin => _containers.All(x => x.CurrentSum < x.TargetSum);


        public double Difference => RichContainer.CurrentSum - PoorContainer.CurrentSum;

        public FlatReportItemMonthContainersPair(FlatReportItemMonthContainer container1, FlatReportItemMonthContainer container2)
        {
            _containers = new List<FlatReportItemMonthContainer> {container1, container2};
        }

        /// <summary>
        /// ѕерекинуть айтем из богатого контейнера в бедный
        /// </summary>
        public void MoveItem()
        {
            var richContainer = RichContainer;
            var poorContainer = PoorContainer;

            //нельз€ скинуть из пустого контейнера
            if (!richContainer.FlatReportItems.Any())
                return;

            //нельз€ скинуть айтемы, которые уже в ќ»“е
            if (richContainer.FlatReportItems.All(x => x.SalesUnit.OrderIsTaken))
                return;

            //нельз€ скинуть в контейнер из прошлого
            if (poorContainer.IsPast)
                return;

            var item = richContainer.FlatReportItems
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(poorContainer, x))
                .ThenBy(x => Math.Abs(poorContainer.Difference - x.Sum))
                .First();

            richContainer.FlatReportItems.Remove(item);
            poorContainer.FlatReportItems.Add(item);
        }

        private int MonthsBetween(FlatReportItemMonthContainer container, FlatReportItem item)
        {
            var date = new DateTime(container.Year, container.Month, 1);
            return Math.Abs(item.OriginalOrderInTakeDate.MonthsBetween(date));
        }
    }
}