using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectWrapper1 : WrapperBase<Project>
    {
        public ProjectWrapper1(Project model) : base(model) { }

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

        #endregion

        #region ComplexProperties

        public ProjectTypeSimpleWrapper ProjectType
        {
            get => GetWrapper<ProjectTypeSimpleWrapper>();
            set => SetComplexValue<ProjectType, ProjectTypeSimpleWrapper>(ProjectType, value);
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(ProjectType), Model.ProjectType == null ? null : new ProjectTypeSimpleWrapper(Model.ProjectType));
        }

    }
}