using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class FlatReportItemMonthContainersPair
    {
        private readonly List<FlatReportItemMonthContainer> _containers;

        public FlatReportItemMonthContainer DonorContainer => _containers.OrderBy(x => x.Difference).First();
        public FlatReportItemMonthContainer AcceptorContainer => _containers.OrderBy(x => x.Difference).Last();

        /// <summary>
        /// ќба контейнера в допуске
        /// </summary>
        public bool IsOk => _containers.All(x => x.IsOk);

        /// <summary>
        /// ћожно ли перекидывать айтемы из богатого контейнера в бедный
        /// </summary>
        public bool CanMove => !IsOk && CanMoveItem;

        public bool HasFatMember => _containers.Any(x => x.CurrentSum > x.TargetSum);
        public bool HasThinMember => _containers.Any(x => x.CurrentSum < x.TargetSum);
        public bool BothIsThin => _containers.All(x => x.CurrentSum < x.TargetSum);


        public double Difference => AcceptorContainer.Difference - DonorContainer.Difference;

        public FlatReportItemMonthContainersPair(FlatReportItemMonthContainer container1, FlatReportItemMonthContainer container2)
        {
            _containers = new List<FlatReportItemMonthContainer> {container1, container2};
        }

        private bool CanMoveItem
        {
            get
            {
                //нельз€ скинуть из пустого контейнера
                if (!DonorContainer.FlatReportItems.Any())
                    return false;

                //нельз€ скинуть айтемы, которые уже в ќ»“е
                if (DonorContainer.FlatReportItems.All(x => x.SalesUnit.OrderIsTaken))
                    return false;

                //нельз€ скинуть в контейнер из прошлого
                if (AcceptorContainer.IsPast)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// ѕерекинуть айтем из богатого контейнера в бедный
        /// </summary>
        public void MoveItem()
        {
            if (!CanMoveItem)
                return;

            var donorContainer = DonorContainer;
            var acceptorContainer = AcceptorContainer;

            var item = donorContainer.FlatReportItems
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(acceptorContainer, x))
                .ThenBy(x => Math.Abs(acceptorContainer.Difference - x.Sum))
                .First();

            donorContainer.FlatReportItems.Remove(item);
            acceptorContainer.FlatReportItems.Add(item);
        }

        private int MonthsBetween(FlatReportItemMonthContainer container, FlatReportItem item)
        {
            var date = new DateTime(container.Year, container.Month, 1);
            return Math.Abs(item.OriginalOrderInTakeDate.MonthsBetween(date));
        }
    }
}