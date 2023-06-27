using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelAdmin : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemSalesManager, PriceEngineeringTaskListItemSalesManager>, IDisposable
    {
        public ICommand RemoveTaskCommand { get; }

        public PriceEngineeringTasksListViewModelAdmin(IUnityContainer container) : base(container)
        {
            RemoveTaskCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить выделенную задачу/подзадачу?",
                () =>
                {
                    if (SelectedItem is null) return;

                    if (SelectedItem is PriceEngineeringTasksListItemSalesManager tasks)
                    {
                        var unitOfWork = container.Resolve<IUnitOfWork>();
                        var priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTasks>().GetById(tasks.Id);
                        unitOfWork.RemoveEntity(priceEngineeringTasks);
                        unitOfWork.SaveChanges();
                        return;
                    }

                    if (SelectedItem is PriceEngineeringTaskListItemSalesManager task)
                    {
                        var unitOfWork = container.Resolve<IUnitOfWork>();
                        var priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTask>().GetById(task.Id);
                        unitOfWork.RemoveEntity(priceEngineeringTasks);
                        unitOfWork.SaveChanges();
                    }
                });
        }

        protected override PriceEngineeringTasksListItemSalesManager GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemSalesManager(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return true;
        }

        public void Dispose()
        {
        }
    }
}