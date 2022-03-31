using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Comparers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksViewModel : IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private PriceEngineeringTaskViewModel _selectedPriceEngineeringTaskViewModel;
        
        public ObservableCollection<PriceEngineeringTaskViewModel> PriceEngineeringTaskViewModels { get; } = new ObservableCollection<PriceEngineeringTaskViewModel>();

        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set => _selectedPriceEngineeringTaskViewModel = value;
        }

        public PriceEngineeringTasksViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
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
            var salesUnitsGrouped = salesUnits
                .Select(salesUnit => _unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .GroupBy(salesUnit => salesUnit, new SalesUnitForPriceEngineeringTaskComparer());

            foreach (var salesUnitsGroup in salesUnitsGrouped)
            {
                PriceEngineeringTaskViewModels.Add(PriceEngineeringTaskViewModelFactory.GetInstance(_container, _unitOfWork, salesUnitsGroup));
            }
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            this.PriceEngineeringTaskViewModels.ForEach(x => x.Dispose());
            this.PriceEngineeringTaskViewModels.Clear();
        }
    }
}