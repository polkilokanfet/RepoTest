using System.Collections;
using System.Collections.Generic;
using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public interface IViewBase
    {
        BindableBase ViewModel { get; set; }
        IList<IRibbonTabItem> RibbonTabs { get; }
    }
}