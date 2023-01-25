using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTasksControlManager : UserControl
    {
        public static readonly DependencyProperty TasksViewModelManagerProperty = DependencyProperty.Register("TasksViewModelManager", typeof(TasksViewModelManager), typeof(PriceEngineeringTasksControlManager), new PropertyMetadata(default(TasksViewModelManager)));

        public TasksViewModelManager TasksViewModelManager
        {
            get => (TasksViewModelManager) GetValue(TasksViewModelManagerProperty);
            set => SetValue(TasksViewModelManagerProperty, value);
        }

        public PriceEngineeringTasksControlManager()
        {
            InitializeComponent();
        }
    }
}
