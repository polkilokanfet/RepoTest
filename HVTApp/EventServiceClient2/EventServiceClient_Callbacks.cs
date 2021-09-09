using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using EventServiceClient2.SyncEntities;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace EventServiceClient2
{
    public partial class EventServiceClient : IEventServiceClient, EventServiceClient2.ServiceReference1.IEventServiceCallback
    {
        //действия, когда прилетают события из сервера синхронизации

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
            FilesStorage.CopyDirectory(PathGetter.GetPath(project), targetDirectory);
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
                this.SyncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

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
                this.SyncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

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
                this.SyncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

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
                this.SyncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

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
                this.SyncContainer.Publish<DirectumTask, AfterSaveDirectumTaskEvent>(directumTask);

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

        #region PriceCalculation

        public void OnSavePriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator != null && calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator || GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this.SyncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
            }
        }

        /// <summary>
        /// Реакция на старт расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnStartPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer)
            {
                this.SyncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Запущен расчет переменных затрат", action);
            }
        }

        /// <summary>
        /// Реакция на завершение расчета ПЗ
        /// </summary>
        /// <param name="calculationId"></param>
        public void OnFinishPriceCalculationServiceCallback(Guid calculationId)
        {
            var calculation = _container.Resolve<IUnitOfWork>().Repository<PriceCalculation>().GetById(calculationId);

            var frontManager = calculation.GetFrontManager();
            var isProjectManager = frontManager != null && frontManager.IsAppCurrentUser();
            var isInitiator = calculation.Initiator.IsAppCurrentUser();

            if (isProjectManager || isInitiator)
            {
                this.SyncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
                this.SyncContainer.Publish<PriceCalculation, AfterFinishPriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Завершен расчет переменных затрат", action);
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
                this.SyncContainer.Publish<PriceCalculation, AfterSavePriceCalculationEvent>(calculation);
                this.SyncContainer.Publish<PriceCalculation, AfterCancelPriceCalculationEvent>(calculation);

                string message = $"{calculation.Name}";
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), calculation } });
                });
                Popup.Popup.ShowPopup(message, "Остановлен расчет переменных затрат", action);
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
                this.SyncContainer.Publish<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

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
                this.SyncContainer.Publish<IncomingRequest, AfterSaveIncomingRequestEvent>(request);

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
                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

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
                    this.SyncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                    Popup.Popup.ShowPopup(message, $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
                }
            }
        }

        public void OnInstructTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (technicalRequrementsTask.BackManager == null) return;
                if (!technicalRequrementsTask.BackManager.IsAppCurrentUser()) return;

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this.SyncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Вам поручена задача ТСЕ (инициатор: {frontManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }

            else if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var technicalRequrementsTask = _container.Resolve<IUnitOfWork>().Repository<TechnicalRequrementsTask>().GetById(technicalRequarementsTaskId);
                var frontManager = technicalRequrementsTask.GetFrontManager();

                if (frontManager == null) return;
                if (!frontManager.IsAppCurrentUser()) return;
                if (technicalRequrementsTask.BackManager == null) return;

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this.SyncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ поручена (back-manager: {technicalRequrementsTask.BackManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }
        }

        public void OnCancelTechnicalRequarementsTaskServiceCallback(Guid technicalRequarementsTaskId)
        {
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

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this.SyncContainer.Publish<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ отклонена (back-manager: {technicalRequrementsTask.BackManager})\nПричина отклонения: {technicalRequrementsTask.LastHistoryElement?.Comment}", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
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

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this.SyncContainer.Publish<TechnicalRequrementsTask, AfterFinishTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ завершена (back-manager: {technicalRequrementsTask.BackManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
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

                var action = new Action(() =>
                {
                    _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { "technicalRequrementsTask", technicalRequrementsTask } });
                });

                this.SyncContainer.Publish<TechnicalRequrementsTask, AfterAcceptTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
                Popup.Popup.ShowPopup($"Ваша задача ТСЕ принята (front-manager: {frontManager})", $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
            }
        }

        #endregion

    }
}
