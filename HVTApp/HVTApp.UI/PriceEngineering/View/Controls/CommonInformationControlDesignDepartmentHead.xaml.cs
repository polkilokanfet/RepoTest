using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class CommonInformationControlDesignDepartmentHead : UserControl
    {
        public static readonly DependencyProperty TasksViewModelDesignDepartmentHeadProperty = DependencyProperty.Register(
            "TasksViewModelDesignDepartmentHead", typeof(TasksViewModelDesignDepartmentHead), typeof(CommonInformationControlDesignDepartmentHead), new PropertyMetadata(default(TasksViewModelDesignDepartmentHead)));

        public TasksViewModelDesignDepartmentHead TasksViewModelDesignDepartmentHead
        {
            get { return (TasksViewModelDesignDepartmentHead) GetValue(TasksViewModelDesignDepartmentHeadProperty); }
            set { SetValue(TasksViewModelDesignDepartmentHeadProperty, value); }
        }
        public CommonInformationControlDesignDepartmentHead()
        {
            InitializeComponent();
        }
    }
}
