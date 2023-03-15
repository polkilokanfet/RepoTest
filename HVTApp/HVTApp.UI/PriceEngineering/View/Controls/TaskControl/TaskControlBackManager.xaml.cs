using System.Windows;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlBackManager
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", 
            typeof(TaskViewModelBackManager), 
            typeof(TaskControlBackManager), 
            new PropertyMetadata(default(TaskViewModelBackManager)));

        public TaskViewModelBackManager TaskViewModel
        {
            get => (TaskViewModelBackManager) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskControlBackManager()
        {
            InitializeComponent();
        }
    }
}
