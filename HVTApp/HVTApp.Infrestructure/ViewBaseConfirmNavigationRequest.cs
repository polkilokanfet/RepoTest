using System;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public abstract class ViewBaseConfirmNavigationRequest : ViewBase, IConfirmNavigationRequest
    {
        private readonly IUnityContainer _container;

        //костыль
        public ViewBaseConfirmNavigationRequest()
        {
            
        }

        protected ViewBaseConfirmNavigationRequest(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
        }

        protected abstract bool IsSomethingChanged();

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var confirmNavigate = true;
            if (IsSomethingChanged())
            {
                var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Внимание!", "При переходе все несохраненные изменения будут потеряны. \nПерейти без сохранения изменений?", defaultYes: true);
                if (dr == MessageDialogResult.No)
                    confirmNavigate = false;
            }
            continuationCallback(confirmNavigate);
        }
    }
}