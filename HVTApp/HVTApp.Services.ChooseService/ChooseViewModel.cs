using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Services.ChooseService.Annotations;
using Prism.Commands;

namespace HVTApp.Services.ChooseService
{
    public abstract class ChooseViewModel : IChooseViewModel
    {
        private object _selectedItem;

        protected ChooseViewModel()
        {
            ChooseCommand = new DelegateCommand(ChooseCommand_Execute, ChooseCommand_CanExecute);
        }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        protected virtual bool ChooseCommand_CanExecute()
        {
            return SelectedItem != null;
        }

        protected virtual void ChooseCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }



        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        public ICommand ChooseCommand { get; }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
