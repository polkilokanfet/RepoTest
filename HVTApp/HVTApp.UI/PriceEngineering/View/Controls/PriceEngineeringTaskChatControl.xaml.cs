using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskChatControl : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModel), typeof(PriceEngineeringTaskChatControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public PriceEngineeringTaskChatControl()
        {
            InitializeComponent();
        }
    }
}
