using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class GroupsCollection<TModel, TGroup, TMember> : ValidatableChangeTrackingCollection<TGroup>
        where TModel : class, IUnit
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TMember : class, IGroupValidatableChangeTracking<TModel>
    {
        private TGroup _selectedGroup;

        public event Action<TGroup> SelectedGroupChanged;

        public TGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                SelectedGroupChanged?.Invoke(SelectedGroup);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedGroup)));
            }
        }

        public GroupsCollection() : base(new List<TGroup>())
        {
        }

        public GroupsCollection(IEnumerable<TGroup> items) : base(items)
        {
        }
    }
}