using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    /// <summary>
    /// PriceEngineeringTasksViewModel для руководителя КБ
    /// </summary>
    public class PriceEngineeringTasksViewModelDesignDepartmentHead : PriceEngineeringTasksViewModelVisible<PriceEngineeringTasksContainerWrapperDesignDepartmentHead, TaskViewModelDesignDepartmentHead>
    {
        private List<TaskViewModelDesignDepartmentHead> AllTasksForInstruct
        {
            get
            {
                var result = new List<TaskViewModelDesignDepartmentHead>();
                if (this.PriceEngineeringTasksWrapper != null)
                {
                    foreach (var childTask in this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                    {
                        result.AddRange(childTask.GetSuitableTasksForInstruct());
                    }
                }

                return result;
            }
        }
        public ICommand InstructAllTasksCommand { get; }

        public PriceEngineeringTasksViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
            InstructAllTasksCommand = new DelegateLogCommand(
                () =>
                {
                    var tasksToInstruct = AllTasksForInstruct;

                    if (tasksToInstruct.Any() == false)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Тут нет задач, которые можно поручить.");
                        return;
                    }

                    var staff = tasksToInstruct.First().Model.DesignDepartment.Staff;
                    foreach (var taskToInstruct in tasksToInstruct)
                    {
                        foreach (var user1 in staff.ToList())
                        {
                            if (taskToInstruct.Model.DesignDepartment.Staff.ContainsById(user1) == false)
                                staff.Remove(user1);
                        }
                    }

                    var user = Container.Resolve<ISelectService>().SelectItem(staff);
                    if (user == null) return;

                    var needVerification = MessageDialogResult.Yes == Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo: true);

                    foreach (var taskForInstruct in tasksToInstruct)
                    {
                        taskForInstruct.Instruct(user, needVerification);
                    }
                });
        }

        protected override PriceEngineeringTasksContainerWrapperDesignDepartmentHead GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new PriceEngineeringTasksContainerWrapperDesignDepartmentHead(priceEngineeringTasks, container);
        }
    }
}