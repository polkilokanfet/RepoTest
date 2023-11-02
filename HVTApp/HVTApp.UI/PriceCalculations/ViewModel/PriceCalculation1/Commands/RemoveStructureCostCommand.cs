using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RemoveStructureCostCommand : BasePriceCalculationCommand
    {
        public RemoveStructureCostCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var dr = MessageService.ConfirmationDialog("Удаление", "Действительно хотите удалить StructureCost?", defaultNo: true);
            if (dr == false) return;

            var structureCost = (StructureCost2Wrapper)ViewModel.SelectedItem;
            var calculationItem2Wrapper = ViewModel.PriceCalculationWrapper.PriceCalculationItems.Single(x => x.StructureCosts.Contains(structureCost));
            calculationItem2Wrapper.StructureCosts.Remove(structureCost);
            if (ViewModel.UnitOfWork1.Repository<StructureCost>().GetById(structureCost.Id) != null)
            {
                ViewModel.UnitOfWork1.Repository<StructureCost>().Delete(structureCost.Model);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is WrapperBase<StructureCost> && !ViewModel.IsStarted;
        }
    }
}