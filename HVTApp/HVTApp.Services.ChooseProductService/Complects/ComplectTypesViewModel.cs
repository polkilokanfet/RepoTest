using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectTypesViewModel
    {
        private Parameter _selectedItem;
        public ObservableCollection<Parameter> Items { get; }

        public Parameter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewTypeCommand { get; }

        public ComplectTypesViewModel(IEnumerable<Parameter> items)
        {
            Items = new ObservableCollection<Parameter>(items);

            SelectCommand = new DelegateCommand(
                () =>
                {
                    IsSelected = true;
                    SelectEvent?.Invoke();
                }, 
                () => SelectedItem != null);

            NewTypeCommand = new DelegateCommand(
                () =>
                {
                    
                });
        }

        public void ShowDialog()
        {
            var window = new ComplectTypesWindow(this);
            window.ShowDialog();
        }

        public event Action SelectEvent;
    }
}