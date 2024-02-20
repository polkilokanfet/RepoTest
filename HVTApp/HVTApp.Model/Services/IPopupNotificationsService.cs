using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Services
{
    public interface IPopupNotificationsService
    {
        void ShowPopupNotification<TModel>(TModel model, string text, string title = null)
            where TModel : BaseEntity;

        void ShowNotification(string text, string title = null, Action action = null);
    }
}