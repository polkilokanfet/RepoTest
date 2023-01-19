using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public abstract class PriceEngineeringTasksContainerWrapper<TPriceEngineeringTaskViewModel> : WrapperBase<PriceEngineeringTasks>
        where TPriceEngineeringTaskViewModel : TaskViewModel<>
    {
        /// <summary>
        /// Задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<TPriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; protected set; }

        #region CollectionProperties

        /// <summary>
        /// Файлы технических требований (общие)
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }

        #endregion

        #region ctors

        /// <summary>
        /// Для создания новой ТСП
        /// </summary>
        protected PriceEngineeringTasksContainerWrapper() : base(new PriceEngineeringTasks())
        {
        }

        protected PriceEngineeringTasksContainerWrapper(PriceEngineeringTasks model, IUnityContainer container) : base(model)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TPriceEngineeringTaskViewModel>(this.GetChildPriceEngineeringTasks(container));
            //RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        #endregion

        protected abstract IEnumerable<TPriceEngineeringTaskViewModel> GetChildPriceEngineeringTasks(IUnityContainer container);

        protected override void InitializeCollectionProperties()
        {
            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTasksFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);
        }
    }
}