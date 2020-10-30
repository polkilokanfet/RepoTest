using System;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.SupplyModule.ViewModels
{
    public class SupplyPlanViewModel : ViewModelBaseCanExportToExcel
    {
        public ObservableCollection<SupplyPlanUnit> Units { get; } = new ObservableCollection<SupplyPlanUnit>();

        public SupplyPlanViewModel(IUnityContainer container) : base(container)
        {
            Load();
        }

        public void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                .Find(x => !x.IsRemoved && !x.IsLoosen && !x.Product.ProductBlock.IsService && !x.IsDone && x.EndProductionDateCalculated >= DateTime.Today && x.Project.ForReport);
            var units = salesUnits
                .GroupBy(x => new
                {
                    x.Product,
                    x.EndProductionDateCalculated
                })
                .Select(x => new SupplyPlanUnit(x))
                .OrderBy(x => x.SupplyDate);

            Units.Clear();
            Units.AddRange(units);
        }
    }
}