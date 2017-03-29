using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Services.ChooseService.Annotations;
using Prism.Commands;

namespace HVTApp.Services.ChooseService
{
    public class ChooseViewModel<TChoosenItem> : IChooseViewModel<TChoosenItem>
    {
        private TChoosenItem _selectedItem;

        public ChooseViewModel(IEnumerable<TChoosenItem> items)
        {
            Items = new ObservableCollection<TChoosenItem>(items);

            ChooseCommand = new DelegateCommand(ChooseCommand_Execute, ChooseCommand_CanExecute);
        }

        public IEnumerable<TChoosenItem> Items { get; }

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

        protected virtual bool ChooseCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected virtual void ChooseCommand_Execute()
        {
            ChooseRequested?.Invoke(this, new ChooseDialogEventArgs<TChoosenItem>(SelectedItem));
        }

        public event EventHandler<ChooseDialogEventArgs<TChoosenItem>> ChooseRequested;


        public ICommand ChooseCommand { get; }
        public string Filter { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
