using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsPriceCalculationGroupsViewModel : IDialogRequestClose
    {
        private object[] _selectedItems;
        public IEnumerable<SalesUnitsPriceCalculationGroupSimple> Groups { get; }

        public object[] SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public List<SalesUnitsPriceCalculationGroupSimple> SelectedGroups => SelectedItems?.Cast<SalesUnitsPriceCalculationGroupSimple>().ToList();

        public ICommand SelectCommand { get; }

        public SalesUnitsPriceCalculationGroupsViewModel(IEnumerable<SalesUnitsPriceCalculationGroupSimple> groups)
        {
            Groups = groups;
            SelectCommand = new DelegateCommand(
                () =>
                {
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => SelectedItems != null && SelectedItems.Any());
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}