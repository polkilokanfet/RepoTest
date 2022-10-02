using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPaymentDocument : SyncUnit<PaymentDocument, AfterSaveActualPaymentDocumentEvent>
    {
        public SyncPaymentDocument(IUnityContainer container) : base(container)
        {
        }

        public override bool IsTargetUser(User user, PaymentDocument model)
        {
            var salesUnits = model.Payments.Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.SalesUnitId));
            return salesUnits.Any(salesUnit => salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id);
        }

        protected override IEnumerable<Role> GetRolesForNotification()
        {
            yield return Role.SalesManager;
        }

        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
        {
            get
            {
                return (eventSourceAppSessionId, targetUserId, paymentDocumentId) => EventServiceHost.SavePaymentDocumentPublishEvent(eventSourceAppSessionId, targetUserId, paymentDocumentId);
            }
        }

        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SavePaymentDocument;
    }
}