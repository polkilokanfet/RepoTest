using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlInspector : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelInspector), typeof(TaskControlInspector), new PropertyMetadata(default(TaskViewModelInspector)));

        public TaskViewModelInspector TaskViewModel
        {
            get => (TaskViewModelInspector) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskControlInspector()
        {
            InitializeComponent();
        }
    }
}
