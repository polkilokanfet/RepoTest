using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public interface IProjectUnit : INotifyPropertyChanged
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        string Comment { get; set; }

        FacilityEmptyWrapper Facility { get; set; }
        ProductEmptyWrapper Product { get; set; }
        PaymentConditionSetEmptyWrapper PaymentConditionSet { get; set; }
        CompanyEmptyWrapper Producer { get; set; }

        Specification Specification { get; }

        /// <summary>
        /// ¬ключенные продукты
        /// </summary>
        IEnumerable<ProjectUnitProductIncludedGroup> ProductsIncludedGroups { get; }

        void CopyProps(IProjectUnit projectUnit);
    }
}