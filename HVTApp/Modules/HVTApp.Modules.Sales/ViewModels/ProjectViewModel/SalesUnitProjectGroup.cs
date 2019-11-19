using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitProjectGroup : IValidatableChangeTracking
    {
        public IValidatableChangeTrackingCollection<SalesUnitProjectItem> Units { get; }
        public SalesUnitProjectItem Unit => Units.First();
        public int Amount => Units.Count;

        public double Cost
        {
            get { return Unit.Cost; }
            set
            {
                Units.ForEach(x => x.CostStructure.Cost = value);
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total => Amount * Cost;

        public double? MarginalIncome
        {
            get { return Unit.CostStructure.MarginalIncome; }
            set
            {
                Units.ForEach(x => x.CostStructure.MarginalIncome = value);
                OnPropertyChanged(nameof(Cost));
                OnPropertyChanged(nameof(Total));
            }
        }

        public int ProductionTerm
        {
            get { return Unit.ProductionTerm; }
            set { Units.ForEach(x => x.ProductionTerm = value); }
        }

        public DateTime DeliveryDateExpected
        {
            get { return Unit.DeliveryDateExpected; }
            set { Units.ForEach(x => x.DeliveryDateExpected = value); }
        }

        public double? CostDelivery
        {
            get { return Unit.CostDelivery; }
            set { Units.ForEach(x => x.CostDelivery = value); }
        }

        public bool CostDeliveryIncluded
        {
            get { return Unit.CostDeliveryIncluded; }
            set { Units.ForEach(x => x.CostDeliveryIncluded = value); }
        }

        #region ComplexProperties

        public FacilityWrapper Facility
        {
            get { return Unit.Facility; }
            set { Units.ForEach(x => x.Facility = value); }
        }


        public ProductWrapper Product
        {
            get { return Unit.Product; }
            set { Units.ForEach(x => x.Product = value); }
        }


        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return Unit.PaymentConditionSet; }
            set { Units.ForEach(x => x.PaymentConditionSet = value); }
        }


        public ProjectWrapper Project
        {
            get { return Unit.Project; }
            set { Units.ForEach(x => x.Project = value); }
        }


        public CompanyWrapper Producer
        {
            get { return Unit.Producer; }
            set { Units.ForEach(x => x.Producer = value); }
        }


        public SpecificationWrapper Specification
        {
            get { return Unit.Specification; }
            set { Units.ForEach(x => x.Specification = value); }
        }


        public PenaltyWrapper Penalty
        {
            get { return Unit.Penalty; }
            set { Units.ForEach(x => x.Penalty = value); }
        }


        public AddressWrapper AddressDelivery
        {
            get { return Unit.AddressDelivery; }
            set { Units.ForEach(x => x.AddressDelivery = value); }
        }

        #endregion



        public SalesUnitProjectGroup(IEnumerable<SalesUnitProjectItem> salesUnits)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitProjectItem>(salesUnits);

            Units.PropertyChanged += (sender, args) =>
            {
                PropertyChanged?.Invoke(this, args);
            };

            Units.CollectionChanged += (sender, args) =>
            {
                if (args.Action != NotifyCollectionChangedAction.Add &&
                    args.Action != NotifyCollectionChangedAction.Remove)
                    return;

                OnPropertyChanged(nameof(Unit));
                OnPropertyChanged(nameof(Amount));
            };
        }

        public bool IsChanged => Units.IsChanged;
        public bool IsValid => Units.IsValid;

        public void AcceptChanges()
        {
            Units.AcceptChanges();
        }

        public void RejectChanges()
        {
            Units.RejectChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}