using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationItemsViewModel : IDialogRequestClose
    {
        private object[] _selectedItems;
        public IEnumerable<PriceCalculationItem2Wrapper> Items { get; }

        public object[] SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                SelectCommand.RaiseCanExecuteChanged();
            }
        }

        public List<PriceCalculationItem2Wrapper> SelectedItemWrappers => SelectedItems?.Cast<PriceCalculationItem2Wrapper>().ToList();

        public DelegateLogCommand SelectCommand { get; }

        public PriceCalculationItemsViewModel(IEnumerable<PriceCalculationItem2Wrapper> items)
        {
            Items = items;
            SelectCommand = new DelegateLogCommand(
                () => { CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true)); }, 
                () => SelectedItems != null && SelectedItems.Any());
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}