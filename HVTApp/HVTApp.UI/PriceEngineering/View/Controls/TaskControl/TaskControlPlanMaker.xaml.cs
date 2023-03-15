using System.Windows;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlPlanMaker
    {

        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", 
            typeof(TaskViewModelPlanMaker), 
            typeof(TaskControlPlanMaker), 
            new PropertyMetadata(default(TaskViewModelPlanMaker)));

        public TaskViewModelPlanMaker TaskViewModel
        {
            get => (TaskViewModelPlanMaker) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskControlPlanMaker()
        {
            InitializeComponent();
        }
    }
}
