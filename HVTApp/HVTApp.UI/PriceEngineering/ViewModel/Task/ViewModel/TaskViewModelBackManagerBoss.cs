using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManagerBoss : TaskViewModel
    {
        public override bool IsTarget => 
            this.Model.Status.Equals(ScriptStep.LoadToTceStart) ||
            this.Model.Status.Equals(ScriptStep.ProductionRequestStart);

        public override bool IsEditMode =>
            this.Model.Status.Equals(ScriptStep.LoadToTceStart) ||
            this.Model.Status.Equals(ScriptStep.ProductionRequestStart);


        public ICommandRaiseCanExecuteChanged InstructOpenOrderCommand { get; }

        public ICommandRaiseCanExecuteChanged StopProductionRequestConfirmCommand { get; }

        public ICommandRaiseCanExecuteChanged StopProductionRequestRejectCommand { get; }

        /// <summary>
        /// Плановик
        /// </summary>
        public UserEmptyWrapper UserPlanMaker
        {
            get => GetWrapper<UserEmptyWrapper>();
            set
            {
                SetComplexValue<User, UserEmptyWrapper>(UserPlanMaker, value);
                foreach (var priceEngineeringTask in this.Model.GetAllPriceEngineeringTasks())
                {
                    priceEngineeringTask.UserPlanMaker = value?.Model;
                }
            }
        }


        public TaskViewModelBackManagerBoss(IUnityContainer container, Guid priceEngineeringTaskId) : base(container,
            priceEngineeringTaskId)
        {
            InitializeComplexProperty(nameof(UserPlanMaker),
                Model.UserPlanMaker == null ? null : new UserEmptyWrapper(Model.UserPlanMaker));

            InstructOpenOrderCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>()
                        .Find(user => user.Roles.Select(role => role.Role).Contains(Role.PlanMaker))
                        .Where(user => user.IsActual);

                    var planMaker = container.Resolve<ISelectService>().SelectItem(users);

                    if (planMaker == null) return;

                    this.UserPlanMaker = new UserEmptyWrapper(planMaker);

                    this.AcceptChanges();

                    UnitOfWork.SaveChanges();

                    this.Messenger.SendMessage($"Назначен плановик: {planMaker.Employee.Person}", false);

                    var notificationUnit = new NotificationUnit
                        {
                            ActionType = NotificationActionType.PriceEngineeringTaskInstructToPlanMaker,
                            RecipientRole = Role.PlanMaker,
                            RecipientUser = planMaker,
                            TargetEntityId = Model.Id
                        };
                    container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
                },
                () => this.Model.Status.Equals(ScriptStep.ProductionRequestStart));
            
            StopProductionRequestConfirmCommand = new DoStepCommandStopProductionRequestConfirm(this, container);
            StopProductionRequestRejectCommand = new DoStepCommandStopProductionRequestReject(this, container);
        }
    }
}