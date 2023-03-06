using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlBackManager : UserControl
    {
        public static readonly DependencyProperty TaskViewModelBackManagerBossProperty = DependencyProperty.Register(
            "TaskViewModelBackManager", 
            typeof(TaskViewModelBackManager), 
            typeof(TaskControlBackManager), 
            new PropertyMetadata(default(TaskViewModelBackManager)));

        public TaskViewModelBackManager TaskViewModelBackManager
        {
            get => (TaskViewModelBackManager) GetValue(TaskViewModelBackManagerBossProperty);
            set => SetValue(TaskViewModelBackManagerBossProperty, value);
        }

        public TaskControlBackManager()
        {
            InitializeComponent();
        }
    }
}
