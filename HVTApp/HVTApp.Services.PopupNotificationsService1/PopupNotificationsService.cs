using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Prism.Regions;

namespace HVTApp.Services.PopupNotificationsService1
{
    public class PopupNotificationsService : IPopupNotificationsService
    {
        private readonly IRegionManager _regionManager;

        public PopupNotificationsService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ShowPopupNotification<TModel>(TModel model, string text, string title = null) where TModel : BaseEntity
        {
            //переводим всплывающее окно в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    new PopupWindow(text, title, GetAction(model)).Show();
                });
        }

        private Action GetAction<TModel>(TModel model)
        {
            if (typeof(TModel) == typeof(DirectumTask))
                return () => _regionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { nameof(DirectumTask), model } });

            if (typeof(TModel) == typeof(PriceCalculation))
                return () => _regionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), model } });

            if (typeof(TModel) == typeof(TechnicalRequrementsTask))
                return () => _regionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), model } });

            if (typeof(TModel) == typeof(IncomingRequest))
                return () => _regionManager.RequestNavigateContentRegion<IncomingRequestsView>(new NavigationParameters());

            if (typeof(TModel) == typeof(PriceEngineeringTasks))
                return () => OpenPriceEngineeringTasks(_regionManager, model);
            //return () => _regionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters { { nameof(PriceEngineeringTasks), model } });

            if (typeof(TModel) == typeof(PriceEngineeringTask))
                return () => OpenPriceEngineeringTasks(_regionManager, model);
            //return () => _regionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters { { nameof(PriceEngineeringTask), model } });

            return () => { };
        }

        private void OpenPriceEngineeringTasks(IRegionManager regionManager, object parameter)
        {
            if (parameter != null)
            {
                var parameters = new NavigationParameters { { string.Empty, parameter } };
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                        regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(parameters);
                        break;

                    case Role.Constructor:
                        regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewConstructor>(parameters);
                        break;

                    case Role.BackManager:
                        regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);
                        break;

                    case Role.BackManagerBoss:
                        regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackOfficeBoss>(parameters);
                        break;

                    case Role.DesignDepartmentHead:
                        regionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(parameters);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}