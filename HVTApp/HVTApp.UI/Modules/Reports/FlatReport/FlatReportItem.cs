using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportItem : INotifyPropertyChanged
    {
        private DateTime _estimatedOrderInTakeDate;
        private bool _inReport = true;

        public List<SalesUnit> SalesUnits { get; }
        public SalesUnit SalesUnit => SalesUnits.First();
        public int Amount => SalesUnits.Count;

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

        public bool AllowEdit => OriginalOrderInTakeDate >= DateTime.Today;

        /// <summary>
        /// ��������� ���� ������
        /// </summary>
        public double Sum { get; }

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
                if (Equals(_estimatedOrderInTakeDate, value)) return;
                _estimatedOrderInTakeDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DifDays));
            }
        }

        public int DifDays => (EstimatedOrderInTakeDate - OriginalOrderInTakeDate).Days;

        public FlatReportItem(IEnumerable<SalesUnit> salesUnits)
        {
            if(!salesUnits.Any())
                throw new ArgumentException(nameof(salesUnits));

            SalesUnits = salesUnits.ToList();
            OriginalOrderInTakeDate = SalesUnits.First().OrderInTakeDate;
            EstimatedOrderInTakeDate = OriginalOrderInTakeDate;
            Sum = SalesUnits.Sum(x => x.Cost);
        }

        /// <summary>
        /// �������� � ������ ����������������� ���� � �������� ���� ���
        /// </summary>
        public void InjectOrderInTakeDates()
        {
            SalesUnits.ForEach(x =>
            {
                if (!Equals(OriginalOrderInTakeDate, EstimatedOrderInTakeDate))
                    x.OrderInTakeDateInjected = EstimatedOrderInTakeDate;
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