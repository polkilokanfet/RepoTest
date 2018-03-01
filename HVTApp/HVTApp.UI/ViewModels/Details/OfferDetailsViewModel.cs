using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Services.MessageService;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        private OfferUnitsGroup _selectedOfferUnitsGroup;
        public ObservableCollection<OfferUnitsGroup> OfferUnitsGroups { get; } = new ObservableCollection<OfferUnitsGroup>();

        public OfferUnitsGroup SelectedOfferUnitsGroup
        {
            get { return _selectedOfferUnitsGroup; }
            set
            {
                if (Equals(_selectedOfferUnitsGroup, value)) return;
                _selectedOfferUnitsGroup = value;
                OnPropertyChanged();
                ((DelegateCommand)EditOfferUnitsGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)BlowUpOfferUnitsGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveOfferUnitsGroupCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddOfferUnitsGroupCommand { get; private set; }
        public ICommand EditOfferUnitsGroupCommand { get; private set; }
        public ICommand RemoveOfferUnitsGroupCommand { get; private set; }

        public ICommand RefreshOfferUnitsGroupsCommand { get; private set; }
        public ICommand BlowUpOfferUnitsGroupCommand { get; private set; }


        protected override void InitCommands()
        {
            AddOfferUnitsGroupCommand = new DelegateCommand(AddOfferUnitsGroupCommand_Execute);
            EditOfferUnitsGroupCommand = new DelegateCommand(EditOfferUnitsGroupCommand_Execute, EditOfferUnitsGroupCommand_CanExecute);
            RemoveOfferUnitsGroupCommand = new DelegateCommand(RemoveOfferUnitsGroupCommand_Execute, RemoveOfferUnitsGroupCommand_CanExecute);

            BlowUpOfferUnitsGroupCommand = new DelegateCommand(BlowUpOfferUnitsGroup_Execute, BlowUpOfferUnitsGroup_CanExecute);
            RefreshOfferUnitsGroupsCommand = new DelegateCommand(RefreshOfferUnitsGroupsCommand_Execute);
        }

        private bool RemoveOfferUnitsGroupCommand_CanExecute()
        {
            return SelectedOfferUnitsGroup != null;
        }

        private void RemoveOfferUnitsGroupCommand_Execute()
        {
            var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Предупреждение", "Вы действительно хотите удалить?");
            if (result != MessageDialogResult.Yes) return;
            UnitOfWork.GetRepository<OfferUnit>().DeleteRange(SelectedOfferUnitsGroup.Units.Select(x => x.Model));
            SelectedOfferUnitsGroup.Units.ForEach(x => Item.OfferUnits.Remove(x));
            OfferUnitsGroups.Remove(SelectedOfferUnitsGroup);
        }


        private void RefreshOfferUnitsGroupsCommand_Execute()
        {
            GroupingOfferUnits();
        }


        private bool BlowUpOfferUnitsGroup_CanExecute()
        {
            return SelectedOfferUnitsGroup != null;
        }

        private void BlowUpOfferUnitsGroup_Execute()
        {
            var ind = OfferUnitsGroups.IndexOf(SelectedOfferUnitsGroup);
            OfferUnitsGroups.Remove(SelectedOfferUnitsGroup);
            foreach (var offerUnitWrapper in SelectedOfferUnitsGroup.Units)
            {
                OfferUnitsGroups.Insert(ind, new OfferUnitsGroup(new []{offerUnitWrapper}));
            }
            SelectedOfferUnitsGroup = OfferUnitsGroups[ind];
        }


        private void AddOfferUnitsGroupCommand_Execute()
        {
            var offerUnitTempl = SelectedOfferUnitsGroup ?? OfferUnitsGroups.FirstOrDefault();
            var offerUnitsGroup = new OfferUnitsGroup(new []{ new OfferUnitWrapper(new OfferUnit()) } );
            if (offerUnitTempl != null)
            {
                offerUnitsGroup.Product = offerUnitTempl.Product;
                offerUnitsGroup.Cost = offerUnitTempl.Cost;
                offerUnitsGroup.Facility = offerUnitTempl.Facility;
                offerUnitsGroup.ProductionTerm = offerUnitTempl.ProductionTerm;
                offerUnitsGroup.PaymentConditionSet = offerUnitTempl.PaymentConditionSet;
            }

            var viewModel = new OfferUnitsGroupDetailsViewModel(offerUnitsGroup, Container, UnitOfWork);
            var flag = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (flag.HasValue && flag.Value)
            {
                offerUnitsGroup.Units.ForEach(Item.OfferUnits.Add);
                OfferUnitsGroups.Add(offerUnitsGroup);
                SelectedOfferUnitsGroup = offerUnitsGroup;
            }
        }


        private bool EditOfferUnitsGroupCommand_CanExecute()
        {
            return SelectedOfferUnitsGroup != null;
        }

        private void EditOfferUnitsGroupCommand_Execute()
        {
            var viewModel = new OfferUnitsGroupDetailsViewModel(SelectedOfferUnitsGroup, Container, UnitOfWork);
            var flag = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!flag.HasValue || flag.Value)
            {
                SelectedOfferUnitsGroup.Units.ForEach(x => x.RejectChanges());
            }
        }

        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                GroupingOfferUnits();
            });
        }

        private void GroupingOfferUnits()
        {
            var groups = Item.OfferUnits.ToUnitGroups();
            OfferUnitsGroups.Clear();
            OfferUnitsGroups.AddRange(groups);
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

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return GetValue<PaymentConditionSetWrapper>(); }
            set { SetValue(value); }
        }

        public double MarginalIncome
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int ProductionTerm
        {
            get { return GetValue<int>(); }
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

        public double Total => Units.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

    }
}