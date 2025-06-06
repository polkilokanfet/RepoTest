using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelAdmin : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemSalesManager, PriceEngineeringTaskListItemSalesManager>, IDisposable
    {
        public ICommand RemoveTaskCommand { get; }

        public PriceEngineeringTasksListViewModelAdmin(IUnityContainer container) : base(container)
        {
            RemoveTaskCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "�� �������, ��� ������ ������� ���������� ������/���������?",
                () =>
                {
                    if (SelectedItem is null) return;

                    if (SelectedItem is PriceEngineeringTasksListItemSalesManager tasks1)
                    {
                        var unitOfWork = container.Resolve<IUnitOfWork>();
                        var tasks = unitOfWork.Repository<PriceEngineeringTasks>().GetById(tasks1.Id);
                        unitOfWork.Repository<PriceEngineeringTask>().DeleteRange(tasks.ChildPriceEngineeringTasks.SelectMany(x => x.GetAllPriceEngineeringTasks()));
                        unitOfWork.Repository<PriceEngineeringTasks>().Delete(tasks);
                        unitOfWork.SaveChanges();
                        return;
                    }

                    if (SelectedItem is PriceEngineeringTaskListItemSalesManager task1)
                    {
                        var unitOfWork = container.Resolve<IUnitOfWork>();
                        var task = unitOfWork.Repository<PriceEngineeringTask>().GetById(task1.Id);
                        unitOfWork.Repository<PriceEngineeringTask>().DeleteRange(task.GetAllPriceEngineeringTasks());
                        unitOfWork.SaveChanges();
                    }
                });
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        protected override PriceEngineeringTasksListItemSalesManager GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemSalesManager(model);
        }

        protected override IEnumerable<PriceEngineeringTasks> GetSuitableTasks()
        {
            return UnitOfWork.Repository<PriceEngineeringTasks>().GetAll();
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