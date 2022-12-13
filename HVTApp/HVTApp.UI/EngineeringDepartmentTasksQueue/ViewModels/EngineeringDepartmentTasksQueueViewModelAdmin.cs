using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public class EngineeringDepartmentTasksQueueViewModelAdmin : EngineeringDepartmentTasksQueueViewModel
    {
        public ICommand PriorityUpCommand { get; }
        public ICommand PriorityDownCommand { get; }

        public EngineeringDepartmentTasksQueueViewModelAdmin(IUnityContainer container) : base(container)
        {
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
            return UnitOfWork.Repository<PriceEngineeringTask>()
                .Find(x => x.IsStarted && x.IsAccepted == false)
                .Select(GetTopTask)
                .Distinct()
                .Where(x => x.SalesUnits.Any())
                .Select(x => new EngineeringDepartmentTaskPrice(x, UnitOfWork.Repository<PriceEngineeringTasks>().GetById(x.ParentPriceEngineeringTasksId.Value).WorkUpTo))
                .OrderBy(x => x);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            ((DelegateLogCommand)PriorityUpCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)PriorityDownCommand).RaiseCanExecuteChanged();
        }
    }
}