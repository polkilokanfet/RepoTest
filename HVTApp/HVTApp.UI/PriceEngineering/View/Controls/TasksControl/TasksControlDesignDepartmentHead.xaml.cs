using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TasksControlDesignDepartmentHead : UserControl
    {

        public static readonly DependencyProperty TasksViewModelProperty = DependencyProperty.Register(
            "TasksViewModel", typeof(TasksViewModelDesignDepartmentHead), typeof(TasksControlDesignDepartmentHead), new PropertyMetadata(default(TasksViewModelDesignDepartmentHead)));

        public TasksViewModelDesignDepartmentHead TasksViewModel
        {
            get { return (TasksViewModelDesignDepartmentHead) GetValue(TasksViewModelProperty); }
            set { SetValue(TasksViewModelProperty, value); }
        }
        public TasksControlDesignDepartmentHead()
        {
            InitializeComponent();
        }
    }
}
