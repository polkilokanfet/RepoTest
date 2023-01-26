using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlConstructor : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelConstructor), typeof(ProductTaskControlConstructor), new PropertyMetadata(default(TaskViewModelConstructor)));

        public TaskViewModelConstructor TaskViewModel
        {
            get { return (TaskViewModelConstructor) GetValue(TaskViewModelProperty); }
            set { SetValue(TaskViewModelProperty, value); }
        }
        public ProductTaskControlConstructor()
        {
            InitializeComponent();
        }
    }
}
