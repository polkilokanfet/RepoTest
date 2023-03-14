using System.Windows;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlBackManager
    {
        public static readonly DependencyProperty TaskViewModelBackManagerProperty = DependencyProperty.Register(
            "TaskViewModelBackManager", 
            typeof(TaskViewModelBackManager), 
            typeof(TaskControlBackManager), 
            new PropertyMetadata(default(TaskViewModelBackManager)));

        public TaskViewModelBackManager TaskViewModelBackManager
        {
            get => (TaskViewModelBackManager) GetValue(TaskViewModelBackManagerProperty);
            set => SetValue(TaskViewModelBackManagerProperty, value);
        }

        public TaskControlBackManager()
        {
            InitializeComponent();
        }
    }
}
