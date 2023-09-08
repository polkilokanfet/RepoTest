using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class PriceEngineeringTaskDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            RemoveFromStatusesCommand = new DelegateLogCommand(
                () =>
                {
                    var status = this.SelectedStatusesItem;
                    this.Item.Statuses.Remove(status);

                    if (status.Model.StatusEnum == 14)
                    {
                        this.Item.Model.SalesUnits.ForEach(x => x.SignalToStartProductionDone = null);
                        var orders = this.Item.Model.SalesUnits.Select(x => x.Order).Distinct();
                        foreach (var order in orders)
                        {
                            this.UnitOfWork.Repository<Order>().Delete(order);
                        }
                    }

                    this.UnitOfWork.Repository<PriceEngineeringTaskStatus>().Delete(status.Model);

                }, () => this.SelectedStatusesItem != null);
        }
    }
}