using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering
{
    public partial class WorkloadOnEmployeesControl : UserControl
    {
        public WorkloadOnEmployeesControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(WorkloadOnEmployeesViewModel), typeof(WorkloadOnEmployeesControl), new PropertyMetadata(default(WorkloadOnEmployeesViewModel)));

        public WorkloadOnEmployeesViewModel ViewModel
        {
            get => (WorkloadOnEmployeesViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
