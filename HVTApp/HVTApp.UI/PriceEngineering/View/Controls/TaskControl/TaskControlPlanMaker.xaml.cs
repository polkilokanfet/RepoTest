using System.Windows;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlPlanMaker
    {

        public static readonly DependencyProperty TaskViewModelPlanMakerProperty = DependencyProperty.Register(
            "TaskViewModelPlanMaker", 
            typeof(TaskViewModelPlanMaker), 
            typeof(TaskControlPlanMaker), 
            new PropertyMetadata(default(TaskViewModelPlanMaker)));

        public TaskViewModelPlanMaker TaskViewModelPlanMaker
        {
            get => (TaskViewModelPlanMaker) GetValue(TaskViewModelPlanMakerProperty);
            set => SetValue(TaskViewModelPlanMakerProperty, value);
        }

        public TaskControlPlanMaker()
        {
            InitializeComponent();
        }
    }
}
