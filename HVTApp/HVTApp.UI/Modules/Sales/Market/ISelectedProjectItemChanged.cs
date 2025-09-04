using System;
using HVTApp.UI.Modules.Sales.Market.Items;

namespace HVTApp.UI.Modules.Sales.Market
{
    public interface ISelectedProjectItemChanged
    {
        MarketProjectItem SelectedProjectItem { get; }
        event Action<MarketProjectItem> SelectedProjectItemChanged;
    }
}