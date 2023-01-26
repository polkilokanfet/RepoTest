using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlConstructor : UserControl
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", typeof(TasksViewModelConstructor), typeof(TasksControlConstructor), new PropertyMetadata(default(TasksViewModelConstructor)));

        public TasksViewModelConstructor TasksViewModel
        {
            get { return (TasksViewModelConstructor) GetValue(TasksViewModelProperty); }
            set { SetValue(TasksViewModelProperty, value); }
        }

        public TasksControlConstructor()
        {
            InitializeComponent();
        }
    }
}
