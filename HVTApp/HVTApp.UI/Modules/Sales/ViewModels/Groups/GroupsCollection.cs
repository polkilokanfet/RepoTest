using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class GroupsCollection<TModel, TGroup, TMember> : ValidatableChangeTrackingCollection<TGroup>
        where TModel : class, IUnit
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TMember : class, IGroupValidatableChangeTracking<TModel>
    {
        private TGroup _selectedGroup;
        private ProductIncludedSimpleWrapper _selectedProductIncluded;

        public event Action SumChanged;

        public event Action<TGroup> SelectedGroupChanged;

        public event Action<ProductIncludedSimpleWrapper> SelectedProductIncludedChanged; 

        /// <summary>
        /// Выбранная группа
        /// </summary>
        public TGroup SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                SelectedGroupChanged?.Invoke(SelectedGroup);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedGroup)));
            }
        }

        public object[] SelectedGroups { get; set; }

        /// <summary>
        /// Выбранный зависимый продукт.
        /// </summary>
        public ProductIncludedSimpleWrapper SelectedProductIncluded
        {
            get => _selectedProductIncluded;
            set
            {
                if (Equals(_selectedProductIncluded?.Model, value?.Model)) return;
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

            //для отслеживания общей суммы
            this.CollectionChanged += (sender, args) =>
            {
                SumChanged?.Invoke();

                if (args.NewItems == null) return;
                foreach (var grp in args.NewItems.Cast<TGroup>())
                {
                    grp.PropertyChanged += GrpOnPropertyChanged;
                }
            };
            this.ForEach(x => x.PropertyChanged += GrpOnPropertyChanged);
        }

        private void GrpOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == nameof(IGroupValidatableChangeTracking<TModel>.Total))
                SumChanged?.Invoke();
        }
    }
}