using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessagesWrapper : WrapperBase<PriceEngineeringTask>
    {
        public PriceEngineeringTaskMessagesWrapper(PriceEngineeringTask model) : base(model) { }
        
        /// <summary>
        /// Переписка
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskMessageWrapper1> Messages { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Messages == null) throw new ArgumentException("Messages cannot be null");
            Messages = new ValidatableChangeTrackingCollection<PriceEngineeringTaskMessageWrapper1>(Model.Messages.Select(taskMessage => new PriceEngineeringTaskMessageWrapper1(taskMessage)));
            RegisterCollection(Messages, Model.Messages);
        }
    }
}