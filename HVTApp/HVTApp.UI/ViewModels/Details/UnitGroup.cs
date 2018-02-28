using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public class UnitGroup : BaseUnitsGroup<SalesUnitWrapper>, IUnitGroup
    {
        public UnitGroup(IEnumerable<SalesUnitWrapper> units) : base(units)
        {
        }

        public FacilityWrapper Facility
        {
            get { return GetValue<FacilityWrapper>(); }
            set { SetValue(value); }
        }

        public ProductWrapper Product
        {
            get { return GetValue<ProductWrapper>(); }
            set { SetValue(value); }
        }

        public DateTime DeliveryDateExpected
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public double MarginalIncome
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total => Groups.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

        public CompanyWrapper Producer
        {
            get { return GetValue<CompanyWrapper>(); }
            set { SetValue(value); }
        }
    }

    public class BaseUnitsGroup<TUnit> : INotifyPropertyChanged
        where TUnit : INotifyPropertyChanged
    {
        public ObservableCollection<TUnit> Groups { get; }
        public int Amount => Groups.Count;

        public BaseUnitsGroup(IEnumerable<TUnit> units)
        {
            Groups = new ObservableCollection<TUnit>(units);
            foreach (var projectUnit in Groups)
            {
                projectUnit.PropertyChanged += ProjectUnitOnPropertyChanged;
            }
        }

        private void ProjectUnitOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(String.Empty);
        }

        #region GetSetValue
        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var unit = Groups.First();
            return (T)unit.GetType().GetProperty(propertyName).GetValue(unit);
        }

        protected void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            bool changed = false;

            foreach (var projectUnit in Groups)
            {
                var currentValue = projectUnit.GetType().GetProperty(propertyName).GetValue(projectUnit);
                if (Equals(currentValue, value)) continue;

                projectUnit.GetType().GetProperty(propertyName).SetValue(projectUnit, value);
                changed = true;
            }

            if (changed)
                OnPropertyChanged(propertyName);
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
    }

    public class ProjectUnitGroupLookup : IProjectUnitGroupLookup
    {
        public ProjectUnitGroupLookup(IEnumerable<SalesUnitLookup> salesUnitLookups)
        {
            SalesUnitLookups = new ObservableCollection<SalesUnitLookup>(salesUnitLookups);
            foreach (var salesUnitLookup in SalesUnitLookups)
            {
                salesUnitLookup.PropertyChanged += ProjectUnitOnPropertyChanged;
            }
        }

        private void ProjectUnitOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(String.Empty);
        }

        public ObservableCollection<SalesUnitLookup> SalesUnitLookups { get; }

        public int Amount => SalesUnitLookups.Count;

        public FacilityLookup Facility => GetValue<FacilityLookup>();

        public ProductLookup Product => GetValue<ProductLookup>();

        public double Cost => GetValue<double>();

        public double MarginalIncome => GetValue<double>();

        public DateTime DeliveryDateExpected => GetValue<DateTime>();

        private T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var unit = SalesUnitLookups.First();
            return (T)unit.GetType().GetProperty(propertyName).GetValue(unit);
        }

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