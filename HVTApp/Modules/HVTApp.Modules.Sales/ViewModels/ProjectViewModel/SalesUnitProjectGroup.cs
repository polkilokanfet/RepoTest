using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitProjectGroup : IValidatableChangeTracking
    {
        public IValidatableChangeTrackingCollection<SalesUnitProjectItem> Units { get; }
        public SalesUnitProjectItem Unit => Units.First();
        public int Amount => Units.Count;

        public SalesUnitProjectGroup(IEnumerable<SalesUnitProjectItem> salesUnits)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitProjectItem>(salesUnits);

            Units.PropertyChanged += (sender, args) =>
            {
                PropertyChanged?.Invoke(this, args);
            };

            Units.CollectionChanged += (sender, args) =>
            {
                if (args.Action != NotifyCollectionChangedAction.Add &&
                    args.Action != NotifyCollectionChangedAction.Remove)
                    return;

                OnPropertyChanged(nameof(Unit));
                OnPropertyChanged(nameof(Amount));
            };
        }

        public bool IsChanged => Units.IsChanged;
        public bool IsValid => Units.IsValid;


        public void AcceptChanges()
        {
            Units.AcceptChanges();
        }

        public void RejectChanges()
        {
            Units.RejectChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}