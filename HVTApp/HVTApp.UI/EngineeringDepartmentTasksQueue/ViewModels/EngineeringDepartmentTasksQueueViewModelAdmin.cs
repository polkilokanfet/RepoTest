using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public class EngineeringDepartmentTasksQueueViewModelAdmin : EngineeringDepartmentTasksQueueViewModel
    {
        public ICommand PriotityUpCommand { get; }

        public EngineeringDepartmentTasksQueueViewModelAdmin(IUnityContainer container) : base(container)
        {
            PriotityUpCommand = new DelegateLogCommand(
                () =>
                {
                    if (Equals(SelectedItem, Items.First()))
                        return;

                    //назначаем срок проработки (на 1 тик меньший, чем у задачи впереди)
                    var itemIndex = Items.IndexOf(SelectedItem);
                    var previousItemIndex = itemIndex - 1;
                    var previousItem = Items[previousItemIndex];
                    SelectedItem.BaseTask.TermPriority = previousItem.Term.AddTicks(-1);

                    UnitOfWork.SaveChanges();

                    Items.Move(itemIndex, previousItemIndex);
                },
                () => this.SelectedItem != null);
        }

        protected override IEnumerable<EngineeringDepartmentTask> GetAllItems()
        {
            return UnitOfWork.Repository<PriceEngineeringTask>().Find(x =>
                    x.ParentPriceEngineeringTaskId == null &&
                    x.ParentPriceEngineeringTasksId != null &&
                    x.SalesUnits.Any() &&
                    x.IsTotalAccepted == false &&
                    x.IsTotalStopped == false)
                .Select(x => new EngineeringDepartmentTaskPrice(x, UnitOfWork.Repository<PriceEngineeringTasks>().GetById(x.ParentPriceEngineeringTasksId.Value).WorkUpTo))
                .OrderBy(x => x);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            ((DelegateLogCommand)PriotityUpCommand).RaiseCanExecuteChanged();
        }
    }
}