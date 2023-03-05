using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlConstructor : UserControl
    {
        public static readonly DependencyProperty TaskViewModelConstructorProperty = DependencyProperty.Register(
            "TaskViewModelConstructor", typeof(TaskViewModelConstructor), typeof(TaskControlConstructor), new PropertyMetadata(default(TaskViewModelConstructor)));

        public TaskViewModelConstructor TaskViewModelConstructor
        {
            get { return (TaskViewModelConstructor) GetValue(TaskViewModelConstructorProperty); }
            set { SetValue(TaskViewModelConstructorProperty, value); }
        }

        public TaskControlConstructor()
        {
            InitializeComponent();
        }
    }
}
