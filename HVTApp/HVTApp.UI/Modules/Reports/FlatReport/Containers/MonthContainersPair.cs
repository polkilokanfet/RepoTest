using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class MonthContainersPair
    {
        private readonly List<FlatReportItemMonthContainer> _containers;

        public FlatReportItemMonthContainer DonorContainer => _containers.OrderBy(x => x.Difference).First();
        public FlatReportItemMonthContainer AcceptorContainer => _containers.OrderBy(x => x.Difference).Last();

        /// <summary>
        /// Оба контейнера в допуске
        /// </summary>
        public bool IsOk => _containers.All(x => x.IsOk);

        /// <summary>
        /// Можно ли перекидывать айтемы
        /// </summary>
        public bool CanMove => !IsOk && CanMoveItem;

        /// <summary>
        /// Потенциал
        /// </summary>
        public double Difference => Math.Abs(DonorContainer.Difference - AcceptorContainer.Difference);

        public MonthContainersPair(FlatReportItemMonthContainer container1, FlatReportItemMonthContainer container2)
        {
            _containers = new List<FlatReportItemMonthContainer> {container1, container2};
        }

        private bool CanMoveItem
        {
            get
            {
                //нельзя скинуть из пустого контейнера
                if (!DonorContainer.FlatReportItems.Any())
                    return false;

                //нельзя скинуть айтемы, которые уже в ОИТе
                if (DonorContainer.FlatReportItems.All(x => x.SalesUnit.OrderIsTaken))
                    return false;

                //нельзя скинуть в контейнер из прошлого
                if (AcceptorContainer.IsPast)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Перекинуть айтем
        /// </summary>
        public void MoveItem()
        {
            if (!CanMoveItem)
                return;

            double dif = Difference;

            var donorContainer = DonorContainer;
            var acceptorContainer = AcceptorContainer;

            var item = donorContainer.FlatReportItems
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(acceptorContainer, x))
                .ThenBy(x => Math.Abs(acceptorContainer.Difference - x.Sum))
                .First();

            donorContainer.FlatReportItems.Remove(item);
            acceptorContainer.FlatReportItems.Add(item);

            //if (Difference > dif)
            //{
            //    donorContainer.FlatReportItems.Add(item);
            //    acceptorContainer.FlatReportItems.Remove(item);
            //}
        }

        private int MonthsBetween(FlatReportItemMonthContainer container, FlatReportItem item)
        {
            var date = new DateTime(container.Year, container.Month, 1);
            return Math.Abs(item.OriginalOrderInTakeDate.MonthsBetween(date));
        }

        public override string ToString()
        {
            if (IsOk)
                return "Ok";
            return $"{Difference:N}";
        }
    }
}