using System.Windows;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlManager
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", 
            typeof(TasksViewModelManager), 
            typeof(TasksControlManager), 
            new PropertyMetadata(default(TasksViewModelManager)));

        public TasksViewModelManager TasksViewModel
        {
            get => (TasksViewModelManager) GetValue(TasksViewModelProperty);
            set => SetValue(TasksViewModelProperty, value);
        }

        public TasksControlManager()
        {
            InitializeComponent();
        }
    }
}
