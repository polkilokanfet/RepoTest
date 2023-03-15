using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.PlanAndEconomy.Dates;
using HVTApp.UI.Modules.PlanAndEconomy.Dates.ServiceRealizationDates;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.PlanAndEconomy.SpecificationSignDates;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceEngineering.Tce.List.View;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;

namespace HVTApp.Modules.PlanAndEconomy.Menus
{
    public class PlanAndEconomyMenuViewModel : BaseMenuViewModel
    {
        protected override void GenerateMenu()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.PlanMaker 
                || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Технико-стоимостные проработки", typeof(PriceEngineeringTasksListView)));
                Items.Add(new NavigationItem("План производства", typeof(ProductionPlanView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.Economist 
                || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
                Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsPlanView)));
                Items.Add(new NavigationItem("Фактические даты", typeof(DatesView)));
                Items.Add(new NavigationItem("Спецификации", typeof(SpecificationSignDatesView)));
                Items.Add(new NavigationItem("Шеф-монтаж", typeof(SupervisionView)));
                //Items.Add(new NavigationItem("Услуги", typeof(ServiceRealizationDatesView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager || 
                GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss || 
                GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                //Items.Add(new NavigationItem("Задачи в ТСЕ (новое)", typeof(PriceEngineeringTasksTceView2)));
                Items.Add(new NavigationItem("Технико-стоимостные проработки", typeof(PriceEngineeringTasksListView)));
                Items.Add(new NavigationItem("Задачи в ТСЕ", typeof(TechnicalRequrementsTasksView)));
                Items.Add(new NavigationItem("Расчеты ПЗ", typeof(PriceCalculationsView)));
            }
        }
    }
}
