using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlObserver : UserControl
    {

        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", typeof(TasksViewModelObserver), typeof(TasksControlObserver), new PropertyMetadata(default(TasksViewModelObserver)));

        public TasksViewModelObserver TasksViewModel
        {
            get { return (TasksViewModelObserver) GetValue(TasksViewModelProperty); }
            set { SetValue(TasksViewModelProperty, value); }
        }
        public TasksControlObserver()
        {
            InitializeComponent();
        }
    }
}
