using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class MonthContainersPair
    {
        private readonly List<ContainerMonth> _containers;

        public ContainerMonth DonorContainer => _containers.OrderBy(x => x.Difference).First();
        public ContainerMonth AcceptorContainer => _containers.OrderBy(x => x.Difference).Last();

        /// <summary>
        /// ��� ���������� � �������
        /// </summary>
        public bool IsOk => _containers.All(x => x.IsOk);

        /// <summary>
        /// ����� �� ������������ ������
        /// </summary>
        public bool CanMove => !IsOk && CanMoveItem;

        /// <summary>
        /// ���������
        /// </summary>
        public double Difference => Math.Abs(DonorContainer.Difference - AcceptorContainer.Difference);

        public MonthContainersPair(ContainerMonth container1, ContainerMonth container2)
        {
            _containers = new List<ContainerMonth> {container1, container2};
        }

        private bool CanMoveItem
        {
            get
            {
                //������ ������� �� ������� ����������
                if (!DonorContainer.ItemsNotLoosed.Any())
                    return false;

                //������ ������� ������, ������� ��� � ����
                if (DonorContainer.ItemsNotLoosed.All(x => x.SalesUnit.OrderIsTaken))
                    return false;

                //������ ������� � ��������� �� ��������
                if (AcceptorContainer.IsPast)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// ���������� �����
        /// </summary>
        public void MoveItem()
        {
            if (!CanMoveItem)
                return;

            var donorContainer = DonorContainer;
            var acceptorContainer = AcceptorContainer;

            var item = donorContainer.ItemsNotLoosed
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(acceptorContainer, x))
                .ThenBy(x => Math.Abs(acceptorContainer.Difference - x.Sum))
                .First();

            donorContainer.Items.Remove(item);
            acceptorContainer.Items.Add(item);
        }

        private int MonthsBetween(ContainerMonth container, FlatReportItem item)
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