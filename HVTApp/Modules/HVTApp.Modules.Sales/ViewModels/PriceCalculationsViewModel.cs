using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationsViewModel : ViewModelBase
    {
        public PriceCalculationItem SelectedCalculation { get; set; }

        public ObservableCollection<PriceCalculationLookup> PriceCalculations { get; } = new ObservableCollection<PriceCalculationLookup>();
        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Subscribe(
                priceCalculation =>
                {
                    if (PriceCalculations.ContainsById(priceCalculation))
                    {
                        PriceCalculations.GetById(priceCalculation).Refresh(priceCalculation);
                    }
                    else
                    {
                        var lookup = new PriceCalculationLookup(priceCalculation);
                        PriceCalculations.Add(lookup);
                    }
                });
        }

        public void Load()
        {
            var priceCalculations = UnitOfWork.Repository<PriceCalculation>()
                .Find(x => true)
                .OrderBy(x => x.TaskOpenMoment);
            PriceCalculations.Clear();
            PriceCalculations.AddRange(priceCalculations.Select(x => new PriceCalculationLookup(x)));
        }
    }
}