using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class GroupsCollection<TModel, TGroup, TMember> : ValidatableChangeTrackingCollection<TGroup>
        where TModel : class, IUnit
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TMember : class, IGroupValidatableChangeTracking<TModel>
    {
        private TGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        public event Action<TGroup> SelectedGroupChanged;

        public event Action<ProductIncludedWrapper> SelectedProductIncludedChanged; 

        /// <summary>
        /// Выбранная группа
        /// </summary>
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

        /// <summary>
        /// Выбранный зависимый продукт.
        /// </summary>
        public ProductIncludedWrapper SelectedProductIncluded
        {
            get { return _selectedProductIncluded; }
            set
            {
                if (Equals(_selectedProductIncluded, value)) return;
                _selectedProductIncluded = value;
                SelectedProductIncludedChanged?.Invoke(SelectedProductIncluded);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedProductIncluded)));
            }
        }

        public GroupsCollection(IEnumerable<TGroup> groups, bool isNew) : base(groups)
        {
            if (isNew)
            {
                this.Clear();
                this.AcceptChanges();
                groups.ForEach(this.Add);
            }
        }
    }
}