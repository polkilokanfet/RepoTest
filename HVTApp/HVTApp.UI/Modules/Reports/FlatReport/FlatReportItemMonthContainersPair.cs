using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportItemMonthContainersPair
    {
        private readonly List<FlatReportItemMonthContainer> _containers;

        public FlatReportItemMonthContainer DonorContainer => _containers.OrderBy(x => x.CurrentSum).Last();
        public FlatReportItemMonthContainer AcceptorContainer => _containers.OrderBy(x => x.CurrentSum).First();

        /// <summary>
        /// ��� ���������� � �������
        /// </summary>
        public bool IsOk => _containers.All(x => x.IsOk);

        /// <summary>
        /// ����� �� ������������ ������ �� �������� ���������� � ������
        /// </summary>
        public bool CanMove => !IsOk || !AcceptorContainer.IsPast;

        public bool HasFatMember => _containers.Any(x => x.CurrentSum > x.TargetSum);
        public bool HasThinMember => _containers.Any(x => x.CurrentSum < x.TargetSum);
        public bool BothIsThin => _containers.All(x => x.CurrentSum < x.TargetSum);


        public double Difference => DonorContainer.CurrentSum - AcceptorContainer.CurrentSum;

        public FlatReportItemMonthContainersPair(FlatReportItemMonthContainer container1, FlatReportItemMonthContainer container2)
        {
            _containers = new List<FlatReportItemMonthContainer> {container1, container2};
        }

        public bool CanMoveItem
        {
            get
            {
                //������ ������� �� ������� ����������
                if (!DonorContainer.FlatReportItems.Any())
                    return false;

                //������ ������� ������, ������� ��� � ����
                if (DonorContainer.FlatReportItems.All(x => x.SalesUnit.OrderIsTaken))
                    return false;

                //������ ������� � ��������� �� ��������
                if (AcceptorContainer.IsPast)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// ���������� ����� �� �������� ���������� � ������
        /// </summary>
        public void MoveItem()
        {
            if (IsOk || !CanMoveItem)
                return;

            var donorContainer = DonorContainer;
            var acceptorContainer = AcceptorContainer;

            var notOkContainer = this._containers.Where(x => !x.IsOk).OrderBy(x => Math.Abs(x.Difference)).LastOrDefault();

            var item = donorContainer.FlatReportItems
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(acceptorContainer, x))
                .ThenBy(x => Math.Abs(Math.Abs(notOkContainer.Difference)- x.Sum))
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