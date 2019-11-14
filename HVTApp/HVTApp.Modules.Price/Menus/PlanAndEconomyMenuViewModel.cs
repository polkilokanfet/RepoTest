using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Modules.PlanAndEconomy.Views;

namespace HVTApp.Modules.PlanAndEconomy.Menus
{
    public class PlanAndEconomyMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.Pricer || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Переменные затраты", typeof(PricesView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.PlanMaker || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("План производства", typeof(ProductionPlanView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.Economist || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Фактические даты", typeof(DatesView)));
                Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
                Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsView)));
            }
        }
    }
}
