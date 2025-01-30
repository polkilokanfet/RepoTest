using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductBlockDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            RemoveFromPricesCommand = new DelegateLogConfirmationCommand(
                this.Container.Resolve<IMessageService>(),
                () =>
                {
                    using (var unitOfWork = Container.Resolve<IUnitOfWork>())
                    {
                        var block = unitOfWork.Repository<ProductBlock>().GetById(this.Entity.Id);
                        var sumOnDate = unitOfWork.Repository<SumOnDate>().GetById(SelectedPricesItem.Id);

                        block.Prices.Remove(sumOnDate);
                        unitOfWork.Repository<SumOnDate>().Delete(sumOnDate);

                        unitOfWork.SaveChanges();
                    }
                }, 
                () => this.SelectedPricesItem != null);
        }
    }
}