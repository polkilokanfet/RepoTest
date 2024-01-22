using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

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
            this.DisableWaitRestart();
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
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask))
            {
                return true;
            }

            return false;
        }

        public bool OnStartDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterStartDirectumTaskEvent>(directumTask))
            {
                string title = "Вам поручена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
                _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

                return true;
            }

            return false;
        }

        public bool OnStopDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterStopDirectumTaskEvent>(directumTask))
            {
                string title = "Остановлена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
                _popupNotificationsService.ShowPopupNotification(directumTask, message, title);
                
                return true;
            }

            return false;
        }

        public bool OnPerformDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterPerformDirectumTaskEvent>(directumTask))
            {
                string title = "Выполнена задача в DirectumLite";
                string message = $"Исполнитель: {directumTask.Performer}\nТема: \"{directumTask.Group.Title}\"";
                _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

                return true;
            }

            return false;
        }

        public bool OnAcceptDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterAcceptDirectumTaskEvent>(directumTask))
            {
                string title = "Принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
                _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

                return true;
            }

            return false;
        }

        public bool OnRejectDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<DirectumTask, AfterRejectDirectumTaskEvent>(directumTask))
            {
                string title = "Не принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";
                _popupNotificationsService.ShowPopupNotification(directumTask, message, title);

                return true;
            }

            return false;
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
            TechnicalRequrementsTask technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                return true;
            }

            return false;
        }

        public bool OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterStartTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                string message = null;

                //если текущий пользователь BackManagerBoss
                if (GlobalAppProperties.UserIsBackManagerBoss && technicalRequrementsTask.BackManager == null)
                {
                    if (technicalRequrementsTask.Start.HasValue)
                    {
                        message = $"Необходимо поручить задачу ТСЕ (инициатор: {technicalRequrementsTask.FrontManager})";
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
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterInstructTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
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
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }

            }

            return false;
        }

        public bool OnStopTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterStopTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                if (GlobalAppProperties.UserIsBackManager)
                {
                    string message = $"Задача ТСЕ остановлена (инициатор: {technicalRequrementsTask.FrontManager})";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterRejectTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            { 
                if (GlobalAppProperties.UserIsManager)
                {
                    string message = $"Задача ТСЕ отклонена (back-manager: {technicalRequrementsTask.BackManager})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterRejectByFrontManagerTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                if (GlobalAppProperties.UserIsBackManager)
                {
                    string message = $"Проработка задачи ТСЕ отклонена (front-manager: {technicalRequrementsTask.FrontManager})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnFinishTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                if (GlobalAppProperties.UserIsManager)
                {
                    string message = $"Завершена проработка задачи ТСЕ (back-manager: {technicalRequrementsTask.BackManager})";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        public bool OnAcceptTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<TechnicalRequrementsTask, AfterAcceptTechnicalRequrementsTaskEvent>(technicalRequrementsTask))
            {
                if (GlobalAppProperties.UserIsBackManager)
                {
                    string message = $"Задача ТСЕ принята (front-manager: {technicalRequrementsTask.FrontManager})";
                    _popupNotificationsService.ShowPopupNotification(technicalRequrementsTask, message, technicalRequrementsTask.ToString());

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region PriceCalculation

        public bool OnSavePriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (this.SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterSavePriceCalculationEvent>(calculation))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Реакция на старт расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public bool OnStartPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterStartPriceCalculationEvent>(calculation))
            {
                var message = $"Запущен: {calculation.Name}";
                var title = $"{calculation.Name} с Id {calculation.Id}";
                _popupNotificationsService.ShowPopupNotification(calculation, message, title);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Реакция на завершение расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public bool OnFinishPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterFinishPriceCalculationEvent>(calculation))
            {
                var message = $"Завершён: {calculation.Name}";
                var title = $"{calculation.Name} с Id {calculation.Id}";
                _popupNotificationsService.ShowPopupNotification(calculation, message, title);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Реакция на остановку расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public bool OnCancelPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterStopPriceCalculationEvent>(calculation))
            {
                var message = $"Остановлен: {calculation.Name}";
                var title = $"{calculation.Name} с Id {calculation.Id}";
                _popupNotificationsService.ShowPopupNotification(calculation, message, title);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Реакция на отклонение расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public bool OnRejectPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceCalculation, AfterRejectPriceCalculationEvent>(calculation))
            {
                var message = $"Отклонен: {calculation.Name}\nКомментарий: {calculation.LastHistoryItem.Comment}";
                var title = $"{calculation.Name} с Id {calculation.Id}";
                _popupNotificationsService.ShowPopupNotification(calculation, message, title);
                return true;
            }

            return false;
        }

        #endregion

        #region PriceEngineeringTasks

        public bool OnPriceEngineeringTasksStartServiceCallback(Guid priceEngineeringTasksId)
        {
            var priceEngineeringTasks = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasksId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTasks, PriceEngineeringTasksStartedEvent>(priceEngineeringTasks))
            {
                var message = $"{priceEngineeringTasks.UserManager} запустил: {priceEngineeringTasks}";
                var title = $"{priceEngineeringTasks} с Id {priceEngineeringTasks.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTasks, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskStartServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskStartedEvent>(priceEngineeringTask))
            {
                var message = $"Перезапущено: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskStopServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskStoppedEvent>(priceEngineeringTask))
            {
                var message = $"Остановлено: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskInstructServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskInstructedEvent>(priceEngineeringTask))
            {
                var message = $"Поручено: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskFinishServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskFinishedEvent>(priceEngineeringTask))
            {
                var message = $"Проработано: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskFinishGoToVerificationServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskFinishedGoToVerificationEvent>(priceEngineeringTask))
            {
                var message = $"Проработано: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskVerificationRejectedByHeadServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskVerificationRejectedByHeadEvent>(priceEngineeringTask))
            {
                var message = $"Возвращено на дороботку начальником отдела: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskVerificationAcceptedByHeadServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskVerificationAcceptedByHeadEvent>(priceEngineeringTask))
            {
                var message = $"Принято начальником отдела: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskRejectByManagerServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskRejectedByManagerEvent>(priceEngineeringTask))
            {
                var message = $"Отклонено: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskRejectByConstructorServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskRejectedByConstructorEvent>(priceEngineeringTask))
            {
                var message = $"Отклонено: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }

        public bool OnPriceEngineeringTaskAcceptServiceCallback(Guid priceEngineeringTaskId)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTask, PriceEngineeringTaskAcceptedEvent>(priceEngineeringTask))
            {
                var message = $"Принято: {priceEngineeringTask}";
                var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                return true;
            }

            return false;
        }


        public bool OnPriceEngineeringTaskSendMessageServiceCallback(Guid messageId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var taskMessage = unitOfWork.Repository<PriceEngineeringTaskMessage>().GetById(messageId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PriceEngineeringTaskMessage, PriceEngineeringTaskSendMessageEvent>(taskMessage))
            {
                var message = $"{taskMessage.Message}";
                var title = $"Сообщение от {taskMessage.Author}";
                var priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>().GetById(taskMessage.PriceEngineeringTaskId);
                _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
                //переводим в основной поток
                Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        _container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskReciveMessageEvent>().Publish(taskMessage);
                    });

                return true;
            }

            return false;
        }


        #endregion


        public bool OnSavePaymentDocumentServiceCallback(Guid paymentDocumentId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var paymentDocument = unitOfWork.Repository<PaymentDocument>().GetById(paymentDocumentId);

            if (SyncContainer.PublishWithinAppForCurrentUser<PaymentDocument, AfterSaveActualPaymentDocumentEvent>(paymentDocument))
            {
                var message = $"123";
                var title = $"Сохранено п/п №{paymentDocument.Number} от {paymentDocument.Date.ToShortDateString()} г.";
                _popupNotificationsService.ShowPopupNotification(paymentDocument, message, title);
                return true;
            }

            return false;
        }

    }
}