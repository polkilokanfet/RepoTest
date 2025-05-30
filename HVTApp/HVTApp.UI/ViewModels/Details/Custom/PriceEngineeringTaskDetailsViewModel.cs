﻿using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class PriceEngineeringTaskDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            RemoveFromStatusesCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Вы уверены? Осторожно! Удаление потом не вернуть!");
                    if (dr == false) return;

                    var status = this.SelectedStatusesItem;
                    this.Item.Statuses.Remove(status);

                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    if (status.Model.StatusEnum == ScriptStep.ProductionRequestFinish.Value)
                    {
                        this.Item.Model.SalesUnits.ForEach(salesUnit => salesUnit.SignalToStartProductionDone = null);
                        var orders = this.Item.Model.SalesUnits
                            .Select(salesUnit => unitOfWork.Repository<Order>().GetById(salesUnit.Order.Id))
                            .Distinct();
                        foreach (var order in orders)
                        {
                            unitOfWork.Repository<Order>().Delete(order);
                        }
                    }

                    unitOfWork.Repository<PriceEngineeringTaskStatus>().Delete(status.Model);
                    unitOfWork.SaveChanges();

                }, () => this.SelectedStatusesItem != null);
        }
    }
}