using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlManager : UserControl
    {
        public static readonly DependencyProperty TasksViewModelManagerProperty = DependencyProperty.Register("TasksViewModelManager", typeof(TasksViewModelManager), typeof(TasksControlManager), new PropertyMetadata(default(TasksViewModelManager)));

        public TasksViewModelManager TasksViewModelManager
        {
            get => (TasksViewModelManager) GetValue(TasksViewModelManagerProperty);
            set => SetValue(TasksViewModelManagerProperty, value);
        }

        public TasksControlManager()
        {
            InitializeComponent();
        }
    }
}
