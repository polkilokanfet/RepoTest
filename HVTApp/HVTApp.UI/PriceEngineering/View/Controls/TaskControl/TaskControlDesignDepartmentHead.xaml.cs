using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlDesignDepartmentHead : UserControl
    {
        public static readonly DependencyProperty TaskViewModelHeadProperty = DependencyProperty.Register(
            "TaskViewModelHead", typeof(TaskViewModelDesignDepartmentHead), typeof(TaskControlDesignDepartmentHead), new PropertyMetadata(default(TaskViewModelDesignDepartmentHead)));

        public TaskViewModelDesignDepartmentHead TaskViewModelHead
        {
            get { return (TaskViewModelDesignDepartmentHead) GetValue(TaskViewModelHeadProperty); }
            set { SetValue(TaskViewModelHeadProperty, value); }
        }

        public TaskControlDesignDepartmentHead()
        {
            InitializeComponent();
        }
    }
}
