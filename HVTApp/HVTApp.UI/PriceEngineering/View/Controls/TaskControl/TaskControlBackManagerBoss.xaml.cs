using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlBackManagerBoss : UserControl
    {
        public static readonly DependencyProperty TaskViewModelBackManagerBossProperty = DependencyProperty.Register(
            "TaskViewModelBackManagerBoss", 
            typeof(TaskViewModelBackManagerBoss), 
            typeof(TaskControlBackManagerBoss), 
            new PropertyMetadata(default(TaskViewModelBackManagerBoss)));

        public TaskViewModelBackManagerBoss TaskViewModelBackManagerBoss
        {
            get { return (TaskViewModelBackManagerBoss) GetValue(TaskViewModelBackManagerBossProperty); }
            set { SetValue(TaskViewModelBackManagerBossProperty, value); }
        }

        public TaskControlBackManagerBoss()
        {
            InitializeComponent();
        }
    }
}
