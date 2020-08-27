using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportItem : INotifyPropertyChanged
    {
        private DateTime _estimatedOrderInTakeDate;
        private bool _inReport = true;
        private double _estimatedCost;
        private DateTime _estimatedRealizationDate;
        private PaymentConditionSet _estimatedPaymentConditionSet;

        public List<SalesUnit> SalesUnits { get; }
        public SalesUnit SalesUnit => SalesUnits.First();
        public int Amount => SalesUnits.Count;
        public string Manager => SalesUnit.Project.Manager.Employee.Person.Surname;

        /// <summary>
        /// �������� � �����
        /// </summary>
        public bool InReport
        {
            get { return _inReport; }
            set
            {
                if (!AllowEdit) return;
                if (Equals(_inReport, value)) return;
                _inReport = value;
                OnPropertyChanged();
            }
        }

        public bool AllowEdit => !SalesUnit.OrderIsTaken;

        /// <summary>
        /// ��������� ���� ������
        /// </summary>
        public double Sum => EstimatedCost * Amount;

        /// <summary>
        /// ����������� ���� ���
        /// </summary>
        public DateTime OriginalOrderInTakeDate { get; }

        /// <summary>
        /// ����������������� ���� ���
        /// </summary>
        public DateTime EstimatedOrderInTakeDate
        {
            get { return _estimatedOrderInTakeDate; }
            set
            {
                if (!AllowEdit) return;
                if (value < DateTime.Today) return;

                if (Equals(_estimatedOrderInTakeDate, value)) return;

                _estimatedOrderInTakeDate = value;

                SalesUnits.ForEach(x => x.OrderInTakeDateInjected = value);
                SalesUnits.ForEach(x => x.StartProductionDateInjected = value);
                EstimatedRealizationDate = SalesUnit.RealizationDateCalculated;

                OnPropertyChanged();
                OnPropertyChanged(nameof(DifOitDays));
            }
        }

        /// <summary>
        /// ����� �� ���� ���
        /// </summary>
        public int DifOitDays => EstimatedOrderInTakeDate.MonthsBetween(OriginalOrderInTakeDate);

        /// <summary>
        /// ����������������� ��������� �����
        /// </summary>
        public double EstimatedCost
        {
            get { return _estimatedCost; }
            set
            {
                if (!AllowEdit) return;
                _estimatedCost = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sum));
            }
        }

        /// <summary>
        /// ����������� ���� ����������
        /// </summary>
        public DateTime OriginalRealizationDate { get; }

        /// <summary>
        /// ����������������� ���� ����������
        /// </summary>
        public DateTime EstimatedRealizationDate
        {
            get { return _estimatedRealizationDate; }
            set
            {
                if (!AllowEdit) return;
                if (value < EstimatedOrderInTakeDate) return;
                _estimatedRealizationDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DifRealizationDays));
            }
        }

        /// <summary>
        /// ����� �� ���� ����������
        /// </summary>
        public int DifRealizationDays => EstimatedRealizationDate.MonthsBetween(OriginalRealizationDate);

        /// <summary>
        /// �������������� ������� ������
        /// </summary>
        public PaymentConditionSet EstimatedPaymentConditionSet
        {
            get { return _estimatedPaymentConditionSet; }
            set
            {
                if (!AllowEdit) return;
                _estimatedPaymentConditionSet = value;
                OnPropertyChanged();
            }
        }

        public FlatReportItem(IEnumerable<SalesUnit> salesUnits)
        {
            if(!salesUnits.Any())
                throw new ArgumentException(nameof(salesUnits));
            SalesUnits = salesUnits.ToList();

            var salesUnit = salesUnits.First();

            _estimatedCost = salesUnit.Cost;
            _estimatedOrderInTakeDate = OriginalOrderInTakeDate = salesUnit.OrderInTakeDate;
            _estimatedRealizationDate = OriginalRealizationDate = salesUnit.RealizationDateCalculated;
            EstimatedPaymentConditionSet = salesUnit.PaymentConditionSet;
        }

        /// <summary>
        /// �������� � ������ ����������������� ���� � �������� ���� ���
        /// </summary>
        public void InjectDates()
        {
            SalesUnits.ForEach(x =>
            {
                if (!Equals(OriginalOrderInTakeDate, EstimatedOrderInTakeDate))
                {
                    x.OrderInTakeDateInjected = EstimatedOrderInTakeDate;
                    x.StartProductionDateInjected = EstimatedOrderInTakeDate;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}