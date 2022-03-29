using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModel : BindableBase, IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;

        public PriceEngineeringTaskWrapper PriceEngineeringTaskWrapper { get; } = new PriceEngineeringTaskWrapper(new PriceEngineeringTask());

        public ObservableCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTaskViewModels { get; } = new ObservableCollection<PriceEngineeringTaskViewModel>();

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Создание ViewModel в соответствии с текущей ролью пользователя
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    return new PriceEngineeringTaskViewModelManager(container, unitOfWork);
                }
                case Role.Constructor:
                {
                    return new PriceEngineeringTaskViewModelConstructor(container, unitOfWork);
                }
                case Role.DesignDepartmentHead:
                {
                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, unitOfWork);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Загрузка при создании новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        /// <param name="product"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits, Product product = null)
        {
            PriceEngineeringTaskWrapper.Amount = salesUnits.Count();

            PriceEngineeringTaskWrapper.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitWrapper(salesUnit)));

            if (product == null)
            {
                product = salesUnits.First().Product;
            }

            ProductBlock productBlock = _unitOfWork.Repository<ProductBlock>().GetById(product.ProductBlock.Id);
            ProductBlockWrapper productBlockWrapper = new ProductBlockWrapper(productBlock);
            PriceEngineeringTaskWrapper.ProductBlockEngineer = PriceEngineeringTaskWrapper.ProductBlockManager = productBlockWrapper;

            foreach (var dependentProduct in product.DependentProducts)
            {
                PriceEngineeringTaskViewModel childEngineeringTaskViewModel = PriceEngineeringTaskViewModel.GetInstance(_container, _unitOfWork);
                childEngineeringTaskViewModel.Load(salesUnits, dependentProduct.Product);
                this.ChildPriceEngineeringTaskViewModels.Add(childEngineeringTaskViewModel);
                this.PriceEngineeringTaskWrapper.ChildPriceEngineeringTasks.Add(childEngineeringTaskViewModel.PriceEngineeringTaskWrapper);
            }

            RaisePropertyChanged(nameof(PriceEngineeringTaskWrapper));
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}