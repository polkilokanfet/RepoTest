using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.PriceEngineering.Wrapper;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessagesWrapper : WrapperBase<PriceEngineeringTask>
    {
        /// <summary>
        /// Переписка
        /// </summary>
        public MessagesCollection Messages { get; }

        public PriceEngineeringTaskMessagesWrapper(PriceEngineeringTask model, IUnitOfWork unitOfWork) : base(model)
        {
            if (Model.Messages == null) throw new ArgumentException("Messages cannot be null");
            Messages = new MessagesCollection(Model.Messages.Select(taskMessage => new PriceEngineeringTaskMessageWrapper1(taskMessage)), unitOfWork);
            RegisterCollection(Messages, Model.Messages);
        }
    }
}