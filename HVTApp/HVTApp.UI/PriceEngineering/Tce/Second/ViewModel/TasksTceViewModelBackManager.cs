using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceViewModelBackManager : TasksTceViewModel
    {
        public DelegateLogConfirmationCommand FinishCommand { get; }

        public TasksTceViewModelBackManager(IUnityContainer container) : base(container)
        {
            FinishCommand = new DelegateLogConfirmationCommand(container.Resolve<IMessageService>(),
                "Вы уверены, что хотите завершить синхронизацию задач с ТСЕ?",
                () => throw new NotImplementedException(), 
                () => throw new NotImplementedException());
        }

        protected override void SaveItem()
        {
            var priceCalculations = this.Item.PriceCalculations
                .Where(x => x.Model.LastHistoryItem?.Type == PriceCalculationHistoryItemType.Create)
                .ToList();

            if (priceCalculations.Any())
            {
                var messageService = this.Container.Resolve<IMessageService>();
                var dr = messageService.ConfirmationDialog("Сохраненные сейчас данные будут использованы для формирования расчёта ПЗ.\nВы хотите продолжить сохранение?");
                if (dr == false)
                    return;

                base.SaveItem();

                foreach (var priceCalculation in priceCalculations)
                {
                    var priceCalculationViewModel = this.Container.Resolve<PriceCalculationViewModel>();
                    priceCalculationViewModel.RegenerateScc(priceCalculation.Model);
                    priceCalculationViewModel.StartCommand.Execute(false);

                    dr = messageService.ConfirmationDialog("Расчёт ПЗ сформирован и запущен на проработку.\nХотите его открыть?");
                    if (dr)
                    {
                        var regionManager = this.Container.Resolve<IRegionManager>();
                        regionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), priceCalculation.Model } });
                    }
                }
            }
            else
            {
                base.SaveItem();
            }
        }
    }
}