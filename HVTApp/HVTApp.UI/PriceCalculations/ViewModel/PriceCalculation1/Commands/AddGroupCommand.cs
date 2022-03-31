using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class AddGroupCommand : BasePriceCalculationCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGroupCommand(PriceCalculationViewModel viewModel, IUnityContainer container, IUnitOfWork unitOfWork) 
            : base(viewModel, container)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void ExecuteMethod()
        {
            //������������� ������
            var items = _unitOfWork.Repository<SalesUnit>()
                .Find(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser())
                .Except(ViewModel.PriceCalculationWrapper.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Model))
                .Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit))
                .GroupBy(x => x, new SalesUnit2Comparer())
                .Select(ViewModel.GetPriceCalculationItem2Wrapper);

            //����� ������
            var viewModel = new PriceCalculationItemsViewModel(items);
            var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            //���������� ������
            if (dialogResult.HasValue && dialogResult.Value)
            {
                viewModel.SelectedItemWrappers.ForEach(x => ViewModel.PriceCalculationWrapper.PriceCalculationItems.Add(x));
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted;
        }
    }
}