using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectUnitGroupsCollection: ValidatableChangeTrackingCollection<ProjectUnitGroup>
    {
        //private ProjectUnitGroup _selectedGroup;
        private ProductIncludedWrapper _selectedProductIncluded;

        #region EventsRegion

        public event Action SumChanged;
        public event Action<ProjectUnitGroup> SelectedGroupChanged;
        public event Action<ProductIncludedWrapper> SelectedProductIncludedChanged; 

        #endregion

        /// <summary>
        /// Выбранная группа
        /// </summary>
        //public ProjectUnitGroup SelectedGroup
        //{
        //    get { return _selectedGroup; }
        //    set
        //    {
        //        if (Equals(_selectedGroup, value)) return;
        //        _selectedGroup = value;
        //        SelectedGroupChanged?.Invoke(SelectedGroup);
        //        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedGroup)));
        //    }
        //}

        public IProjectUnit SelectedItem { get; set; }

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

        public ProjectUnitGroupsCollection(IEnumerable<ProjectUnitGroup> groups, bool isNew) : base(groups)
        {
            if (isNew)
            {
                this.Clear();
                this.AcceptChanges();
                groups.ForEach(this.Add);
            }

            //для отслеживания общей суммы
            //this.CollectionChanged += (sender, args) =>
            //{
            //    SumChanged?.Invoke();

            //    if (args.NewItems == null)
            //        return;

            //    foreach (var projectUnitGroup in args.NewItems.Cast<ProjectUnitGroup>())
            //    {
            //        projectUnitGroup.PropertyChanged += GrpOnPropertyChanged;
            //    }
            //};
            //this.ForEach(x => x.PropertyChanged += GrpOnPropertyChanged);
        }

        //private void GrpOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    if(args.PropertyName == nameof(IGroupValidatableChangeTracking<TModel>.Total))
        //        SumChanged?.Invoke();
        //}
    }
}