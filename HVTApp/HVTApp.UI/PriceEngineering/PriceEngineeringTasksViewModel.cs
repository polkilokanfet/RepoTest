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
        
        public PriceEngineeringTasksWrapper1 PriceEngineeringTasksWrapper { get; private set; }

        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set => _selectedPriceEngineeringTaskViewModel = value;
        }

        public PriceEngineeringTasksViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;

            PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(new PriceEngineeringTasks(), container, unitOfWork);
        }

        /// <summary>
        /// �������� ��� �������� ����� �������-����������� ���������� �� �������� ������
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsGrouped = salesUnits
                .Select(salesUnit => _unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .GroupBy(salesUnit => salesUnit, new SalesUnitForPriceEngineeringTaskComparer());

            foreach (var salesUnitsGroup in salesUnitsGrouped)
            {
                PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Add(PriceEngineeringTaskViewModelFactory.GetInstance(_container, _unitOfWork, salesUnitsGroup));
            }
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(x => x.Dispose());
        }
    }
}