using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.FlatReport.Comparator
{
    public class BudgetComparisionViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {

        public ObservableCollection<BudgetComparisonItem> Items { get; } = new ObservableCollection<BudgetComparisonItem>();

        public BudgetComparisionViewModel(IEnumerable<FlatReportItem> flatReportItems1, IEnumerable<BudgetUnit> budgetUnits1, IUnityContainer container) : base(container)
        {
            var flatReportItems = flatReportItems1.ToList();
            var budgetUnits = budgetUnits1.ToList();

            var items = new List<BudgetComparisonItem>();

            foreach (var flatReportItem in flatReportItems)
            {
                foreach (var salesUnit in flatReportItem.SalesUnits)
                {
                    var budgetUnit = budgetUnits.SingleOrDefault(x => Equals(x.SalesUnit, salesUnit));
                    items.Add(new BudgetComparisonItem(budgetUnit, flatReportItem));
                    if (budgetUnit != null)
                        budgetUnits.Remove(budgetUnit);
                }
            }

            items.AddRange(budgetUnits.Where(x => !x.IsRemoved).Select(budgetUnit => new BudgetComparisonItem(budgetUnit)));

            Items.AddRange(items.Where(x => x.ToComparision));

            //foreach (var flatReportItem in flatReportItems)
            //{
            //    //если айтемы в новом бюджете
            //    if (flatReportItem.InReport)
            //    {
            //        foreach (var salesUnit in flatReportItem.SalesUnits)
            //        {
            //            var budgetUnit = budgetUnits.SingleOrDefault(x => Equals(x.SalesUnit, salesUnit));
            //            //если айтем "засветился" старом бюджете
            //            if (budgetUnit != null)
            //            {
            //                //если айтем был удален из старого бюджета
            //                if (budgetUnit.IsRemoved)
            //                {
            //                    //то айтем "новый"
            //                    items.Add(new BudgetComparisonItem(budgetUnit, isNew:true));
            //                }
            //                //если айтмы присутствуют в старом бюджете
            //                else
            //                {
            //                    //то айтем "старый"
            //                    items.Add(new BudgetComparisonItem(budgetUnit));
            //                }
            //                budgetUnits.Remove(budgetUnit);
            //            }
            //            //если айтем не "засветился" старом бюджете
            //            else
            //            {
            //                items.Add(new BudgetComparisonItem(new BudgetUnit(salesUnit), isNew:true));
            //            }
            //        }
            //    }
            //    //если айтем удалён из нового бюджета
            //    else
            //    {
            //        foreach (var salesUnit in flatReportItem.SalesUnits)
            //        {
            //            var budgetUnit = budgetUnits.SingleOrDefault(x => Equals(x.SalesUnit, salesUnit));
            //            //если айтем "засветился" старом бюджете
            //            if (budgetUnit != null)
            //            {
            //                //если айтем был удален из старого бюджета
            //                if (budgetUnit.IsRemoved)
            //                {
            //                    //то ничего и не добавляем
            //                }
            //                //если айтмы присутствуют в старом бюджете
            //                else
            //                {
            //                    //то айтем "удален"
            //                    items.Add(new BudgetComparisonItem(budgetUnit, isRemoved:true));
            //                }
            //                budgetUnits.Remove(budgetUnit);
            //            }
            //        }
            //    }
            //}

            ////свободные бюджетные айтемы (единицы продаж удалены из бюджета)
            //items.AddRange(budgetUnits.Where(x => !x.IsRemoved).Select(x => new BudgetComparisonItem(x, true)));

            //Items.AddRange(items.OrderBy(x => x.BudgetUnit.OrderInTakeDate));
        }
    }
}