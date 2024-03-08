using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PriceService1.Containers
{
    /// <summary>
    /// Контейнер стоимостей нормо-часов на дату
    /// </summary>
    internal class LaborHourCostsContainer
    {
        private readonly IUnityContainer _container;
        private List<LaborHourCost> _laborHourCosts = null;

        public LaborHourCostsContainer(IUnityContainer container)
        {
            _container = container;
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            _laborHourCosts = unitOfWork.Repository<LaborHourCost>().GetAll();
        }

        public double GetLaborHoursCost(DateTime targetDate)
        {
            if (_laborHourCosts == null)
                Reload();

            return _laborHourCosts.GetClosedSumOnDate(targetDate).Sum;
        }

    }
}