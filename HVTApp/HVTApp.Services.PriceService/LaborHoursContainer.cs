using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PriceService1
{
    /// <summary>
    /// Список всех известных нормо-часов на производство блоков оборудования
    /// </summary>
    internal class LaborHoursContainer
    {
        private readonly IUnityContainer _container;
        private List<LaborHours> _laborHoursList = null;

        public LaborHoursContainer(IUnityContainer container)
        {
            _container = container;
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            _laborHoursList = unitOfWork.Repository<LaborHours>().GetAll();
        }

        /// <summary>
        /// Получение нормо-часов на изготовление блока оборудования
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(ProductBlock block)
        {
            if (_laborHoursList == null)
                Reload();

            var laborHours = _laborHoursList
                .Where(hours => EnumerableExtansions.AllContainsIn(hours.Parameters, block.Parameters))
                .ToList();

            if (laborHours.Any())
            {
                return laborHours.OrderBy(hours => hours.Parameters.Count).Last().Amount;
            }

            return null;
        }

    }
}