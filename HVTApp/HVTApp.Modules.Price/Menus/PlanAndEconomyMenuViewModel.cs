using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using DatesView = HVTApp.UI.Modules.PlanAndEconomy.Dates.DatesView;
using PaymentsActualView = HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual.PaymentsActualView;
using PaymentsPlanView = HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan.PaymentsPlanView;

namespace HVTApp.Modules.PlanAndEconomy.Menus
{
    public class PlanAndEconomyMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.PlanMaker || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("План производства", typeof(ProductionPlanView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.Economist || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
                Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsPlanView)));

                Items.Add(new NavigationItem("Фактические даты", typeof(DatesView)));

                Items.Add(new NavigationItem("Шеф-монтаж", typeof(SupervisionView)));
            }
        }
    }
}
