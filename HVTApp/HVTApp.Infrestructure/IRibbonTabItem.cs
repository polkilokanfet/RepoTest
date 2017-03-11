using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public interface IRibbonTabItem
    {
        BindableBase ViewModel { get; set; }
        bool IsSelected { get; set; }
    }
}
