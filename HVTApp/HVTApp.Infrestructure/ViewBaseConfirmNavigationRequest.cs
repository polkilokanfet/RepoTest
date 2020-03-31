using System;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public abstract class ViewBaseConfirmNavigationRequest : ViewBase, IConfirmNavigationRequest
    {
        protected readonly IUnityContainer Container;

        //костыль
        public ViewBaseConfirmNavigationRequest()
        {
            
        }

        protected ViewBaseConfirmNavigationRequest(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            Container = container;
        }

        protected abstract bool IsSomethingChanged();

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var confirmNavigate = true;
            if (IsSomethingChanged())
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Внимание!", "При переходе все несохраненные изменения будут потеряны. \nПерейти без сохранения изменений?", defaultYes: true);
                if (dr == MessageDialogResult.No)
                    confirmNavigate = false;
            }
            continuationCallback(confirmNavigate);
        }
    }
}