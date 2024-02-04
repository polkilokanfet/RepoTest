using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2
{
    //действия, когда прилетают события из сервера синхронизации
    public partial class EventServiceClient
    {
        /// <summary>
        /// Реакция сервиса-клиента на остановку сервиса-сервера
        /// </summary>
        public void OnServiceDisposeEvent()
        {
            this.StopWaitRestart();
        }

        public bool IsAlive()
        {
            return true;
        }

        /// <summary>
        /// Скопировать приложения к проекту
        /// </summary>
        /// <param name="projectId">Id проекта</param>
        /// <param name="targetDirectory">Куда копировать</param>
        /// <returns></returns>
        public void CopyProjectAttachmentsCallback(Guid projectId, string targetDirectory)
        {
            Project project = _container.Resolve<IUnitOfWork>().Repository<Project>().GetById(projectId);
            _filesStorageService.CopyDirectory(_fileManagerService.GetPath(project), targetDirectory);
        }

        #region Directum

        public bool OnSaveDirectumTaskServiceCallback(Guid taskId)
        {
            return true;

            //var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask))
            //{
            //    return true;
            //}

            //return false;
        }

        public bool OnStartDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            string title = "Вам поручена задача в DirectumLite";
            string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
            _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

            return true;
        }

        public bool OnStopDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            string title = "Остановлена задача в DirectumLite";
            string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
            _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

            return true;
        }

        public bool OnPerformDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            string title = "Выполнена задача в DirectumLite";
            string message = $"Исполнитель: {directumTask.Performer}\nТема: \"{directumTask.Group.Title}\"";
            _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

            return true;
        }

        public bool OnAcceptDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            string title = "Принята задача в DirectumLite";
            string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
            _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

            return true;
        }

        public bool OnRejectDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            string title = "Не принята задача в DirectumLite";
            string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
            _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

            return true;
        }

        #endregion

        #region IncomingRequest

        public bool OnSaveIncomingRequestServiceCallback(Guid requestId)
        {
            //TODO: implement
            return false;

            //var request = _container.Resolve<IUnitOfWork>().Repository<IncomingRequest>().GetById(requestId);

            //var canInstruct = GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director;

            //var canPerform = GlobalAppProperties.UserIsManager &&
            //                 request.Performers.Any(employee => employee.Id == GlobalAppProperties.User.Employee.Id);

            //if (canInstruct || canPerform)
            //{
            //    this.SyncContainer.PublishWithinAppForCurrentUser<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

            //    string message = $"{request.Document.Comment}";
            //    var action = new Action(() =>
            //    {
            //        _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestsView>(new NavigationParameters());
            //    });
            //    _popupNotificationsService.ShowPopupNotification(request, message, "Запрос");
        }
    
        public bool OnSaveIncomingDocumentServiceCallback(Guid documentId)
        {
            //TODO: implement
            return false;

            //var unitOfWork = _container.Resolve<IUnitOfWork>();
            //var document = unitOfWork.Repository<Document>().GetById(documentId);

            //var canInstruct = (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
            //                  GlobalAppProperties.User.RoleCurrent == Role.Director) &&
            //                  document.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany?.Id;

            //if (canInstruct)
            //{
            //    var request = new IncomingRequest { Document = document };
            //    this.SyncContainer.PublishWithinAppForCurrentUser<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

            //    string message = $"{document.Comment}";
            //    var action = new Action(() =>
            //    {
            //        _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestView>(
            //            new NavigationParameters
            //            {
            //                {"Model", request},
            //                {"UnitOfWork", unitOfWork}
            //            });

            //    });
            //    //Popup.Popup.ShowPopup(message, "Запрос", action);
        }

        #endregion

        #region TechnicalRequarementsTask

        public bool OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            return true;

            //TechnicalRequrementsTask technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            //if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            //{
            //    return true;
            //}

            //return false;
        }

        public bool OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            string message = null;

            //если текущий пользователь BackManagerBoss
            if (GlobalAppProperties.UserIsBackManagerBoss && technicalRequrementsTask.BackManager == null)
            {
                if (technicalRequrementsTask.Start.HasValue)
                {
                    message = $"Необходимо поручить задачу ТСЕ (инициатор: {technicalRequrementsTask.FrontManager.Employee.Person})";
                }
            }

            //если текущий пользователь Back-Менеджер
            else if (GlobalAppProperties.UserIsBackManager && technicalRequrementsTask.BackManager != null)
            {
                if (technicalRequrementsTask.BackManager.IsAppCurrentUser())
                {
                    message = $"Рестартована задача (инициатор: {technicalRequrementsTask.FrontManager})";
                }
            }

            if (message != null)
            {
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message,
                    technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        public bool OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            string message = null;

            if (GlobalAppProperties.UserIsBackManager)
            {
                message = $"Вам поручена задача ТСЕ (инициатор: {technicalRequrementsTask.FrontManager})";
            }

            else if (GlobalAppProperties.UserIsManager)
            {
                message = $"Задача ТСЕ поручена (back-manager: {technicalRequrementsTask.BackManager})";
            }


            if (message != null)
            {
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message,
                    technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        public bool OnStopTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            if (GlobalAppProperties.UserIsBackManager)
            {
                var message = $"Задача ТСЕ остановлена (инициатор: {technicalRequrementsTask.FrontManager.Employee.Person})";
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message,
                    technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        public bool OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            if (GlobalAppProperties.UserIsManager)
            {
                var message = $"Задача ТСЕ отклонена (back-manager: {technicalRequrementsTask.BackManager.Employee.Person})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}";
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message,
                    technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        public bool OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (GlobalAppProperties.UserIsBackManager)
            {
                var message = $"Проработка задачи ТСЕ отклонена (front-manager: {technicalRequrementsTask.FrontManager.Employee.Person})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}";
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message,
                    technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        public bool OnFinishTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            {
                if (GlobalAppProperties.UserIsManager)
                {
                    string message = $"Завершена проработка задачи ТСЕ (back-manager: {technicalRequrementsTask.BackManager.Employee.Person})";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnAcceptTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (GlobalAppProperties.UserIsBackManager)
            {
                var message = $"Задача ТСЕ принята (front-manager: {technicalRequrementsTask.FrontManager.Employee.Person})";
                _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                return true;
            }

            return false;
        }

        #endregion

        #region PriceCalculation

        public bool OnSavePriceCalculationServiceCallback(Guid calculationId)
        {
            return true;
            //var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            //if (this.SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterSavePriceCalculationEvent>(calculation))
            //{
            //    return true;
            //}

            //return false;
        }

        /// <summary>
        /// Реакция на старт расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public bool OnStartPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var message = $"Запущен: {calculation.Name}";
            var title = $"{calculation.Name} с Id {calculation.Id}";
            _popupNotificationsService.ShowPopupNotification(calculation, message, title);
            return true;
        }

        /// <summary>
        /// Реакция на завершение расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public bool OnFinishPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var message = $"Завершён: {calculation.Name}";
            var title = $"{calculation.Name} с Id {calculation.Id}";
            _popupNotificationsService.ShowPopupNotification(calculation, message, title);
            return true;
        }

        /// <summary>
        /// Реакция на остановку расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public bool OnCancelPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var message = $"Остановлен: {calculation.Name}";
            var title = $"{calculation.Name} с Id {calculation.Id}";
            _popupNotificationsService.ShowPopupNotification(calculation, message, title);
            return true;
        }

        /// <summary>
        /// Реакция на отклонение расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public bool OnRejectPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var message = $"Отклонен: {calculation.Name}\nКомментарий: {calculation.LastHistoryItem.Comment}";
            var title = $"{calculation.Name} с Id {calculation.Id}";
            _popupNotificationsService.ShowPopupNotification(calculation, message, title);
            return true;
        }

        #endregion

        public bool OnPriceEngineeringNotificationServiceCallback(Guid priceEngineeringTaskId, string message)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        #region PriceEngineeringTasks

        public bool OnPriceEngineeringTasksStartServiceCallback(Guid priceEngineeringTasksId)
        {
            var priceEngineeringTasks = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasksId);

            var message = $"{priceEngineeringTasks.UserManager} запустил: {priceEngineeringTasks}";
            var title = $"{priceEngineeringTasks} с Id {priceEngineeringTasks.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTasks, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskStartServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Перезапущено: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskStopServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Остановлено: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskInstructServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Поручено: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskFinishServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Проработано: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskFinishGoToVerificationServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Проработано: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskVerificationRejectedByHeadServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Возвращено на дороботку начальником отдела: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskVerificationAcceptedByHeadServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Принято руководителем КБ: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskRejectByManagerServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Отклонено менеджером: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskRejectByConstructorServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Отклонено: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }

        public bool OnPriceEngineeringTaskAcceptServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            var message = $"Принято: {priceEngineeringTask}";
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }


        public bool OnPriceEngineeringTaskSendMessageServiceCallback(Guid messageId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var taskMessage = unitOfWork.Repository<PriceEngineeringTaskMessage>().GetById(messageId);

            var message = $"{taskMessage.Message}";
            var title = $"Сообщение от {taskMessage.Author}";
            var priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>()
                .GetById(taskMessage.PriceEngineeringTaskId);
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            //переводим в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    _container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskReciveMessageEvent>()
                        .Publish(taskMessage);
                });

            return true;
        }


        #endregion


        public bool OnSavePaymentDocumentServiceCallback(Guid paymentDocumentId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var paymentDocument = unitOfWork.Repository<PaymentDocument>().GetById(paymentDocumentId);

            var message = $"123";
            var title = $"Сохранено п/п №{paymentDocument.Number} от {paymentDocument.Date.ToShortDateString()} г.";
            _popupNotificationsService.ShowPopupNotification(paymentDocument, message, title);
            return true;
        }

        public bool PriceEngineeringTaskRejectByHeadPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return true;
        }
    }
}