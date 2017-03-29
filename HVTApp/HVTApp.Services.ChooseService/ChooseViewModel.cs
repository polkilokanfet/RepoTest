using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Services.ChooseService.Annotations;
using Prism.Commands;

namespace HVTApp.Services.ChooseService
{
    public class ChooseViewModel<TChoosenItem> : IChooseViewModel<TChoosenItem>
    {
        private TChoosenItem _selectedItem;
        private string _filterString;

        public ChooseViewModel(IEnumerable<TChoosenItem> items)
        {
            //Items = new ObservableCollection<TChoosenItem>(items);
            Items = CollectionViewSource.GetDefaultView(items);
            Items.Filter = ItemsFilter;

            ChooseCommand = new DelegateCommand(ChooseCommand_Execute, ChooseCommand_CanExecute);
        }

        public ICollectionView Items { get; }
        public ICommand ChooseCommand { get; }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                Items.Refresh();
            }
        }

        public TChoosenItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                ((DelegateCommand)ChooseCommand).RaiseCanExecuteChanged();
            }
        }

        private bool ItemsFilter(object o)
        {
            if (string.IsNullOrEmpty(FilterString))
                return true;
            string filterString = FilterString.ToLower().Trim();
            return o.ToString().ToLower().Contains(filterString);
        }


        protected virtual bool ChooseCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected virtual void ChooseCommand_Execute()
        {
            ChooseRequested?.Invoke(this, new ChooseDialogEventArgs<TChoosenItem>(SelectedItem));
        }

        public event EventHandler<ChooseDialogEventArgs<TChoosenItem>> ChooseRequested;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
