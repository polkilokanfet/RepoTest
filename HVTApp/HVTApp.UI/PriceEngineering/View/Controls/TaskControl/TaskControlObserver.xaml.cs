using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlObserver : UserControl
    {
        public static readonly DependencyProperty TaskViewModelHeadProperty = DependencyProperty.Register(
            "TaskViewModelHead", typeof(TaskViewModelObserver), typeof(TaskControlObserver), new PropertyMetadata(default(TaskViewModelObserver)));

        public TaskViewModelObserver TaskViewModelHead
        {
            get { return (TaskViewModelObserver) GetValue(TaskViewModelHeadProperty); }
            set { SetValue(TaskViewModelHeadProperty, value); }
        }

        public TaskControlObserver()
        {
            InitializeComponent();
        }
    }
}
