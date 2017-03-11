using Infragistics.Windows.Ribbon;
using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public class RibbonTabItemWithViewModel : RibbonTabItem, IRibbonTabItem
    {
        public BindableBase ViewModel
        {
            get { return (BindableBase)DataContext; }
            set { DataContext = value; }
        }
    }
}