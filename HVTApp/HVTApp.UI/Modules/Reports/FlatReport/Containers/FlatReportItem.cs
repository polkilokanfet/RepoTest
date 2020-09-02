using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
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
                if (!AllowEditOit) return;
                if (Equals(_inReport, value)) return;
                _inReport = value;
                OnPropertyChanged();
                InReportIsChanged?.Invoke();
            }
        }

        public event Action InReportIsChanged;

        public bool AllowEditOit => !SalesUnit.OrderIsTaken && !IsLoosen;

        public bool AllowEditRealization => !SalesUnit.OrderIsRealized && !IsLoosen;

        public bool IsLoosen => SalesUnit.IsLoosen;

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
                if (!AllowEditOit) return;
                if (value < DateTime.Today) return;

                if (Equals(_estimatedOrderInTakeDate, value)) return;

                if (!Equals(_estimatedOrderInTakeDate.Year, value.Year) ||
                    !Equals(_estimatedOrderInTakeDate.Month, value.Month))
                {
                    _estimatedOrderInTakeDate = value;
                    EstimatedOrderInTakeMonthIsChanged?.Invoke();
                }

                _estimatedOrderInTakeDate = value;

                SalesUnits.ForEach(x => x.OrderInTakeDateInjected = value);
                SalesUnits.ForEach(x => x.StartProductionDateInjected = value);
                EstimatedRealizationDate = SalesUnit.RealizationDateCalculated;

                OnPropertyChanged();
                OnPropertyChanged(nameof(DifOitDays));
            }
        }

        public event Action EstimatedOrderInTakeMonthIsChanged;

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
                if (!AllowEditOit) return;
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
                if (!AllowEditRealization) return;
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
                if (!AllowEditOit) return;
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
            _estimatedPaymentConditionSet = salesUnit.PaymentConditionSet;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// �������� ����� � ����������� ������� (��� ����� �� ���������!)
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public IEnumerable<SalesUnit> GetSalesUnitsWithInjactedData(IUnitOfWork unitOfWork)
        {
            foreach (var salesUnit in SalesUnits)
            {
                var salesUnitWithInjactedData = unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id);

                //��������� ���������
                if (!Equals(EstimatedCost, salesUnit.Cost))
                {
                    salesUnitWithInjactedData.Cost = EstimatedCost;
                }

                //��������� ���� ���
                if (!Equals(OriginalOrderInTakeDate, EstimatedOrderInTakeDate))
                {
                    salesUnitWithInjactedData.OrderInTakeDateInjected = EstimatedOrderInTakeDate;
                    salesUnitWithInjactedData.StartProductionDate = EstimatedOrderInTakeDate;
                }

                //��������� ���� ����������
                if (!Equals(OriginalRealizationDate, EstimatedRealizationDate))
                {
                    salesUnitWithInjactedData.EndProductionDate = EstimatedRealizationDate;
                    salesUnitWithInjactedData.ShipmentDate = EstimatedRealizationDate;
                    salesUnitWithInjactedData.RealizationDate = EstimatedRealizationDate;
                    salesUnitWithInjactedData.DeliveryDate = null;
                }

                yield return salesUnitWithInjactedData;
            }
        }
    }
}