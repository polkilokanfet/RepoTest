using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlConstructor : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register("TaskViewModel", typeof(TaskViewModelConstructor), typeof(TaskControl), new PropertyMetadata(default(TaskViewModelConstructor)));

        public TaskViewModelConstructor TaskViewModel
        {
            get => (TaskViewModelConstructor) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskControlConstructor()
        {
            InitializeComponent();
        }
    }
}
