using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates.ServiceRealizationDates
{
    public class ServiceRealizationDatesGroup : INotifyPropertyChanged
    {
        private DateTime? _realizationDate;

        public List<ServiceRealizationDatesItem> Units { get; }
        public SalesUnit Model => Units.First().Model;

        public DateTime? RealizationDate
        {
            get => _realizationDate;
            set
            {
                Units.ForEach(item => item.RealizationDate = value);
                _realizationDate = Units.First().RealizationDate;
            }
        }

        public bool HasFullInformation => Units.All(item => item.HasFullInformation);

        public ServiceRealizationDatesGroup(IEnumerable<ServiceRealizationDatesItem> items)
        {
            Units = items.ToList();
            Units.ForEach(item =>
            {
                item.SettedValueToProperty += () =>
                {
                    OnPropertyChanged(nameof(HasFullInformation));
                };
            });

            _realizationDate = Units.First().RealizationDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}