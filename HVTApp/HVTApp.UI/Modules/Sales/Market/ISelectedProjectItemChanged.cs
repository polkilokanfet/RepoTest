using System;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market
{
    public interface ISelectedProjectItemChanged
    {
        ProjectItem SelectedProjectItem { get; }
        event Action<ProjectItem> SelectedProjectItemChanged;
    }
}