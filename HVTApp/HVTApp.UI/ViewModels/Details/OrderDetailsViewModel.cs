using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OrderDetailsViewModel
    {
        private SalesUnitsWrappersGroup _selectedPotentialWrappersGroup;
        public ObservableCollection<SalesUnitsWrappersGroup> RealGroups { get; } = new ObservableCollection<SalesUnitsWrappersGroup>();
        public ObservableCollection<SalesUnitsWrappersGroup> PotentialGroups { get; } = new ObservableCollection<SalesUnitsWrappersGroup>();

        public SalesUnitsWrappersGroup SelectedPotentialWrappersGroup
        {
            get { return _selectedPotentialWrappersGroup; }
            set
            {
                _selectedPotentialWrappersGroup = value;
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; private set; }

        protected override async Task AfterLoading()
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);

            var salesUnits = await UnitOfWork.Repository<SalesUnit>().GetAllAsync();

            var realUnits = salesUnits.Where(x => x.Order?.Id == Item.Id).ToList();
            var potentialUnits = salesUnits.Where(x => x.Order == null && x.SignalToStartProduction != null).ToList();

            var realGroups = realUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsWrappersGroup(x.ToList())).OrderBy(x => x.EndProductionDateCalculated);
            RealGroups.Clear();
            RealGroups.AddRange(realGroups);

            var potentialGroups = potentialUnits.GroupBy(x => x, new SalesUnitsGroupsComparer()).Select(x => new SalesUnitsWrappersGroup(x.ToList())).OrderBy(x => x.EndProductionDateCalculated);
            PotentialGroups.Clear();
            PotentialGroups.AddRange(potentialGroups);

        }

        private void AddCommand_Execute()
        {
            SelectedPotentialWrappersGroup.Order = Item;

            //SelectedPotentialGroup.Units.ForEach(x => Item.SalesUnits.Add(new SalesUnitWrapper(x)));

            RealGroups.Add(SelectedPotentialWrappersGroup);
            if (PotentialGroups.Contains(SelectedPotentialWrappersGroup))
            {
                PotentialGroups.Remove(SelectedPotentialWrappersGroup);
            }
            else
            {
                SalesUnitsWrappersGroup remove = null;
                foreach (var gr in PotentialGroups)
                {
                    if (gr.Groups.Contains(SelectedPotentialWrappersGroup))
                    {
                        gr.Groups.Remove(SelectedPotentialWrappersGroup);
                        if(!gr.Groups.Any()) remove = gr;
                        break;
                    }
                }
                if (remove != null) PotentialGroups.Remove(remove);
            }
        }
    }
}