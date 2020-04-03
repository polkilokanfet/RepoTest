using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public partial class FlatReportViewModel
    {
        public PaymentsPlanViewModel PaymentsPlanViewModel { get; }
        public ObservableCollection<SalesReportUnit> SalesReportUnits { get; } = new ObservableCollection<SalesReportUnit>();

        private void MakeReportExecuteMethod()
        {
            Items.ForEach(x => x.InjectOrderInTakeDates());
            var salesUnits = Items.Where(x => x.InReport).SelectMany(x => x.SalesUnits).ToList();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }

            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);
            var countryUnions = UnitOfWork.Repository<CountryUnion>().Find(x => true);

            var salesReportUnits = groups
                .Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), countryUnions, x.First().ActualPriceCalculationItem(UnitOfWork)))
                .ToList();

            SalesReportUnits.Clear();
            SalesReportUnits.AddRange(salesReportUnits);

            //формирование поступлений
            PaymentsPlanViewModel.Load(salesUnits);

            Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Формирование отчетов успешно завершено!");
        }
    }
}