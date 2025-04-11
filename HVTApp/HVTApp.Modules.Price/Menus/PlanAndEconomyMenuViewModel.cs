using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.Modules.PlanAndEconomy.Dates;
using HVTApp.UI.Modules.PlanAndEconomy.InformationForTeamCenter;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.PlanAndEconomy.SpecificationSignDates;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.Specifications;
using HVTApp.UI.TaskInvoiceForPayment1.List;
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
                Items.Add(new NavigationItem("Технико-стоимостные проработки", typeof(PriceEngineeringTasksListViewPlanMaker)));
                Items.Add(new NavigationItem("План производства", typeof(ProductionPlanView)));
                Items.Add(new NavigationItem("Счета", typeof(TaskInvoiceForPaymentListView)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.Economist 
                || GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Спецификации", typeof(SpecificationsViewBase)));
                Items.Add(new NavigationItem("Поступления (факт)", typeof(PaymentsActualView)));
                Items.Add(new NavigationItem("Поступления (план)", typeof(PaymentsPlanView)));
                Items.Add(new NavigationItem("Фактические даты", typeof(DatesView)));
                //Items.Add(new NavigationItem("Задачи на счета", typeof(InformationForTeamCenterView)));
                Items.Add(new NavigationItem("Спецификации", typeof(SpecificationSignDatesView)));
                Items.Add(new NavigationItem("Шеф-монтаж", typeof(SupervisionView)));
                //Items.Add(new NavigationItem("Услуги", typeof(ServiceRealizationDatesView)));
            }

            if (GlobalAppProperties.UserIsBackManager || 
                GlobalAppProperties.UserIsBackManagerBoss || 
                GlobalAppProperties.User.RoleCurrent == Role.Admin)
            {
                Items.Add(new NavigationItem("Технико-стоимостные проработки", typeof(PriceEngineeringTasksListView)));
                Items.Add(new NavigationItem("Задачи в ТСЕ", typeof(TechnicalRequrementsTasksView)));
                Items.Add(new NavigationItem("Счета", typeof(TaskInvoiceForPaymentListView)));
                Items.Add(new NavigationItem("Расчеты ПЗ", typeof(PriceCalculationsView)));
                Items.Add(new NavigationItem("Спецификации", typeof(SpecificationsViewBase)));
            }
        }
    }
}
