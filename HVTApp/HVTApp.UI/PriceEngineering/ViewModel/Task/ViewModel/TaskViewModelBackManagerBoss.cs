using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

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

        public TaskViewModelBackManagerBoss(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
            InstructOpenOrderCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>()
                        .Find(user1 => user1.Roles.Select(role => role.Role).Contains(Role.PlanMaker))
                        .Where(user1 => user1.IsActual);
                    
                    var user = container.Resolve<ISelectService>().SelectItem(users);

                    if (user == null) return;

                    foreach (var priceEngineeringTask in this.Model.GetAllPriceEngineeringTasks())
                    {
                        priceEngineeringTask.UserPlanMaker = user;
                    }

                    this.AcceptChanges();

                    UnitOfWork.SaveChanges();

                    this.Messenger.SendMessage($"Назначен плановик: {user.Employee.Person}");

                    RaisePropertyChanged(nameof(this.Model.UserPlanMaker));
                },
                () => this.Model.Status.Equals(ScriptStep.ProductionRequestStart));
        }
    }
}