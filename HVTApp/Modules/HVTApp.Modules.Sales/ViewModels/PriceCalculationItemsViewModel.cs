using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PriceCalculationItemsViewModel : IDialogRequestClose
    {
        private object[] _selectedItems;
        public IEnumerable<PriceCalculationItem2Wrapper> Items { get; }

        public object[] SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public List<PriceCalculationItem2Wrapper> SelectedItemWrappers => SelectedItems?.Cast<PriceCalculationItem2Wrapper>().ToList();

        public ICommand SelectCommand { get; }

        public PriceCalculationItemsViewModel(IEnumerable<PriceCalculationItem2Wrapper> items)
        {
            Items = items;
            SelectCommand = new DelegateCommand(
                () => { CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true)); }, 
                () => SelectedItems != null && SelectedItems.Any());
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}