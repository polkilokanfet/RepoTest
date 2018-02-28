using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        public ICommand AddOfferUnitCommand { get; private set; }
        public ICommand EditOfferUnitCommand { get; private set; }

        protected override void InitCommands()
        {
            AddOfferUnitCommand = new DelegateCommand(AddOfferUnitCommand_Execute);
            EditOfferUnitCommand = new DelegateCommand(EditOfferUnitCommand_Execute, EditOfferUnitCommand_CanExecute);
        }

        private async void AddOfferUnitCommand_Execute()
        {
            var offerUnitTempl = SelectedOfferUnitsItem ?? Item.OfferUnits.FirstOrDefault();
            OfferUnitWrapper wrapper = new OfferUnitWrapper(new OfferUnit());
            if (offerUnitTempl != null)
            {
                wrapper.Product = offerUnitTempl.Product;
                wrapper.Cost = offerUnitTempl.Cost;
                wrapper.Facility = offerUnitTempl.Facility;
                wrapper.ProductionTerm = offerUnitTempl.ProductionTerm;
                wrapper.PaymentConditionSet = offerUnitTempl.PaymentConditionSet;
            }
            if (await Container.Resolve<IUpdateDetailsService>().UpdateDetails<OfferUnit, OfferUnitWrapper>(wrapper, UnitOfWork))
            {
                Item.OfferUnits.Add(wrapper);
            }
        }

        private bool EditOfferUnitCommand_CanExecute()
        {
            return SelectedOfferUnitsItem != null;
        }

        private async void EditOfferUnitCommand_Execute()
        {
            if (!await Container.Resolve<IUpdateDetailsService>().UpdateDetails<OfferUnit, OfferUnitWrapper>(SelectedOfferUnitsItem, UnitOfWork))
                SelectedOfferUnitsItem.RejectChanges();
        }

        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Item.OfferUnits.CollectionChanged += OfferUnitsOnCollectionChanged;
                this.PropertyChanged += OnSelectedOfferUnitPropertyChanged;
            });
        }

        private void OnSelectedOfferUnitPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, nameof(SelectedOfferUnitsItem))) return;
            ((DelegateCommand)EditOfferUnitCommand).RaiseCanExecuteChanged();
        }

        private void OfferUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (var removedOfferUnit in Item.OfferUnits.RemovedItems)
            {
                UnitOfWork.GetRepository<OfferUnit>().Delete(removedOfferUnit.Model);
            }
        }


    }

    public class OfferUnitsGroup : BaseUnitsGroup<OfferUnitWrapper>
    {
        public OfferUnitsGroup(IEnumerable<OfferUnitWrapper> units) : base(units)
        {
        }

        public FacilityWrapper Facility
        {
            get { return GetValue<FacilityWrapper>(); }
            set { SetValue(value); }
        }

        public ProductWrapper Product
        {
            get { return GetValue<ProductWrapper>(); }
            set { SetValue(value); }
        }

        public double MarginalIncome
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total => Groups.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

    }
}