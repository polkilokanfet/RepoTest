using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public class FlatReportItemMonthContainer
    {
        public List<FlatReportItem> FlatReportItems { get; }

        public DateTime Date => new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

        public string Title => $"{Year}.{Month}";

        /// <summary>
        /// ������� �����
        /// </summary>
        public double CurrentSum => FlatReportItems.Sum(x => x.Sum);

        /// <summary>
        /// ������� �����
        /// </summary>
        public double TargetSum { get; }

        /// <summary>
        /// �������� ��������� (� �����: 0,01; 0,05 � �.�.)
        /// </summary>
        public double Accuracy { get; }

        /// <summary>
        /// ������� ����� ����������
        /// </summary>
        public bool IsOk => IsPast || CurrentSum < TargetSum * (1.0 + Accuracy) && CurrentSum > TargetSum * (1.0 - Accuracy);

        /// <summary>
        /// ��������� �� ��������
        /// </summary>
        public bool IsPast => new DateTime(Year, Month, 1) < DateTime.Today && !(Year == DateTime.Today.Year && Month == DateTime.Today.Month);

        /// <summary>
        /// ������� ����� ������� � ������� ������
        /// </summary>
        public double Difference => TargetSum - CurrentSum;

        public int Year { get; }

        public int Month { get; }

        public string MonthName => new DateTime(Year, Month, 1).MonthName();

        public FlatReportItemMonthContainer(IEnumerable<FlatReportItem> flatReportItems, double targetSum, double accuracy = 0.05, DateTime? date = null)
        {
            FlatReportItems = flatReportItems?.ToList() ?? new List<FlatReportItem>();

            TargetSum = targetSum;
            Accuracy = accuracy;

            Year = date?.Year ?? FlatReportItems.First().EstimatedOrderInTakeDate.Year;
            Month = date?.Month ?? FlatReportItems.First().EstimatedOrderInTakeDate.Month;
        }

        /// <summary>
        /// ��������� ������������ ����� �������� �� ����������
        /// </summary>
        public void FillEstimatedOrderInTakeDates()
        {
            foreach (var flatReportItem in FlatReportItems)
            {
                if (Year != flatReportItem.OriginalOrderInTakeDate.Year || Month != flatReportItem.OriginalOrderInTakeDate.Month)
                {
                    var minDate = new DateTime(Year, Month, 1);
                    var maxDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
                    flatReportItem.EstimatedOrderInTakeDate = 
                        Math.Abs((minDate - flatReportItem.OriginalOrderInTakeDate).Days) < Math.Abs((maxDate - flatReportItem.OriginalOrderInTakeDate).Days) 
                        ? minDate 
                        : maxDate;
                }
            }
        }

        public override string ToString()
        {
            return $"{Year}/{Month} {Difference / 1000000.0} {IsOk}";
        }
    }
}