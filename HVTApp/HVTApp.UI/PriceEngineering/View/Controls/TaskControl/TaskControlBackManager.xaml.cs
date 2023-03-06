using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.Tce.Second;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlBackManager : UserControl
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


        #region TasksTceItem

        public static readonly DependencyProperty TasksTceItemProperty = DependencyProperty.Register(
            "TasksTceItem", typeof(TasksTceItem), typeof(TaskControlBackManagerBoss), new PropertyMetadata(default(TasksTceItem)));

        public TasksTceItem TasksTceItem
        {
            get { return (TasksTceItem) GetValue(TasksTceItemProperty); }
            set { SetValue(TasksTceItemProperty, value); }
        }

        #endregion

        public TaskControlBackManager()
        {
            InitializeComponent();
        }
    }
}
