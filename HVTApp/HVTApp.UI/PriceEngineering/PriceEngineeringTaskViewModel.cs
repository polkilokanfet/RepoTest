using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModel : IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;

        public PriceEngineeringTaskWrapper PriceEngineeringTaskWrapper { get; } = new PriceEngineeringTaskWrapper(new PriceEngineeringTask());

        public PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Загрузка при создании новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            PriceEngineeringTaskWrapper.Amount = salesUnits.Count();
            ProductBlockWrapper productBlockWrapper = new ProductBlockWrapper(_unitOfWork.Repository<ProductBlock>().GetById(salesUnits.First().Product.ProductBlock.Id));
            PriceEngineeringTaskWrapper.ProductBlockEngineer = PriceEngineeringTaskWrapper.ProductBlockManager = productBlockWrapper;
            PriceEngineeringTaskWrapper.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitWrapper(salesUnit)));
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}