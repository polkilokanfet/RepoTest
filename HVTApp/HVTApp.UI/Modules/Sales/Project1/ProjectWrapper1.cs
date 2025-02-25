using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using System;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1
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

        #region CollectionProperties

        public ProjectUnitGroupsContainer Units { get; private set; }

        #endregion

        public ProjectWrapper1(Project model) : base(model) { }

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException($"{nameof(Model.SalesUnits)} cannot be null");
            Units = new ProjectUnitGroupsContainer(Model.SalesUnits);
            RegisterCollection(Units, Model.SalesUnits);
        }
    }
}