using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectWithGroupsWrapper : WrapperBase<Project>
    {
        public ProjectWithGroupsWrapper(Project model, IEnumerable<SalesUnit> salesUnits) : base(model)
        {
            var salesUnits1 = salesUnits.ToList();
            if (salesUnits1 == null) throw new ArgumentException("SalesUnits cannot be null");

            //группировка юнитов
            var groups = salesUnits1.GroupBy(x => x, new SalesUnitsGroupsComparer());

            //создание групп
            SalesUnitGroups = new ValidatableChangeTrackingCollection<SalesUnitProjectGroup>
                (
                    groups.Select(group => new SalesUnitProjectGroup(group.Select(x => new SalesUnitProjectItem(x))))
                );

            SalesUnitGroups.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(IsChanged)) OnPropertyChanged(nameof(IsChanged));
                if (args.PropertyName == nameof(IsValid)) OnPropertyChanged(nameof(IsValid));
            };

            SalesUnitGroups.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(IsChanged));
                OnPropertyChanged(nameof(IsValid));
            };
        }

        public override bool IsValid => base.IsValid && SalesUnitGroups.IsValid;
        public override bool IsChanged => base.IsChanged || SalesUnitGroups.IsChanged;

        #region SimpleProperties

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string NameOriginalValue => GetOriginalValue<string>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));


        public bool InWork
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool InWorkOriginalValue => GetOriginalValue<bool>(nameof(InWork));
        public bool InWorkIsChanged => GetIsChanged(nameof(InWork));

        public bool ForReport
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool ForReportOriginalValue => GetOriginalValue<bool>(nameof(ForReport));
        public bool ForReportIsChanged => GetIsChanged(nameof(ForReport));

        #endregion

        #region ComplexProperties

        public ProjectTypeWrapper ProjectType
        {
            get { return GetWrapper<ProjectTypeWrapper>(); }
            set { SetComplexValue<ProjectType, ProjectTypeWrapper>(ProjectType, value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<SalesUnitProjectGroup> SalesUnitGroups { get; }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(ProjectType), Model.ProjectType == null ? null : new ProjectTypeWrapper(Model.ProjectType));
        }
    }
}