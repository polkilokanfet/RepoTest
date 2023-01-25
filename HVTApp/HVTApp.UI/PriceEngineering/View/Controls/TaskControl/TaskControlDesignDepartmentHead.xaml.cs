using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlDesignDepartmentHead : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register("TaskViewModel", typeof(TaskViewModelDesignDepartmentHead), typeof(TaskControl), new PropertyMetadata(default(TaskViewModelDesignDepartmentHead)));

        public TaskViewModelDesignDepartmentHead TaskViewModel
        {
            get => (TaskViewModelDesignDepartmentHead) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public TaskControlDesignDepartmentHead()
        {
            InitializeComponent();
        }
    }
}
