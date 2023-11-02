using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations.Commands
{
    public class RemoveCalculationCommand : DelegateLogCommand
    {
        private readonly PriceCalculationsViewModel _viewModel;
        private readonly IUnityContainer _container;

        public RemoveCalculationCommand(PriceCalculationsViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var messageService = _container.Resolve<IMessageService>();
            var result = messageService.ConfirmationDialog("Удаление", "Действительно хотите удалить из расчет ПЗ?", defaultNo: true);
            if (result == false) return;

            var unitOfWork = _container.Resolve<IUnitOfWork>();

            var calculation = unitOfWork.Repository<PriceCalculation>().GetById(_viewModel.SelectedItem.Id);
            foreach (var item in calculation.PriceCalculationItems.ToList())
            {
                var salesUnits = item.SalesUnits.ToList();

                //единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
                var salesUnitsNotForRemove = salesUnits
                    .Where(salesUnit => salesUnit.SignalToStartProduction.HasValue)
                    .Where(salesUnit => salesUnit.ActualPriceCalculationItem(unitOfWork).Id == item.Id)
                    .ToList();

                if (salesUnitsNotForRemove.Any())
                {
                    var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                    salesUnitsToRemove.ForEach(x => item.SalesUnits.Remove(x));
                    if (!item.SalesUnits.Any())
                    {
                        calculation.PriceCalculationItems.Remove(item);
                        unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                    }
                }
                else
                {
                    calculation.PriceCalculationItems.Remove(item);
                    unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                }
            }

            if (calculation.PriceCalculationItems.Any())
            {
                messageService.Message("Удаление", "Вы не можете удалить некоторые строки в расчете, т.к. они размещены в производстве.");
            }
            else
            {
                unitOfWork.Repository<PriceCalculation>().Delete(calculation);
                ((ICollection<PriceCalculationLookup>)_viewModel.Lookups).Remove(_viewModel.SelectedLookup);
            }

            unitOfWork.SaveChanges();
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem != null;
        }
    }
}