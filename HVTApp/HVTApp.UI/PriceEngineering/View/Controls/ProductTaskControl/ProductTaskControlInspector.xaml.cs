using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlInspector : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelInspector), typeof(ProductTaskControlInspector), new PropertyMetadata(default(TaskViewModelInspector)));

        public TaskViewModelInspector TaskViewModel
        {
            get => (TaskViewModelInspector) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public ProductTaskControlInspector()
        {
            InitializeComponent();
        }
    }
}
