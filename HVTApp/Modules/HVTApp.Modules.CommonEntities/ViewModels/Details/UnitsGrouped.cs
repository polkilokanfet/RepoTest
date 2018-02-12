using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public abstract class UnitsGrouped<TGroupingProduct> : INotifyPropertyChanged
        where TGroupingProduct : IGroupingProduct
    {
        public ObservableCollection<TGroupingProduct> UnitWrappers { get; }

        protected UnitsGrouped(IEnumerable<TGroupingProduct> unitWrappers)
        {
            UnitWrappers = new ObservableCollection<TGroupingProduct>(unitWrappers);
            foreach (var projectUnitWrapper in UnitWrappers)
            {
                projectUnitWrapper.PropertyChanged += UnitWrapperOnPropertyChanged;
            }
        }

        private void UnitWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var propInfo = this.GetType().GetProperty(e.PropertyName);
            if (propInfo != null)
                OnPropertyChanged(e.PropertyName);
        }

        public virtual ProductWrapper Product
        {
            get { return GetValue<ProductWrapper>(); }
            set { SetValue(value); }
        }

        public virtual FacilityWrapper Facility
        {
            get { return GetValue<FacilityWrapper>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double MarginalIncome
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var unit = UnitWrappers.First();
            return (T)unit.GetType().GetProperty(propertyName).GetValue(unit);
        }

        protected void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(GetValue<object>(propertyName), value))
                return;

            var unit = UnitWrappers.First();
            var propertyInfo = unit.GetType().GetProperty(propertyName);
            foreach (var projectUnitWrapper in UnitWrappers)
            {
                propertyInfo.SetValue(projectUnitWrapper, value);
                OnPropertyChanged(propertyName);
            }
        }

        public int Amount => UnitWrappers.Count;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}