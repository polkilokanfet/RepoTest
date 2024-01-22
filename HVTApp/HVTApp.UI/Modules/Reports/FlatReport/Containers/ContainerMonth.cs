using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToString) + "()}")]
    public abstract class ContainerMonth : BindableBase
    {
        private double _targetSum;
        private double _accuracy;
        private bool _inReport;

        public ObservableCollection<FlatReportItem> Items { get; } = new ObservableCollection<FlatReportItem>();

        public IEnumerable<FlatReportItem> ItemsNotLoosed => Items.Where(x => !x.IsLoosen);

        public DateTime Date => new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

        public string Title => $"{Year}.{Month}";

        /// <summary>
        /// ������� �����
        /// </summary>
        public double CurrentSum => Items.Any(x => x.InReport && !x.IsLoosen) ? Items.Where(x => x.InReport && !x.IsLoosen).Sum(x => x.Sum) : 0.0;

        public event Action CurrentSumIsChanged;

        /// <summary>
        /// ������� ����� (� ������� ���������� ����������).
        /// </summary>
        public double TargetSum
        {
            get
            {
                if (IsPast) return CurrentSum;
                return _targetSum;
            }
            set
            {
                if (IsPast)
                    return;

                //���� ��������� �������� ������, � ��� ����� ���� ��� ������ � ��� ������
                if (this.Date.IsFromCurrentMonth())
                {
                    var thisMonthOitSum = Items
                        .Where(x => x.InReport)
                        .Where(x => !x.IsLoosen)
                        .Where(x => x.OriginalOrderInTakeDate.BetweenDates(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today))
                        .Where(x => x.SalesUnit.OrderIsTaken)
                        .Sum(x => x.Sum);

                    if (value < thisMonthOitSum)
                        value = thisMonthOitSum;
                }

                _targetSum = value;
                TargetSumIsChanged?.Invoke();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsOk));
            }
        }

        public event Action TargetSumIsChanged;


        /// <summary>
        /// �������� ��������� (� �����: 0,01; 0,05 � �.�.)
        /// </summary>
        public double Accuracy
        {
            get => _accuracy;
            set
            {
                _accuracy = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsOk));
            }
        }

        /// <summary>
        /// ������� ����� ����������
        /// </summary>
        public bool IsOk => IsPast || CurrentSum < TargetSum * (1.0 + Accuracy) && CurrentSum > TargetSum * (1.0 - Accuracy);

        /// <summary>
        /// ��������� �� ��������
        /// </summary>
        public bool IsPast => new DateTime(Year, Month, 1) < DateTime.Today && !this.Date.IsFromCurrentMonth();

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

        public bool InReport
        {
            get { return _inReport; }
            set
            {
                if (_inReport == value)
                    return;

                _inReport = value;
                InReportIsChanged?.Invoke();
                RaisePropertyChanged();
            }
        }

        public event Action InReportIsChanged;

        private ContainerMonth(double accuracy)
        {
            Accuracy = accuracy;

            this.Items.CollectionChanged += (sender, args) =>
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

                ItemOnEstimatedCostIsChanged(null);
            };
        }

        private void ItemOnEstimatedCostIsChanged(FlatReportItem item)
        {
            RaisePropertyChanged(nameof(CurrentSum));
            RaisePropertyChanged(nameof(IsOk));
            CurrentSumIsChanged?.Invoke();
        }

        protected ContainerMonth(IEnumerable<FlatReportItem> flatReportItems, double accuracy) : this(accuracy)
        {
            Items.AddRange(flatReportItems);
            FillYearAndMonth(Items);
        }

        protected ContainerMonth(DateTime date, double accuracy) : this(accuracy)
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