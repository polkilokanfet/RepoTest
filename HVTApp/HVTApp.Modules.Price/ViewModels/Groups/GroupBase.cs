using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public abstract class GroupBase<TGroup> : BindableBase
        where TGroup : GroupBase<TGroup>
    {
        private readonly SalesUnitWrapper _salesUnitWrapper;

        public SalesUnitWrapper Unit => _salesUnitWrapper ?? Groups.First().Unit;

        public ObservableCollection<TGroup> Groups { get; }

        public int Amount => Groups?.Count ?? 1;

        protected GroupBase(IEnumerable<SalesUnitWrapper> salesUnitWrappers)
        {
            var units = salesUnitWrappers as SalesUnitWrapper[] ?? salesUnitWrappers.ToArray();
            if (units.Length == 1)
            {
                _salesUnitWrapper = units.First();
                return;
            }

            Groups = new ObservableCollection<TGroup>(CreateGroups(units));
        }

        protected abstract IEnumerable<TGroup> CreateGroups(IEnumerable<SalesUnitWrapper> salesUnitWrappers);


        protected void SetValue(object newValue, [CallerMemberName] string propertyName = null)
        {
            // старое значение в свойстве
            var oldValue = this.GetType().GetProperty(propertyName).GetValue(this);

            // если старое значение равно новому, завершаем метод
            if (Equals(oldValue, newValue)) return;


            if (Groups != null)
            {
                Groups.ForEach(x => SetProperty(x, newValue, propertyName));
            }
            else
            {
                SetProperty(_salesUnitWrapper, newValue, propertyName);
            }

            //сообщаем об изменении
            OnPropertyChanged(propertyName);
        }

        private void SetProperty(object obj, object value, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            property.SetValue(obj, value);
        }
    }
}