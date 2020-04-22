using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public partial class ComplectTypeWindow : INotifyPropertyChanged
    {
        private string _complectType;

        public string ComplectType
        {
            get { return _complectType; }
            set
            {
                _complectType = value;
                OnPropertyChanged();
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsOk { get; private set; } = false;

        public ICommand OkCommand { get; }

        public ComplectTypeWindow()
        {
            InitializeComponent();

            OkCommand = new DelegateCommand(
                () =>
                {
                    IsOk = true;
                    this.Close();
                }, 
                () => !string.IsNullOrEmpty(ComplectType));

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
