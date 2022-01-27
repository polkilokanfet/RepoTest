using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Production
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
                ProductionItems.ForEach(productionItem => productionItem.SignalToStartProduction = value);
            }
        }

        public DateTime EndProductionDateExpected
        {
            get => ProductionItems.First().EndProductionDateExpected;
            set { ProductionItems.ForEach(productionItem => productionItem.EndProductionDateExpected = value);}
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

        public bool IsProduced => ProductionItems.All(productionItem => productionItem.IsProduced);


        public int DifExpected => ProductionItems.First().DifExpected;

        public int DifContract => ProductionItems.First().DifContract;


        public void RejectChanges()
        {
            ProductionItems.RejectChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsValid => ProductionItems.IsValid;


        /// <summary>
        /// Очистка дат при удалении группы из производства
        /// </summary>
        public void CleanDatesOnRemoveFromProduction()
        {
            this.ProductionItems.ForEach(productionItem => productionItem.CleanDatesOnRemoveFromProduction());
        }
    }
}