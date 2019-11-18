using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public interface ICostStructureItem
    {
        double Cost { get; set; }
        ProductWrapper Product { get; set; }

        IUnit GetIUnit();
        DateTime GetPriceDate();
    }
}