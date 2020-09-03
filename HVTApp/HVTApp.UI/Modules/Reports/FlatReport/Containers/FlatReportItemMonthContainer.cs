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
        /// Текущая сумма
        /// </summary>
        public double CurrentSum => FlatReportItems.Any() ? FlatReportItems.Sum(x => x.Sum) : 0.0;

        /// <summary>
        /// Целевая сумма (к которой необходимо стремиться).
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
        /// Точность попадания (в долях: 0,01; 0,05 и т.д.)
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
        /// Целевая сумма достигнута
        /// </summary>
        public bool IsOk => IsPast || CurrentSum < TargetSum * (1.0 + Accuracy) && CurrentSum > TargetSum * (1.0 - Accuracy);

        /// <summary>
        /// Контейнер из прошлого
        /// </summary>
        public bool IsPast => new DateTime(Year, Month, 1) < DateTime.Today && !(Year == DateTime.Today.Year && Month == DateTime.Today.Month);

        /// <summary>
        /// Разница между целевой и текущей суммой.
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
        /// Присвоить контейнеру год и месяц (в зависимости от того, что содержит контейнер: ОИТ или реализацию)
        /// </summary>
        /// <param name="flatReportItems"></param>
        protected abstract void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems);

        public override string ToString()
        {
            var d = "";
            if (!IsOk)
            {
                if (IsHigh)
                    d = "избыток";
                if (IsLow)
                    d = "недостаток";

                return $"Год: {Year}, месяц: {Month:D2}; не в допуске; {Difference:N} ({d})";
            }

            return $"Год: {Year}, месяц: {Month:D2}; в допуске";
        }
    }
}