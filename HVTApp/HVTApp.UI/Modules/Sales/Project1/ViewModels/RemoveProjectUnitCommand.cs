using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class RemoveProjectUnitCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly IMessageService _messageService;
        private readonly IUnitOfWork _unitOfWork;


        public RemoveProjectUnitCommand(ProjectViewModel viewModel, IMessageService messageService, IUnitOfWork unitOfWork)
        {
            _viewModel = viewModel;
            _messageService = messageService;
            _unitOfWork = unitOfWork;
            _viewModel.SelectedUnitChanged += RaiseCanExecuteChanged;
        }
        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedUnit != null;
        }

        public override void Execute(object parameter)
        {
            var dr1 = _messageService.ConfirmationDialog("Вы уверены в удалении?");
            if (dr1 != true) return;


            var salesUnits = _viewModel.SelectedUnit is ProjectUnitGroup projectUnitGroup
                ? projectUnitGroup.Units.Select(x => x.Model).ToList()
                : new List<SalesUnit> { ((ProjectUnit)_viewModel.SelectedUnit).Model };

            //если ни один юнит ещё не сохранен в БД
            if (salesUnits.All(salesUnit => _unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id) == null))
            {
                salesUnits.ForEach(this.Remove);
                return;
            }

            if (salesUnits.Any(x => x.Order != null))
            {
                _messageService.Message("Удаление невозможно", "Оборудованию присвоен заводской заказ.");
                return;
            }

            //проверяем не включено ли оборудование в какой-либо бюджет
            var budgetUnits = _unitOfWork.Repository<BudgetUnit>().Find(budgetUnit => budgetUnit.IsRemoved == false);
            var idIntersection = salesUnits
                .Select(salesUnit => salesUnit.Id)
                .Intersect(budgetUnits.Select(budgetUnit => budgetUnit.SalesUnit.Id))
                .ToList();
            if (idIntersection.Any())
            {
                var dr = _messageService.ConfirmationDialog("Это оборудование включено в бюджет. Вы уверены, что хотите удалить его?");

                if (dr)
                    salesUnits.Where(salesUnit => idIntersection.Contains(salesUnit.Id)).ForEach(salesUnit => salesUnit.IsRemoved = true);
                else
                    return;
            }

            //проверка на включение в задачи ТСП
            var salesUnitsInTasks = _unitOfWork.Repository<PriceEngineeringTask>()
                .Find(priceEngineeringTask => priceEngineeringTask.SalesUnits.Any())
                .SelectMany(priceEngineeringTask => priceEngineeringTask.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTasks))
            {
                salesUnit.IsRemoved = true;
            }

            //проверка на включение в задачи TCE
            var salesUnitsInTce = _unitOfWork.Repository<TechnicalRequrements>()
                .Find(technicalRequrements => technicalRequrements.SalesUnits.Any())
                .SelectMany(technicalRequrements => technicalRequrements.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTce))
            {
                salesUnit.IsRemoved = true;
            }

            _unitOfWork.Repository<SalesUnit>().DeleteRange(salesUnits.Where(salesUnit => salesUnit.IsRemoved == false));
            salesUnits.ForEach(Remove);
        }

        private void Remove(SalesUnit salesUnit)
        {
            var projectUnit = _viewModel.ProjectWrapper.Units.Single(x => x.Model.Id == salesUnit.Id);
            _viewModel.ProjectWrapper.Units.Remove(projectUnit);
        }
    }
}