using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper
{
    public partial class TechnicalRequrementsTask2Wrapper : WrapperBase<TechnicalRequrementsTask>
    {
        #region SimpleProperties

        public string ProjectName { get; }

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public string TceNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string TceNumberOriginalValue => GetOriginalValue<string>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        public DateTime? Start
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? StartOriginalValue => GetOriginalValue<DateTime?>(nameof(Start));
        public bool StartIsChanged => GetIsChanged(nameof(Start));

        #endregion

        #region ComplexProperties

        public UserWrapper BackManager
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(BackManager, value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<TechnicalRequrements2Wrapper> Requrements { get; private set; }

        #endregion

        public TechnicalRequrementsTask2Wrapper(TechnicalRequrementsTask model) : base(model)
        {
            ProjectName = model.Requrements.First().SalesUnits.First().Project.Name;
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserWrapper(Model.BackManager));
        }

        protected override void InitializeCollectionProperties()
        {

            if (Model.Requrements == null) throw new ArgumentException("Requrements cannot be null");
            Requrements = new ValidatableChangeTrackingCollection<TechnicalRequrements2Wrapper>(Model.Requrements.Select(e => new TechnicalRequrements2Wrapper(e)));
            RegisterCollection(Requrements, Model.Requrements);
        }

    }
}