using System;
using System.Linq;
using EventServiceClient2.ServiceCallbackBase;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using Microsoft.Practices.Unity;
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

        public void OnSaveDirectumTaskServiceCallback(Guid taskId)
        {
        }

        public void OnStartDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this.SyncContainer.PublishWithinApp<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Вам поручена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnStopDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var isPerformer = directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (isPerformer)
            {
                this.SyncContainer.PublishWithinApp<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Остановлена задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }

        }

        public void OnPerformDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли принимать
            var isAuthor = directumTask.Group.Author.IsAppCurrentUser();

            if (isAuthor)
            {
                this.SyncContainer.PublishWithinApp<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Выполнена задача в DirectumLite";
                string message = $"Исполнитель: {directumTask.Performer}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnAcceptDirectumTaskServiceCallback(Guid taskId)
        {
            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this.SyncContainer.PublishWithinApp<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        public void OnRejectDirectumTaskServiceCallback(Guid taskId)
        {

            var directumTask = _container.Resolve<IUnitOfWork>().Repository<DirectumTask>().GetById(taskId);

            //можно ли исполнять
            var allowPerform = directumTask.StartResult.HasValue && directumTask.Performer != null && directumTask.Performer.IsAppCurrentUser();
            if (allowPerform)
            {
                this.SyncContainer.PublishWithinApp<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

                string title = "Не принята задача в DirectumLite";
                string message = $"Инициатор: {directumTask.Group.Author}\nТема: \"{directumTask.Group.Title}\"";

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { "task", directumTask } });
                });

                Popup.Popup.ShowPopup(message, title, action);
            }
        }

        #endregion

        #region IncomingRequest

        public void OnSaveIncomingRequestServiceCallback(Guid requestId)
        {
            var request = _container.Resolve<IUnitOfWork>().Repository<IncomingRequest>().GetById(requestId);

            var canInstruct = GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director;

            var canPerform = GlobalAppProperties.User.RoleCurrent == Role.SalesManager &&
                             request.Performers.Any(employee => employee.Id == GlobalAppProperties.User.Employee.Id);

            if (canInstruct || canPerform)
            {
                this.SyncContainer.PublishWithinApp<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

                string message = $"{request.Document.Comment}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestsView>(new NavigationParameters());
                });
                Popup.Popup.ShowPopup(message, "Запрос", action);
            }
        }

        public void OnSaveIncomingDocumentServiceCallback(Guid documentId)
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var document = unitOfWork.Repository<Document>().GetById(documentId);

            var canInstruct = (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                              GlobalAppProperties.User.RoleCurrent == Role.Director) &&
                              document.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany?.Id;

            if (canInstruct)
            {
                var request = new IncomingRequest { Document = document };
                this.SyncContainer.PublishWithinApp<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

                string message = $"{document.Comment}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestView>(
                        new NavigationParameters
                        {
                            {"Model", request},
                            {"UnitOfWork", unitOfWork}
                        });

                });
                Popup.Popup.ShowPopup(message, "Запрос", action);
            }
        }

        #endregion

        #region TechnicalRequarementsTask

        public void OnSaveTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            //зацикливается
            //TechnicalRequrementsTask technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
            //if (technicalRequrementsTask != null)
            //    _container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(technicalRequrementsTask);
        }

        public void OnStartTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss ||
                GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();
                if (frontManager == null) return;

                string message = string.Empty;

                //если текущий пользователь BackManagerBoss
                if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss)
                {
                    if (technicalRequrementsTask.Start.HasValue && technicalRequrementsTask.BackManager == null)
                    {
                        message = $"Поручите кому-нибудь новую задачу ТСЕ (инициатор: {frontManager})";
                    }
                }

                //если текущий пользователь Back-Менеджер
                else if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
                {
                    if (technicalRequrementsTask.BackManager == null) return;
                    if (technicalRequrementsTask.BackManager.IsAppCurrentUser())
                    {
                        message = $"Рестартована задача (инициатор: {frontManager})";
                    }
                }

                if (message != string.Empty)
                {
                    (new ServiceCallbackBaseTechnicalRequarementsTask<AfterSaveTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                        .Start(technicalRequrementsTask, message);
                }
            }
        }

        public void OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager || GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

                var frontManager = technicalRequrementsTask.GetFrontManager();
                if (frontManager == null) return;

                var backManager = technicalRequrementsTask.BackManager;
                if (backManager == null) return;

                string message = null;

                if (GlobalAppProperties.User.RoleCurrent == Role.BackManager && backManager.IsAppCurrentUser())
                {
                    message = $"Вам поручена задача ТСЕ (инициатор: {frontManager})";
                }

                else if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager && frontManager.IsAppCurrentUser())
                {
                    message = $"Ваша задача ТСЕ поручена (back-manager: {backManager})";
                }

                if (message != null)
                {
                    (new ServiceCallbackBaseTechnicalRequarementsTask<AfterSaveTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                        .Start(technicalRequrementsTask, message);
                }
            }
        }

        public void OnStopTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);

                var frontManager = technicalRequrementsTask.GetFrontManager();
                if (frontManager == null) return;

                var backManager = technicalRequrementsTask.BackManager;
                if (backManager == null) return;

                if (backManager.IsAppCurrentUser())
                {
                    (new ServiceCallbackBaseTechnicalRequarementsTask<AfterSaveTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                        .Start(technicalRequrementsTask, $"Задача ТСЕ остановлена (инициатор: {frontManager})");
                }
            }
        }

        public void OnRejectTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (!frontManager.IsAppCurrentUser()) return;
                if (technicalRequrementsTask.BackManager == null) return;

                (new ServiceCallbackBaseTechnicalRequarementsTask<AfterRejectTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                    .Start(technicalRequrementsTask, $"Ваша задача ТСЕ отклонена (back-manager: {technicalRequrementsTask.BackManager})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}");
            }
        }

        public void OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var backManager = technicalRequrementsTask.BackManager;

                if (backManager == null) return;
                if (!backManager.IsAppCurrentUser()) return;

                var frontManager = technicalRequrementsTask.GetFrontManager();
                if (frontManager == null) return;

                (new ServiceCallbackBaseTechnicalRequarementsTask<AfterRejectByFrontManagerTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                    .Start(technicalRequrementsTask, $"Проработка Вашей задача ТСЕ отклонена (front-manager: {frontManager})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}");
            }
        }

        public void OnFinishTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (frontManager.IsAppCurrentUser() == false) return;
                if (technicalRequrementsTask.BackManager == null) return;

                (new ServiceCallbackBaseTechnicalRequarementsTask<AfterFinishTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                    .Start(technicalRequrementsTask, $"Завершена проработка Вашей задачи ТСЕ (back-manager: {technicalRequrementsTask.BackManager})");
            }
        }

        public void OnAcceptTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (technicalRequrementsTask.BackManager == null) return;
                if (technicalRequrementsTask.BackManager.IsAppCurrentUser() == false) return;

                (new ServiceCallbackBaseTechnicalRequarementsTask<AfterAcceptTechnicalRequrementsTaskEvent>(_container, SyncContainer))
                    .Start(technicalRequrementsTask, $"Задача ТСЕ принята (front-manager: {frontManager})");
            }
        }

        #endregion

        #region PriceCalculation

        public void OnSavePriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator != null && calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator || GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this.SyncContainer.PublishWithinApp<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
            }
        }

        /// <summary>
        /// Реакция на старт расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public bool OnStartPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                (new ServiceCallbackBasePriceCalculation<AfterSavePriceCalculationEvent>(_container, _syncContainer)).Start(calculation, $"Запущен: {calculation.Name}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Реакция на завершение расчета ПЗ
        /// </summary>
        /// <param name="calculationId">Id калькуляции</param>
        public void OnFinishPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator)
            {
                this.SyncContainer.PublishWithinApp<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
                (new ServiceCallbackBasePriceCalculation<AfterFinishPriceCalculationEvent>(_container, _syncContainer)).Start(calculation, $"Завершен: {calculation.Name}");
            }
        }

        /// <summary>
        /// Реакция на остановку расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnCancelPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this.SyncContainer.PublishWithinApp<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
                (new ServiceCallbackBasePriceCalculation<AfterCancelPriceCalculationEvent>(_container, _syncContainer)).Start(calculation, $"Остановлен: {calculation.Name}");
            }
        }

        /// <summary>
        /// Реакция на отклонение расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnRejectPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (calculation.Initiator.IsAppCurrentUser())
            {
                this.SyncContainer.PublishWithinApp<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
                (new ServiceCallbackBasePriceCalculation<AfterRejectPriceCalculationEvent>(_container, _syncContainer)).Start(calculation, $"Отклонен: {calculation.Name}\nКомментарий: {calculation.LastHistoryItem.Comment}");
            }
        }

        #endregion
    }
}