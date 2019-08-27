using HVTApp.UI.Lookup;

namespace HVTApp.Modules.Sales.ViewModels
{
    public interface IShowable
    {
        void ShowByProject(ProjectLookup projectLookup);
    }
}