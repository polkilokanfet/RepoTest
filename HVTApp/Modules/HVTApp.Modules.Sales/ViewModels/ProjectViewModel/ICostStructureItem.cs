using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public interface ICostStructureItem
    {
        double Cost { get; set; }
        ProductWrapper Product { get; set; }

        DateTime PriceDate { get; }
        IUnit GetIUnit();

        event Action ProductChanged;
        event Action ProductsIncludedCollectionChanged;

    }
}