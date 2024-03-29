using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure.Annotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitOrderGroup : INotifyPropertyChanged, ISalesUnitOrder
    {
        private DateTime? _signalToStartProductionDone;
        private DateTime? _endProductionPlanDate;
        private OrderWrapper _order;

        public ObservableCollection<SalesUnitOrderItem> Units { get; }

        public SalesUnit Unit => Units.First().Model;

        public int Amount => Units.Count;

        public DateTime? SignalToStartProductionDone
        {
            get => _signalToStartProductionDone;
            set
            {
                _signalToStartProductionDone = value;
                Units.ForEach(salesUnitOrderItem => salesUnitOrderItem.SignalToStartProductionDone = value);
                OnPropertyChanged();
            }
        }

        public DateTime? EndProductionPlanDate
        {
            get => _endProductionPlanDate;
            set
            {
                _endProductionPlanDate = value;
                Units.ForEach(salesUnitOrderItem => salesUnitOrderItem.EndProductionPlanDate = value);
                OnPropertyChanged();
            }
        }

        public OrderWrapper Order
        {
            get { return _order; }
            set
            {
                _order = value;
                Units.ForEach(x => x.Order = value);
            }
        }

        public DateTime EndProductionDateExpected => Units.First().EndProductionDateExpected;


        public SalesUnitOrderGroup(IEnumerable<SalesUnitOrderItem> salesUnits)
        {
            Units = new ObservableCollection<SalesUnitOrderItem>(salesUnits);
            _endProductionPlanDate = Units.First().EndProductionPlanDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}