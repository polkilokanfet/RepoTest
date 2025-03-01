using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectWrapper1 : WrapperBase<Project>
    {
        #region SimpleProperties

        //Name
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string NameOriginalValue => GetOriginalValue<string>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        //InWork
        public bool InWork
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool InWorkOriginalValue => GetOriginalValue<bool>(nameof(InWork));
        public bool InWorkIsChanged => GetIsChanged(nameof(InWork));

        //ForReport
        public bool ForReport
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool ForReportOriginalValue => GetOriginalValue<bool>(nameof(ForReport));
        public bool ForReportIsChanged => GetIsChanged(nameof(ForReport));

        /// <summary>
        /// ProjectTypeId
        /// </summary>
        public System.Guid ProjectTypeId
        {
            get => Model.ProjectTypeId;
            set => SetValue(value);
        }
        public System.Guid ProjectTypeIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ProjectTypeId));
        public bool ProjectTypeIdIsChanged => GetIsChanged(nameof(ProjectTypeId));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Тип проекта
        /// </summary>
        public ProjectTypeSimpleWrapper ProjectType
        {
            get => GetWrapper<ProjectTypeSimpleWrapper>();
            set => SetComplexValue<ProjectType, ProjectTypeSimpleWrapper>(ProjectType, value);
        }

        #endregion


        #region CollectionProperties

        public ProjectUnitGroupsContainer Units { get; private set; }

        #endregion

        public ProjectWrapper1(Project model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(ProjectType), Model.ProjectType == null ? null : new ProjectTypeSimpleWrapper(Model.ProjectType));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException($"{nameof(Model.SalesUnits)} cannot be null");
            Units = new ProjectUnitGroupsContainer(Model.SalesUnits.Where(salesUnit => salesUnit.IsRemoved == false));
            RegisterCollection(Units, Model.SalesUnits);
        }
    }
}