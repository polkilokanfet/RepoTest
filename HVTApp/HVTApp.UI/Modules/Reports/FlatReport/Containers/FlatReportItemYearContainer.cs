using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class FlatReportItemYearContainer : BindableBase
    {
        private double _accuracy;
        private bool _inReport;
        public ObservableCollection<FlatReportItemMonthContainer> MonthContainers { get; } = new ObservableCollection<FlatReportItemMonthContainer>();
        public int Year => MonthContainers.First().Year;
        public double Sum => MonthContainers.Sum(x => x.CurrentSum);
        public double TargetSum => MonthContainers.Sum(x => x.TargetSum);

        public bool InReport
        {
            get { return _inReport; }
            set
            {
                _inReport = value;
                OnPropertyChanged();
            }
        }

        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                _accuracy = value;
                MonthContainers.ForEach(x => x.Accuracy = _accuracy);
            }
        }

        public FlatReportItemYearContainer(int year)
        {
            for (int month = 1; month <= 12; month++)
            {
                MonthContainers.Add(new MonthContainerOit(new DateTime(year, month, 1), Accuracy));                
            }

            foreach (var monthContainer in MonthContainers)
            {
                monthContainer.CurrentSumIsChanged += () => OnPropertyChanged(nameof(Sum));
                monthContainer.TargetSumIsChanged += () => OnPropertyChanged(nameof(TargetSum));
            }
        }
    }
}