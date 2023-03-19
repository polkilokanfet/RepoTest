using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public class EngineeringDepartmentTasksQueueViewModelAdmin : EngineeringDepartmentTasksQueueViewModel
    {
        public ICommandRaiseCanExecuteChanged PriorityUpToTopCommand { get; }
        public ICommandRaiseCanExecuteChanged PriorityUpCommand { get; }
        public ICommandRaiseCanExecuteChanged PriorityDownCommand { get; }

        public EngineeringDepartmentTasksQueueViewModelAdmin(IUnityContainer container) : base(container)
        {
            PriorityUpToTopCommand = new DelegateLogCommand(
                () =>
                {
                    if (Equals(SelectedItem, Items.First())) return;

                    //назначаем срок проработки (на 1 тик меньший, чем у первой задачи)
                    SelectedItem.BaseTask.TermPriority = Items.First().Term.AddTicks(-1);

                    UnitOfWork.SaveChanges();

                    Items.Move(Items.IndexOf(SelectedItem), 0);
                },
                () => this.SelectedItem != null);

            PriorityUpCommand = new DelegateLogCommand(
                () =>
                {
                    if (Equals(SelectedItem, Items.First()))
                        return;

                    //назначаем срок проработки (на 1 тик меньший, чем у задачи впереди)
                    var itemIndex = Items.IndexOf(SelectedItem);
                    var targetItemIndex = GetTargetItemIndexUp(itemIndex);
                    var targetItem = Items[targetItemIndex];
                    SelectedItem.BaseTask.TermPriority = targetItem.Term.AddTicks(-1);

                    UnitOfWork.SaveChanges();

                    Items.Move(itemIndex, targetItemIndex);
                },
                () => this.SelectedItem != null);

            PriorityDownCommand = new DelegateLogCommand(
                () =>
                {
                    if (Equals(SelectedItem, Items.Last()))
                        return;

                    //назначаем срок проработки (на 1 тик больший, чем у задачи впереди)
                    var itemIndex = Items.IndexOf(SelectedItem);
                    var targetItemIndex = GetTargetItemIndexDown(itemIndex);
                    var targetItem = Items[targetItemIndex];
                    SelectedItem.BaseTask.TermPriority = targetItem.Term.AddTicks(1);

                    UnitOfWork.SaveChanges();

                    Items.Move(itemIndex, targetItemIndex);
                },
                () => this.SelectedItem != null);
        }

        private int GetTargetItemIndexUp(int currentIndex)
        {
            int resultIndex = currentIndex - 1;
            var targetItemTerm = Items[resultIndex].Term;
            while (resultIndex - 1 > 0 && targetItemTerm == Items[resultIndex - 1].Term)
            {
                resultIndex -= 1;
            }

            return resultIndex;
        }

        private int GetTargetItemIndexDown(int currentIndex)
        {
            int resultIndex = currentIndex + 1;
            var targetItemTerm = Items[resultIndex].Term;
            while (resultIndex + 1 < Items.Count && targetItemTerm == Items[resultIndex + 1].Term)
            {
                resultIndex += 1;
            }

            return resultIndex;
        }

        protected override IEnumerable<EngineeringDepartmentTask> GetAllItems()
        {
            var tasks = UnitOfWork.Repository<PriceEngineeringTask>()
                .Find(task => task.IsStarted && task.IsAccepted == false)
                .Select(task => task.GetPriceEngineeringTasks(UnitOfWork))
                .Distinct();

            return tasks
                .SelectMany(priceEngineeringTasks => priceEngineeringTasks.ChildPriceEngineeringTasks)
                .Distinct()
                .Where(task => task.SalesUnits.Any())
                .Where(task => task.ParentPriceEngineeringTasksId.HasValue)
                .Select(task => new EngineeringDepartmentTaskPrice(task, UnitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value).WorkUpTo))
                .OrderBy(taskPrice => taskPrice);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            PriorityUpToTopCommand.RaiseCanExecuteChanged();
            PriorityUpCommand.RaiseCanExecuteChanged();
            PriorityDownCommand.RaiseCanExecuteChanged();
        }
    }
}