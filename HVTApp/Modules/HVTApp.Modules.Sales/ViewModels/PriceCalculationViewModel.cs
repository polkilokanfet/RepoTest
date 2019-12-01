using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationViewModel : ViewModelBase
    {
        public PriceCalculationWrapper PriceCalculationWrapper { get; private set; }

        public List<SalesUnitsPriceCalculationGroup> Groups { get; } = new List<SalesUnitsPriceCalculationGroup>();

        public PriceCalculationViewModel(IUnityContainer container) : base(container)
        {
        }

        /// <summary>
        /// Загрузка при создании нового расчета
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id))
                .Select(x => new SalesUnitWrapper(x))
                .ToList();

            PriceCalculationWrapper = new PriceCalculationWrapper(new PriceCalculation());
            salesUnitWrappers.ForEach(x => PriceCalculationWrapper.SalesUnits.Add(x));

            var groups = salesUnitWrappers.GroupBy(x => new
            {
                Product = x.Product.Id,
                Facility = x.Facility.Id,
                OrderInTakeDate = x.OrderInTakeDate,
                RealizationDate = x.RealizationDateCalculated,
                PaymentConditionSet = x.PaymentConditionSet.Id,
                StructureCosts = x.StructureCosts?.Id
            });

            Groups.AddRange(groups.Select(x => new SalesUnitsPriceCalculationGroup(x)).ToList());
        }
    }

    public class SalesUnitsPriceCalculationGroup
    {
        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount { get; }
        public DateTime OrderInTakeDate { get; }
        public DateTime RealizationDate { get; }
        public PaymentConditionSet PaymentConditionSet { get; }
        public StructureCostsWrapper StructureCostsWrapper { get; }
        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCostWrappers => StructureCostsWrapper.StructureCostsList;

        public SalesUnitsPriceCalculationGroup(IEnumerable<SalesUnitWrapper> salesUnitWrappers)
        {
            var salesUnits = salesUnitWrappers as IList<SalesUnitWrapper> ?? salesUnitWrappers.ToList();
            var salesUnit = salesUnits.First().Model;

            Facility = salesUnit.Facility;
            Product = salesUnit.Product;
            Amount = salesUnits.Count;
            OrderInTakeDate = salesUnit.OrderInTakeDate;
            RealizationDate = salesUnit.RealizationDateCalculated;
            PaymentConditionSet = salesUnit.PaymentConditionSet;

            StructureCostsWrapper = salesUnit.StructureCosts == null 
                ? new StructureCostsWrapper(new StructureCosts()) 
                : new StructureCostsWrapper(salesUnit.StructureCosts);

            if (salesUnit.StructureCosts == null)
            {
                salesUnits.ForEach(x => x.StructureCosts = StructureCostsWrapper);

                //создание основного стракчакоста
                var structureCost = new StructureCost
                {
                    Comment = $"{Product}"
                };
                StructureCostsWrapper.StructureCostsList.Add(new StructureCostWrapper(structureCost));

                //создание стракчакостов доп.оборудования
                foreach (var productIncluded in salesUnit.ProductsIncluded)
                {
                    var structureCostPrIncl = new StructureCost
                    {
                        Comment = $"{productIncluded.Product}",
                        Amount = (double)productIncluded.Amount/Amount
                    };
                    StructureCostsWrapper.StructureCostsList.Add(new StructureCostWrapper(structureCostPrIncl));
                }
            }
        }
    }
}