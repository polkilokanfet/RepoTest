using System;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationsViewModel : ViewModelBase
    {
        public PriceCalculationItem SelectedCalculation { get; set; }

        public ObservableCollection<PriceCalculationItem> PriceCalculationItems { get; } = new ObservableCollection<PriceCalculationItem>();
        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();
        }

        public void Load()
        {
            var priceCalculations = UnitOfWork.Repository<PriceCalculation>()
                .Find(x => x.Author.IsAppCurrentUser())
                .OrderBy(x => x.TaskOpenMoment);
            PriceCalculationItems.Clear();
            PriceCalculationItems.AddRange(priceCalculations.Select(x => new PriceCalculationItem(x)));
        }
    }

    public class PriceCalculationItem : BindableBase
    {
        private readonly PriceCalculation _priceCalculation;

        public DateTime? TaskOpenMoment => _priceCalculation.TaskOpenMoment;
        public DateTime? TaskCloseMoment => _priceCalculation.TaskCloseMoment;
        public string Name => _priceCalculation.Name;

        public PriceCalculationItem(PriceCalculation priceCalculation)
        {
            _priceCalculation = priceCalculation;
        }
    }
}