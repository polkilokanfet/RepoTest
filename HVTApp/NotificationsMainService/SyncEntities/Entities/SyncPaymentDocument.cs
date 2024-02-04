//using System.Collections.Generic;
//using System.Linq;
//using HVTApp.Infrastructure;
//using HVTApp.Infrastructure.Interfaces.Services.EventService;
//using HVTApp.Model;
//using HVTApp.Model.Events;
//using HVTApp.Model.POCOs;
//using HVTApp.Model.Services;
//using Prism.Events;

//namespace NotificationsMainService.SyncEntities.Entities
//{
//    public class SyncPaymentDocument : SyncUnit<PaymentDocument, AfterSaveActualPaymentDocumentEvent>
//    {
//        public SyncPaymentDocument(IEventAggregator eventAggregator, INotificationFromDataBaseService notificationFromDataBaseService, IUnitOfWork unitOfWork, IEventServiceClient eventServiceClient) : base(eventAggregator, notificationFromDataBaseService, unitOfWork, eventServiceClient)
//        {
//        }

//        public override bool IsTargetUser(User user, PaymentDocument model)
//        {
//            var salesUnits = model.Payments.Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.SalesUnitId));
//            return salesUnits.Any(salesUnit => salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id);
//        }

//        protected override IEnumerable<Role> GetRolesForNotification()
//        {
//            yield return Role.SalesManager;
//        }

//        protected override ActionPublishThroughEventServiceForUserDelegate ActionPublishThroughEventServiceForUser
//        {
//            get
//            {
//                return (targetUserId, targetRole, paymentDocumentId) => EventServiceClient.SavePaymentDocumentPublishEvent(targetUserId, targetRole, paymentDocumentId);
//            }
//        }

//        protected override EventServiceActionType EventServiceActionType => EventServiceActionType.SavePaymentDocument;
//    }
//}