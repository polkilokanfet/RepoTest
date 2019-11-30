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
        public ObservableCollection<PriceCalculationItem> PriceCalculationItems { get; } = new ObservableCollection<PriceCalculationItem>();
        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
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
        public PriceCalculationItem(PriceCalculation priceCalculation)
        {
        }
    }
}