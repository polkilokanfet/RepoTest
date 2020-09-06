using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class ContainerYear : BindableBase
    {
        private double _accuracy;
        public ObservableCollection<ContainerMonth> MonthContainers { get; } = new ObservableCollection<ContainerMonth>();
        public int Year => MonthContainers.First().Year;
        public double Sum => MonthContainers.Where(x => x.InReport).Sum(x => x.CurrentSum);
        public double TargetSum
        {
            get { return MonthContainers.Where(x => x.InReport).Sum(x => x.TargetSum); }
            set
            {
                if (Math.Abs(value - TargetSum) > 0.001)
                {
                    var sum = value - MonthContainers.Where(x => x.InReport && x.IsPast).Sum(x => x.TargetSum);
                    if (sum > 0)
                    {
                        var monthContainers = MonthContainers.Where(container => container.InReport && !container.IsPast).ToList();
                        foreach (var monthContainer in monthContainers)
                        {
                            monthContainer.TargetSum = sum / monthContainers.Count;
                        }
                    }
                }
            }
        }

        public bool InReport => MonthContainers.Any(x => x.InReport);

        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                _accuracy = value;
                MonthContainers.ForEach(x => x.Accuracy = _accuracy);
            }
        }

        public ContainerYear(int year, double accuracy)
        {
            Accuracy = accuracy;

            for (int month = 1; month <= 12; month++)
            {
                MonthContainers.Add(new ContainerMonthOit(new DateTime(year, month, 1), Accuracy));                
            }

            foreach (var monthContainer in MonthContainers)
            {
                monthContainer.InReportIsChanged += () => OnPropertyChanged(nameof(InReport));
                monthContainer.CurrentSumIsChanged += () => OnPropertyChanged(nameof(Sum));
                monthContainer.TargetSumIsChanged += () => OnPropertyChanged(nameof(TargetSum));
            }
        }
    }
}