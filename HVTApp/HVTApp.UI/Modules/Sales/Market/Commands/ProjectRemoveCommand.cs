using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class ProjectRemoveCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;

        public ProjectRemoveCommand(Market2ViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
            _messageService = container.Resolve<IMessageService>();
        }

        protected override void ExecuteMethod()
        {
            if (_messageService.ConfirmationDialog("Удалить проект.", "Вы уверены, что хотите удалить проект?", defaultNo: true) == false)
                return;

            var unitOfWork = _container.Resolve<IUnitOfWork>();
            var salesUnits = unitOfWork.Repository<SalesUnit>()
                .Find(salesUnit => salesUnit.Project.Id == _viewModel.SelectedProjectItem.Project.Id)
                .Where(salesUnit => !salesUnit.IsRemoved)
                .ToList();

            if (salesUnits.Any(salesUnit => salesUnit.Order != null))
            {
                _messageService.Message("Информация", "Нельзя удалить проект целиком, т.к. в нем есть оборудование, размещенное в производстве.");
                return;
            }

            //проверяем не включено ли оборудование в какой-либо бюджет
            var budgetUnits = unitOfWork.Repository<BudgetUnit>().Find(x => !x.IsRemoved);
            var idIntersection = salesUnits.Select(salesUnit => salesUnit.Id).Intersect(budgetUnits.Select(budgetUnit => budgetUnit.SalesUnit.Id)).ToList();
            if (idIntersection.Any())
            {
                if (_messageService.ConfirmationDialog("Информация", "В проекте есть оборудование, занесенное в бюджет. Вы уверены, что хотите удалить его?", defaultNo: true) == false)
                    return;
            }

            foreach (var salesUnit in salesUnits)
            {
                if (salesUnit.Order != null) continue;
                if (idIntersection.Contains(salesUnit.Id))
                {
                    salesUnit.IsRemoved = true;
                }
                else
                {
                    unitOfWork.Repository<SalesUnit>().Delete(salesUnit);
                }
            }
            unitOfWork.SaveChanges();

            var remove = _viewModel.ProjectItems.Where(projectItem => projectItem.Project.Id == _viewModel.SelectedProjectItem.Project.Id).ToList();
            remove.ForEach(projectItem => _viewModel.ProjectItems.Remove(projectItem));
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}