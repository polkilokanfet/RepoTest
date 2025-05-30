using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    internal class IncludeServiceInSpecificationCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;
        private readonly ISelectService _selectService;

        public IncludeServiceInSpecificationCommand(ProjectViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
            _messageService = container.Resolve<IMessageService>();
            _selectService = container.Resolve<ISelectService>();

            _viewModel.SelectedUnitChanged += RaiseCanExecuteChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedUnit != null && 
                   _viewModel.SelectedUnit.Product.Model.ProductBlock.IsService;
        }

        public override void Execute(object parameter)
        {
            var salesUnits = _viewModel.SelectedUnit is ProjectUnitGroup projectUnitGroup
                ? projectUnitGroup.Units.Select(projectUnit => projectUnit.Model).ToList()
                : new List<SalesUnit>() { ((ProjectUnit)_viewModel.SelectedUnit).Model };

            if (salesUnits.Any(salesUnit => salesUnit.Specification != null))
            {
                _messageService.Message("Уведомление", $"Услуга в спецификациях:\n{salesUnits.Select(salesUnit => salesUnit.Specification).Where(specification => specification != null).Distinct().Select(x => $"№{x.Number} к договору {x.Contract.Number}").ToStringEnum()}");
                return;
            }


            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var specifications = unitOfWork.Repository<SalesUnit>()
                    .Find(salesUnit =>
                        salesUnit.Specification != null &&
                        salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id)
                    .Select(salesUnit => salesUnit.Specification)
                    .Distinct();

                var specification = _selectService.SelectItem(specifications);
                if (specification == null) return;
                specification = unitOfWork.Repository<Specification>().GetById(specification.Id);

                salesUnits = salesUnits.Select(salesUnit => unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id)).ToList();
                salesUnits.ForEach(salesUnit => salesUnit.Specification = specification);

                unitOfWork.SaveChanges();

                _messageService.Message("Уведомление", $"Выбранные услуги добавлены в спецификацию {specification}");
            }

        }
    }
}