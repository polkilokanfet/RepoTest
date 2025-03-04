using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public interface IProjectUnit : INotifyPropertyChanged, IIsValid
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        string Comment { get; set; }

        bool IsRemoved { get; }

        FacilityEmptyWrapper Facility { get; set; }
        ProductEmptyWrapper Product { get; set; }
        PaymentConditionSetEmptyWrapper PaymentConditionSet { get; set; }
        CompanyEmptyWrapper Producer { get; set; }

        Specification Specification { get; }

        /// <summary>
        /// ¬ключенные продукты
        /// </summary>
        IEnumerable<ProjectUnitProductIncludedGroup> ProductsIncludedGroups { get; }


        Price Price { get; }

        ProjectUnitCalculatedParts CalculatedParts { get; }
    }
}