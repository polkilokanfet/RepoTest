using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlBackManager : UserControl
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", typeof(TasksViewModelManagerBack), typeof(TasksControlBackManager), new PropertyMetadata(default(TasksViewModelManagerBack)));

        public TasksViewModelManagerBack TasksViewModel
        {
            get => (TasksViewModelManagerBack) GetValue(TasksViewModelProperty);
            set => SetValue(TasksViewModelProperty, value);
        }

        public TasksControlBackManager()
        {
            InitializeComponent();
        }
    }
}
