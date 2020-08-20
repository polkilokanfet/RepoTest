using System;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class LoadableExportableExpandCollapseViewModel : LoadableExportableViewModel
    {
        public ICommand ExpandCommand { get; }
        public ICommand CollapseCommand { get; }

        public event Action<bool> ExpandCollapseEvent;

        protected LoadableExportableExpandCollapseViewModel(IUnityContainer container) : base(container)
        {
            //развернуть
            ExpandCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(true); });
            //свернуть
            CollapseCommand = new DelegateCommand(() => { ExpandCollapseEvent?.Invoke(false); });
        }
    }
}