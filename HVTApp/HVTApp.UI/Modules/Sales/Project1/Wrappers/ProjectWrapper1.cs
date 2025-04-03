using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

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
        public Guid ProjectTypeId
        {
            get => Model.ProjectTypeId;
            set => SetValue(value);
        }
        public Guid ProjectTypeIdOriginalValue => GetOriginalValue<Guid>(nameof(ProjectTypeId));
        public bool ProjectTypeIdIsChanged => GetIsChanged(nameof(ProjectTypeId));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Менеджер
        /// </summary>
        public UserEmptyWrapper Manager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(Manager, value);
        }

        #endregion

        #region CollectionProperties

        public ProjectUnitGroupsContainer Units { get; private set; }

        #endregion

        public ProjectWrapper1(Project model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Manager), Model.Manager == null ? null : new UserEmptyWrapper(Model.Manager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException($"{nameof(Model.SalesUnits)} cannot be null");
            Units = new ProjectUnitGroupsContainer(Model.SalesUnits);
            RegisterCollection(Units, Model.SalesUnits);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Название проекта не может быть пустым", new[] { nameof(Name) });
        }
    }
}