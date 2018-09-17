using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OrderDetailsViewModel
    {
        private ProductUnitsGroup _selectedPotentialGroup;
        public ObservableCollection<ProductUnitsGroup> RealGroups { get; } = new ObservableCollection<ProductUnitsGroup>();
        public ObservableCollection<ProductUnitsGroup> PotentialGroups { get; } = new ObservableCollection<ProductUnitsGroup>();

        public ProductUnitsGroup SelectedPotentialGroup
        {
            get { return _selectedPotentialGroup; }
            set
            {
                _selectedPotentialGroup = value;
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; private set; }

        protected override async Task AfterLoading()
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);

            var salesUnits = await UnitOfWork.Repository<SalesUnit>().GetAllAsync();

            var realUnits = salesUnits.Where(x => x.Order?.Id == Item.Id);
            var potentialUnits = salesUnits.Where(x => x.Order == null && x.SignalToStartProduction != null);

            RealGroups.Clear();
            RealGroups.AddRange(ProductUnitsGroup.Grouping(realUnits));

            PotentialGroups.Clear();
            PotentialGroups.AddRange(ProductUnitsGroup.Grouping(potentialUnits));
        }

        private void AddCommand_Execute()
        {
            SelectedPotentialGroup.SetOrder(Item.Model);
            SelectedPotentialGroup.Units.ForEach(x => Item.SalesUnits.Add(new SalesUnitWrapper(x)));
            RealGroups.Add(SelectedPotentialGroup);
            if (PotentialGroups.Contains(SelectedPotentialGroup))
            {
                PotentialGroups.Remove(SelectedPotentialGroup);
            }
            else
            {
                ProductUnitsGroup remove = null;
                foreach (var gr in PotentialGroups)
                {
                    if (gr.Groups.Contains(SelectedPotentialGroup))
                    {
                        gr.Groups.Remove(SelectedPotentialGroup);
                        if(!gr.Groups.Any()) remove = gr;
                        break;
                    }
                }
                if (remove != null) PotentialGroups.Remove(remove);
            }
        }
    }
}