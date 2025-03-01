using System.Collections.Generic;
using System.Windows;
using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public interface IViewBase
    {
        BindableBase ViewModel { get; set; }
        IList<IRibbonTabItem> RibbonTabs { get; }
        event DependencyPropertyChangedEventHandler DataContextChanged;
    }
}