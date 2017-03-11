using System;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    class NavigationAwareViewModelBase : BindableBase, IConfirmNavigationRequest
    {

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback.Invoke(true);
        }
    }
}
