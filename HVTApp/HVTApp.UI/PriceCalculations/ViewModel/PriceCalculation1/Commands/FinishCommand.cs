using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class FinishCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;

        public FinishCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите завершить задачу?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            var historyItemWrapper = _viewModel.HistoryItem;
            historyItemWrapper.Moment = DateTime.Now;
            historyItemWrapper.Type = PriceCalculationHistoryItemType.Finish;
            _viewModel.PriceCalculationWrapper.History.Add(historyItemWrapper);

            _viewModel.SaveCommand.Execute();

            _viewModel.CanChangePriceOnPropertyChanged();

            _viewModel.SaveCommand.RaiseCanExecuteChanged();
            _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            _container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            _viewModel.RefreshCommands();

            _viewModel.GenerateNewHistoryItem();

            //добавление новых ПЗ в блоки
            AddPricesInProductBlocks();
        }

        /// <summary>
        /// Добавление новых ПЗ в блоки
        /// </summary>
        private void AddPricesInProductBlocks()
        {
            var structureCosts = _viewModel.PriceCalculationWrapper.Model.PriceCalculationItems
                .SelectMany(calculationItem => calculationItem.StructureCosts)
                .Where(structureCost => structureCost.OriginalStructureCostNumber != null &&
                                        structureCost.OriginalStructureCostProductBlock != null)
                .ToList();

            if (structureCosts.Any())
            {
                var unitOfWork = _container.Resolve<IUnitOfWork>();
                foreach (var structureCost in structureCosts)
                {
                    if (structureCost.OriginalStructureCostProductBlock.StructureCostNumber ==
                        structureCost.OriginalStructureCostNumber)
                    {
                        var productBlock = unitOfWork.Repository<ProductBlock>()
                            .GetById(structureCost.OriginalStructureCostProductBlock.Id);
                        var calculationItem = unitOfWork.Repository<PriceCalculationItem>()
                            .GetById(structureCost.PriceCalculationItemId);
                        if (calculationItem.RealizationDate.Value > DateTime.Today.AddDays(180) &&
                            productBlock.Prices.Any())
                            continue;

                        var sumOnDate = new SumOnDate
                        {
                            Sum = structureCost.UnitPrice.Value,
                            Date = calculationItem.RealizationDate.Value
                        };

                        if (productBlock.Prices.Any(x =>
                            Math.Abs(x.Sum - sumOnDate.Sum) < 0.0001 && x.Date == sumOnDate.Date) == false)
                        {
                            productBlock.Prices.Add(sumOnDate);
                            unitOfWork.SaveChanges();
                        }
                    }
                }

                unitOfWork.Dispose();
            }
        }

        protected override bool CanExecuteMethod()
        {
            if (_viewModel.PriceCalculationWrapper == null)
            {
                return false;
            }

            if (_viewModel.PriceCalculationWrapper.IsNeedExcelFile && !_viewModel.CalculationHasFile)
            {
                return false;
            }

            return _viewModel.IsStarted &&
                   !_viewModel.IsFinished &&
                   _viewModel.PriceCalculationWrapper.IsValid &&
                   _viewModel.PriceCalculationWrapper.PriceCalculationItems.SelectMany(item => item.StructureCosts).All(structureCost => structureCost.UnitPrice.HasValue);

        }
    }
}