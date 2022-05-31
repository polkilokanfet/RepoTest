using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceViewModelBackManager : TasksTceViewModel
    {
        public TasksTceViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override void SaveItem()
        {
            var priceCalculations = this.Item.PriceCalculations
                .Where(x => x.Model.LastHistoryItem?.Type == PriceCalculationHistoryItemType.Create)
                .ToList();

            if (priceCalculations.Any())
            {
                var dr = this.Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Уведомление", "Сохраненные сейчас данные будут использованы для формирования расчёта ПЗ. Вы хотите продолжить сохранение?");
                if (dr != MessageDialogResult.Yes)
                    return;

                base.SaveItem();

                foreach (var priceCalculation in priceCalculations)
                {
                    var priceCalculationViewModel = this.Container.Resolve<PriceCalculationViewModel>();
                    priceCalculationViewModel.RegenerateScc(priceCalculation.Model);
                    priceCalculationViewModel.StartCommand.Execute();
                }
            }
            else
            {
                base.SaveItem();
            }

        }
    }
}