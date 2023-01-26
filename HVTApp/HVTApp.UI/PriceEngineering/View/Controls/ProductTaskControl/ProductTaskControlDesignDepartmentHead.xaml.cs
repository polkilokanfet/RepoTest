using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlDesignDepartmentHead : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelDesignDepartmentHead), typeof(ProductTaskControlDesignDepartmentHead), new PropertyMetadata(default(TaskViewModelDesignDepartmentHead)));

        public TaskViewModelDesignDepartmentHead TaskViewModel
        {
            get { return (TaskViewModelDesignDepartmentHead) GetValue(TaskViewModelProperty); }
            set { SetValue(TaskViewModelProperty, value); }
        }
        public ProductTaskControlDesignDepartmentHead()
        {
            InitializeComponent();
        }
    }
}
