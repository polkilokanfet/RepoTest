using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class FlatReportItem : INotifyPropertyChanged
    {
        private DateTime _estimatedOrderInTakeDate;
        private bool _inReport = false;
        private double _estimatedCost;
        private DateTime _estimatedRealizationDate;
        private PaymentConditionSet _estimatedPaymentConditionSet;

        public List<SalesUnit> SalesUnits { get; }
        public SalesUnit SalesUnit => SalesUnits.First();
        public int Amount => SalesUnits.Count;
        public string Manager => SalesUnit.Project.Manager.Employee.Person.Surname;

        /// <summary>
        /// Включать в отчет
        /// </summary>
        public bool InReport
        {
            get { return _inReport; }
            set
            {
                if (Equals(_inReport, value))
                    return;

                _inReport = value;
                OnPropertyChanged();
                InReportIsChanged?.Invoke(this);
            }
        }

        public event Action<FlatReportItem> InReportIsChanged;

        public bool AllowEditOit => !SalesUnit.OrderIsTaken && !IsLoosen;

        public bool AllowEditRealization => !SalesUnit.OrderIsRealized && !IsLoosen;

        public bool IsLoosen => SalesUnit.IsLoosen;

        /// <summary>
        /// Стоимость всех юнитов
        /// </summary>
        public double Sum => EstimatedCost * Amount;

        /// <summary>
        /// Изначальная дата ОИТ
        /// </summary>
        public DateTime OriginalOrderInTakeDate { get; }

        /// <summary>
        /// Предположительная дата ОИТ
        /// </summary>
        public DateTime EstimatedOrderInTakeDate
        {
            get { return _estimatedOrderInTakeDate; }
            set
            {
                if (!AllowEditOit) return;
                if (value < DateTime.Today) return;

                if (Equals(_estimatedOrderInTakeDate, value)) return;

                if (!Equals(_estimatedOrderInTakeDate.Year, value.Year) ||
                    !Equals(_estimatedOrderInTakeDate.Month, value.Month))
                {
                    _estimatedOrderInTakeDate = value;
                    EstimatedOrderInTakeMonthIsChanged?.Invoke(this);
                }

                _estimatedOrderInTakeDate = value;

                //внедряем даты для расчета даты реализации
                SalesUnits.ForEach(x => x.OrderInTakeDateInjected = value);
                SalesUnits.ForEach(x => x.StartProductionDateInjected = value);
                EstimatedRealizationDate = SalesUnit.RealizationDateCalculated;

                OnPropertyChanged();
                OnPropertyChanged(nameof(EstimatedOrderInTakeDateYear));
                OnPropertyChanged(nameof(EstimatedOrderInTakeDateMonth));
                OnPropertyChanged(nameof(DifOitDays));
            }
        }

        public int EstimatedOrderInTakeDateYear => EstimatedOrderInTakeDate.Year;
        public string EstimatedOrderInTakeDateMonth => EstimatedOrderInTakeDate.MonthName();


        public event Action<FlatReportItem> EstimatedOrderInTakeMonthIsChanged;

        /// <summary>
        /// Сдвиг по дате ОИТ
        /// </summary>
        public int DifOitDays => EstimatedOrderInTakeDate.MonthsBetween(OriginalOrderInTakeDate);

        /// <summary>
        /// Предположительная стоимость юнита
        /// </summary>
        public double EstimatedCost
        {
            get { return _estimatedCost; }
            set
            {
                if (!AllowEditOit) return;
                if (Math.Abs(_estimatedCost - value) < 0.001) return;
                _estimatedCost = value;
                EstimatedCostIsChanged?.Invoke(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
            }
        }

        public event Action<FlatReportItem> EstimatedCostIsChanged;

        /// <summary>
        /// Предполагаемая себестоимость
        /// </summary>
        public double? EstimatedPrice { get; set; }

        /// <summary>
        /// Изначальная дата реализации
        /// </summary>
        public DateTime OriginalRealizationDate { get; }

        /// <summary>
        /// Предположительная дата реализации
        /// </summary>
        public DateTime EstimatedRealizationDate
        {
            get { return _estimatedRealizationDate; }
            set
            {
                if (!AllowEditRealization) return;
                if (Equals(value, _estimatedRealizationDate)) return;
                if (value < EstimatedOrderInTakeDate) return;

                if (!Equals(_estimatedRealizationDate.Year, value.Year) ||
                    !Equals(EstimatedRealizationDate.Month, value.Month))
                {
                    _estimatedRealizationDate = value;
                    EstimatedRealizationMonthIsChanged?.Invoke(this);
                }

                _estimatedRealizationDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DifRealizationDays));
            }
        }

        public event Action<FlatReportItem> EstimatedRealizationMonthIsChanged;

        /// <summary>
        /// Сдвиг по дате реализации
        /// </summary>
        public int DifRealizationDays => EstimatedRealizationDate.MonthsBetween(OriginalRealizationDate);

        /// <summary>
        /// Предполагаемые условия оплаты
        /// </summary>
        public PaymentConditionSet EstimatedPaymentConditionSet
        {
            get { return _estimatedPaymentConditionSet; }
            set
            {
                if (!AllowEditOit) return;
                _estimatedPaymentConditionSet = value;
                OnPropertyChanged();
            }
        }

        public FlatReportItem(IEnumerable<SalesUnit> salesUnits, bool inReport,
            double estimatedCost, DateTime estimatedOrderInTakeDate, DateTime estimatedRealizationDate,
            PaymentConditionSet estimatedPaymentConditionSet) : this(salesUnits, inReport)
        {
            var salesUnit = salesUnits.First();

            EstimatedCost = salesUnit.Cost;
            _estimatedCost = estimatedCost;

            _estimatedOrderInTakeDate = estimatedOrderInTakeDate;
            _estimatedRealizationDate = estimatedRealizationDate;
            _estimatedPaymentConditionSet = estimatedPaymentConditionSet;
        }

        public FlatReportItem(IEnumerable<SalesUnit> salesUnits, bool inReport)
        {
            if(!salesUnits.Any())
                throw new ArgumentException(nameof(salesUnits));
            SalesUnits = salesUnits.ToList();

            var salesUnit = salesUnits.First();

            _estimatedCost = salesUnit.Cost;
            _estimatedOrderInTakeDate = OriginalOrderInTakeDate = salesUnit.OrderInTakeDate;
            _estimatedRealizationDate = OriginalRealizationDate = salesUnit.RealizationDateCalculated;
            _estimatedPaymentConditionSet = salesUnit.PaymentConditionSet;
            _inReport = inReport;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{SalesUnit} ({Amount} шт.)";
        }
    }
}