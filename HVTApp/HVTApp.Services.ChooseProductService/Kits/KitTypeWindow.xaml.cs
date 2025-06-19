using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Kits
{
    public partial class KitTypeWindow : INotifyPropertyChanged
    {
        public ParameterWrapper ParameterComplectType { get; }

        public bool IsOk { get; private set; } = false;

        public ICommand OkCommand { get; }

        public KitTypeWindow(ParameterGroup parameterGroup)
        {
            ParameterComplectType = new ParameterWrapper(new Parameter {ParameterGroup = parameterGroup});

            OkCommand = new DelegateCommand(
                () =>
                {
                    IsOk = true;
                    this.Close();
                }, 
                () => ParameterComplectType.IsValid);

            ParameterComplectType.PropertyChanged += (sender, args) => ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();

            InitializeComponent();
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
