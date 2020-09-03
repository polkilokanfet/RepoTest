using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public abstract class FlatReportItemMonthContainer : BindableBase
    {
        private double _targetSum;
        private double _accuracy;

        public ObservableCollection<FlatReportItem> FlatReportItems { get; } = new ObservableCollection<FlatReportItem>();

        public DateTime Date => new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

        public string Title => $"{Year}.{Month}";

        /// <summary>
        /// ������� �����
        /// </summary>
        public double CurrentSum => FlatReportItems.Any() ? FlatReportItems.Sum(x => x.Sum) : 0.0;

        /// <summary>
        /// ������� ����� (� ������� ���������� ����������).
        /// </summary>
        public double TargetSum
        {
            get { return _targetSum; }
            set
            {
                if (IsPast)
                    return;

                _targetSum = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsOk));
            }
        }

        /// <summary>
        /// �������� ��������� (� �����: 0,01; 0,05 � �.�.)
        /// </summary>
        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                _accuracy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsOk));
            }
        }

        /// <summary>
        /// ������� ����� ����������
        /// </summary>
        public bool IsOk => IsPast || CurrentSum < TargetSum * (1.0 + Accuracy) && CurrentSum > TargetSum * (1.0 - Accuracy);

        /// <summary>
        /// ��������� �� ��������
        /// </summary>
        public bool IsPast => new DateTime(Year, Month, 1) < DateTime.Today && !(Year == DateTime.Today.Year && Month == DateTime.Today.Month);

        /// <summary>
        /// ������� ����� ������� � ������� ������.
        /// Difference => TargetSum - CurrentSum
        /// </summary>
        public double Difference => TargetSum - CurrentSum;

        public bool IsHigh => CurrentSum > TargetSum;
        public bool IsLow => CurrentSum < TargetSum;

        public int Year { get; protected set; }

        public int Month { get; protected set; }

        public string MonthName => new DateTime(Year, Month, 1).MonthName();

        private FlatReportItemMonthContainer(double targetSum, double accuracy)
        {
            _targetSum = targetSum;
            Accuracy = accuracy;

            this.FlatReportItems.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (var item in args.NewItems.Cast<FlatReportItem>())
                    {
                        item.EstimatedCostIsChanged += ItemOnEstimatedCostIsChanged;
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (var item in args.OldItems.Cast<FlatReportItem>())
                    {
                        item.EstimatedCostIsChanged -= ItemOnEstimatedCostIsChanged;
                    }
                }

                ItemOnEstimatedCostIsChanged();
            };
        }

        private void ItemOnEstimatedCostIsChanged()
        {
            OnPropertyChanged(nameof(CurrentSum));
            OnPropertyChanged(nameof(IsOk));
        }

        protected FlatReportItemMonthContainer(IEnumerable<FlatReportItem> flatReportItems, double targetSum, double accuracy) : this(targetSum, accuracy)
        {
            FlatReportItems.AddRange(flatReportItems);
            FillYearAndMonth(FlatReportItems);
        }

        protected FlatReportItemMonthContainer(DateTime date, double targetSum, double accuracy) : this(targetSum, accuracy)
        {
            Year = date.Year;
            Month = date.Month;
        }

        /// <summary>
        /// ��������� ���������� ��� � ����� (� ����������� �� ����, ��� �������� ���������: ��� ��� ����������)
        /// </summary>
        /// <param name="flatReportItems"></param>
        protected abstract void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems);

        public override string ToString()
        {
            var d = "";
            if (!IsOk)
            {
                if (IsHigh)
                    d = "�������";
                if (IsLow)
                    d = "����������";

                return $"���: {Year}, �����: {Month:D2}; �� � �������; {Difference:N} ({d})";
            }

            return $"���: {Year}, �����: {Month:D2}; � �������";
        }
    }
}