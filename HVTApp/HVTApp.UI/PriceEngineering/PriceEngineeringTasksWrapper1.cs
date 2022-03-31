using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksWrapper1 : WrapperBase<PriceEngineeringTasks>
    {
        public PriceEngineeringTasksWrapper1(PriceEngineeringTasks model) : base(model) { }

        #region SimpleProperties

        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id => Model.Id;

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Менеджер
        /// </summary>
        public UserWrapper UserManager
        {
            get => GetWrapper<UserWrapper>();
            set => SetComplexValue<User, UserWrapper>(UserManager, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Файлы технических требований (общие)
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }
        
        /// <summary>
        /// Задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskWrapper> ChildPriceEngineeringTasks { get; private set; }
        
        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(UserManager), Model.UserManager == null ? null : new UserWrapper(Model.UserManager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTasksFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);
            
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskWrapper>(Model.ChildPriceEngineeringTasks.Select(e => new PriceEngineeringTaskWrapper(e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }
    }
}