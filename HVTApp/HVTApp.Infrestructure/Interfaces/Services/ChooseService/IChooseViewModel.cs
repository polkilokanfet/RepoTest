using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseViewModel : IDialogRequestClose, INotifyPropertyChanged
    {
        object SelectedItem { get; set; }
        ICommand ChooseCommand { get; }
    }
}
