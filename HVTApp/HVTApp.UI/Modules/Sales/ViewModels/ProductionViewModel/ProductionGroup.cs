using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProductionGroup : IValidatableChangeTracking
    {
        public SalesUnit SalesUnit => ProductionItems.First().Model;

        public IValidatableChangeTrackingCollection<ProductionItem> ProductionItems { get; }

        public int Amount => ProductionItems.Count;

        public DateTime? SignalToStartProduction
        {
            set
            {
                ProductionItems.ForEach(x => x.SignalToStartProduction = value);
            }
        }

        public DateTime EndProductionDateExpected
        {
            get { return ProductionItems.First().EndProductionDateExpected; }
            set { ProductionItems.ForEach(x => x.EndProductionDateExpected = value);}
        }

        public ProductionGroup(IEnumerable<ProductionItem> productionItems)
        {
            ProductionItems = new ValidatableChangeTrackingCollection<ProductionItem>(productionItems);
            ProductionItems.PropertyChanged += (sender, args) =>
            {
                PropertyChanged?.Invoke(this, args);
            };
        }

        public void AcceptChanges()
        {
            ProductionItems.AcceptChanges();
        }

        public bool IsChanged => ProductionItems.IsChanged;

        public void RejectChanges()
        {
            ProductionItems.RejectChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsValid => ProductionItems.IsValid;

    }
}