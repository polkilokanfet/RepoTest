using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlManager1 : UserControl
    {
        public static readonly DependencyProperty TaskViewModelManagerProperty = DependencyProperty.Register(
            "TaskViewModelManager", typeof(TaskViewModelManager), typeof(TaskControlManager1), new PropertyMetadata(default(TaskViewModelManager)));

        public TaskViewModelManager TaskViewModelManager
        {
            get { return (TaskViewModelManager) GetValue(TaskViewModelManagerProperty); }
            set { SetValue(TaskViewModelManagerProperty, value); }
        }

        public TaskControlManager1()
        {
            InitializeComponent();
        }

        
    }
}
