using System.Windows;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlBackManagerBoss
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", 
            typeof(TasksViewModelBackManagerBoss), 
            typeof(TasksControlBackManagerBoss), 
            new PropertyMetadata(default(TasksViewModelBackManagerBoss)));

        public TasksViewModelBackManagerBoss TasksViewModel
        {
            get => (TasksViewModelBackManagerBoss) GetValue(TasksViewModelProperty);
            set => SetValue(TasksViewModelProperty, value);
        }
        public TasksControlBackManagerBoss()
        {
            InitializeComponent();
        }
    }
}
