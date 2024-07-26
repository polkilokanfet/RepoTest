using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class IncludeInSpecificationCommand : DelegateLogCommand
    {
        protected readonly IUnityContainer Container;
        private readonly Func<bool> _canExecute;
        private readonly IMessageService _messageService;



        public IncludeInSpecificationCommand(IUnityContainer container, Func<bool> canExecute)
        {
            Container = container;
            _canExecute = canExecute;
            _messageService = container.Resolve<IMessageService>();
        }

        protected override void ExecuteMethod()
        {
            throw new NotImplementedException();
        }

        protected virtual bool AllowExecute(ISalesUnitsContainer salesUnitsContainer)
        {
            var dr = _messageService.ConfirmationDialog("Вы уверены, что хотите включить данное оборудование в спецификацию?");
            if (dr == false) return false;

            if (salesUnitsContainer.SalesUnits.Any(salesUnit => salesUnit.Specification != null))
            {
                Container.Resolve<IMessageService>().Message("Отказ", "В задаче есть оборудование, которое уже включено в спецификацию.");
                return false;
            }
            if (salesUnitsContainer.SalesUnits.Any(salesUnit => salesUnit.IsRemoved))
            {
                Container.Resolve<IMessageService>().Message("Отказ", "В задаче есть удалённое Вами оборудование.");
                return false;
            }
            return true;
        }

        protected override void ExecuteMethod(object parameter)
        {
            if (!(parameter is ISalesUnitsContainer salesUnitsContainer)) throw new ArgumentException();

            if (this.AllowExecute(salesUnitsContainer) == false) return;

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var specification = this.GetSpecification(unitOfWork);
            if (specification == null) return;

            switch (salesUnitsContainer)
            {
                case TechnicalRequrements technicalRequrements:
                {
                    var tr = unitOfWork.Repository<TechnicalRequrements>().GetById(technicalRequrements.Id);
                    specification.TechnicalRequrements.Add(tr);
                    break;
                }
                case PriceEngineeringTask priceEngineeringTask:
                {
                    var task = unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id);
                    specification.PriceEngineeringTasks.Add(task);
                    break;
                }
            }

            foreach (var salesUnit in salesUnitsContainer.SalesUnits)
            {
                var su = unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id);
                su.Specification = specification;
            }

            unitOfWork.SaveChanges();
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Specification), specification } });
        }

        private Specification GetSpecification(IUnitOfWork unitOfWork)
        {
            //спецификации менеджера
            var specifications = unitOfWork.Repository<Specification>()
                .Find(specification1 =>
                    specification1.PriceEngineeringTasks.SelectMany(x => x.SalesUnits)
                        .Any(xx => xx.Project.Manager.Id == GlobalAppProperties.User.Id) ||
                    specification1.TechnicalRequrements.SelectMany(x => x.SalesUnits)
                        .Any(xx => xx.Project.Manager.Id == GlobalAppProperties.User.Id));
            var specification = Container.Resolve<ISelectService>().SelectItem(specifications);
            return specification == null 
                ? null 
                : unitOfWork.Repository<Specification>().GetById(specification.Id);
        }

        protected override bool CanExecuteMethod()
        {
            return _canExecute.Invoke();
        }
    }
}