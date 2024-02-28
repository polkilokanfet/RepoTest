using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class FinishCommand : BaseBasePriceCalculationCommandNotifyCommand
    {
        protected override string ConfirmationMessage => "Вы уверены, что хотите завершить расчёт ПЗ?";
        protected override PriceCalculationHistoryItemType HistoryItemType => PriceCalculationHistoryItemType.Finish;

        public FinishCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool CanExecuteMethod()
        {
            if (ViewModel.PriceCalculationWrapper == null)
            {
                return false;
            }

            if (ViewModel.PriceCalculationWrapper.IsNeedExcelFile && !ViewModel.CalculationHasFile)
            {
                return false;
            }

            return ViewModel.IsStarted &&
                   !ViewModel.IsFinished &&
                   ViewModel.PriceCalculationWrapper.IsValid &&
                   ViewModel.PriceCalculationWrapper.PriceCalculationItems.SelectMany(item => item.StructureCosts).All(structureCost => structureCost.UnitPrice.HasValue);

        }

        protected override void ExecuteMethod()
        {
            base.ExecuteMethod();

            //добавление новых ПЗ в блоки
            if (this.DialogResult)
                AddPricesInProductBlocks();
        }

        /// <summary>
        /// Добавление новых ПЗ в блоки
        /// </summary>
        private void AddPricesInProductBlocks()
        {
            var structureCosts = ViewModel.PriceCalculationWrapper.Model.PriceCalculationItems
                .SelectMany(calculationItem => calculationItem.StructureCosts)
                .Where(structureCost => structureCost.OriginalStructureCostNumber != null &&
                                        structureCost.OriginalStructureCostProductBlock != null)
                .ToList();

            if (structureCosts.Any())
            {
                var unitOfWork = Container.Resolve<IUnitOfWork>();
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

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            var users = ViewModel.PriceCalculationWrapper.Model.PriceCalculationItems
                .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                .Select(salesUnit => salesUnit.Project.Manager)
                .Distinct();

            foreach (var user in users)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.FinishPriceCalculation,
                    RecipientRole = Role.SalesManager,
                    RecipientUser = user,
                    TargetEntityId = ViewModel.PriceCalculationWrapper.Model.Id
                };
            }
        }
    }
}