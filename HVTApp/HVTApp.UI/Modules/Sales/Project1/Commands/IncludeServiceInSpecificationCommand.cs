using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    internal class IncludeServiceInSpecificationCommand : ICommand
    {
        private readonly ProjectViewModel _projectViewModel;
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;
        private readonly ISelectService _selectService;

        public IncludeServiceInSpecificationCommand(
            ProjectViewModel projectViewModel, 
            IUnityContainer container)
        {
            _projectViewModel = projectViewModel;
            _container = container;
            _messageService = container.Resolve<IMessageService>();
            _selectService = container.Resolve<ISelectService>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ActionIsValid() == false) return;

            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var specifications = unitOfWork.Repository<SalesUnit>()
                    .Find(salesUnit =>
                        salesUnit.Specification != null &&
                        salesUnit.Project.Manager.Id == GlobalAppProperties.User.Id)
                    .Select(salesUnit => salesUnit.Specification)
                    .Distinct();

                var specification = _selectService.SelectItem(specifications);

                if (specification == null)
                    return;

                var salesUnits = _projectViewModel.GroupsViewModel.Groups.SelectedGroup.SalesUnits.Select(salesUnit =>
                    unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id));
                salesUnits.ForEach(salesUnit => salesUnit.Specification = specification);

                unitOfWork.SaveChanges();

                _messageService.Message("Уведомление", $"Выбранные услуги добавлены в спецификацию {specification}");
            }

        }

        public event EventHandler CanExecuteChanged;

        private bool ActionIsValid()
        {
            var salesUnits = _projectViewModel.GroupsViewModel.Groups.SelectedGroup?.SalesUnits;
            if (salesUnits == null)
            {
                _messageService.Message("Уведомление", "Выберите услугу для включения в спецификацию");
                return false;
            }

            if (salesUnits.Any(salesUnit => salesUnit.Product.ProductBlock.IsService == false))
            {
                _messageService.Message("Уведомление", "Данная опция доступна только для услуг");
                return false;
            }

            if (salesUnits.Any(salesUnit => salesUnit.Specification != null))
            {
                _messageService.Message("Уведомление", $"Услуга в спецификациях:\n{salesUnits.Select(salesUnit => salesUnit.Specification).Where(specification => specification != null).Distinct().Select(x => $"№{x.Number} к договору {x.Contract.Number}").ToStringEnum()}");
                return false;
            }

            return true;
        }
    }
}