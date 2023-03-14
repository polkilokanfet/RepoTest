using System.Windows;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlPlanMaker
    {
        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", 
            typeof(TasksViewModelPlanMaker), 
            typeof(TasksControlPlanMaker), 
            new PropertyMetadata(default(TasksViewModelPlanMaker)));

        public TasksViewModelPlanMaker TasksViewModel
        {
            get => (TasksViewModelPlanMaker) GetValue(TasksViewModelProperty);
            set => SetValue(TasksViewModelProperty, value);
        }

        public TasksControlPlanMaker()
        {
            InitializeComponent();
        }
    }
}
