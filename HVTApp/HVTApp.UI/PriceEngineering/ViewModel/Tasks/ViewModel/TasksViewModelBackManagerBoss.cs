using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
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
                    var users = UnitOfWork.Repository<User>().Find(user => user.Roles.Select(role => role.Role).Contains(Role.BackManager));
                    var backManager = container.Resolve<ISelectService>().SelectItem(users);
                    if (backManager != null)
                    {
                        this.TasksWrapper.BackManager = new UserEmptyWrapper(backManager);
                        this.TasksWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                        InstructCommand.RaiseCanExecuteChanged();
                    }
                },
                () =>
                    TasksWrapper != null &&
                    TasksWrapper.IsValid);
        }

        protected override TasksWrapperBackManagerBoss GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperBackManagerBoss(priceEngineeringTasks, container);
        }
    }
}