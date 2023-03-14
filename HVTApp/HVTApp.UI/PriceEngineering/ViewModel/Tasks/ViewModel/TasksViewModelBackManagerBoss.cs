using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelBackManagerBoss : TasksViewModelVisible<TasksWrapperBackManagerBoss, TaskViewModelBackManagerBoss>
    {
        public ICommandRaiseCanExecuteChanged InstructCommand { get; }

        public TasksViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>()
                        .Find(user => user.Roles.Select(role => role.Role).Contains(Role.BackManager))
                        .Where(user => user.IsActual);
                    var backManager = container.Resolve<ISelectService>().SelectItem(users);
                    if (backManager == null) return;

                    var author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                    foreach (var task in this.TasksWrapper.Model.ChildPriceEngineeringTasks.Where(x => x.Status.Equals(ScriptStep.LoadToTceStart)))
                    {
                        foreach (var priceEngineeringTask in task.GetAllPriceEngineeringTasks().Where(x => x.Status.Equals(ScriptStep.LoadToTceStart)))
                        {
                            priceEngineeringTask.Messages.Add(new PriceEngineeringTaskMessage
                            {
                                Moment = DateTime.Now,
                                Author = author,
                                Message = $"Назначен бэк-менеджер: {backManager.Employee.Person}"
                            });
                        }
                    }
                        
                    this.TasksWrapper.BackManager = new UserEmptyWrapper(backManager);
                    this.TasksWrapper.AcceptChanges();
                    UnitOfWork.SaveChanges();
                },
                () =>
                    TasksWrapper != null &&
                    TasksWrapper.ChildTasks.Any(x => x.Model.Status.Equals(ScriptStep.LoadToTceStart)) &&
                    TasksWrapper.IsValid);
        }
        
        protected override TasksWrapperBackManagerBoss GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperBackManagerBoss(priceEngineeringTasks, container);
        }
    }
}