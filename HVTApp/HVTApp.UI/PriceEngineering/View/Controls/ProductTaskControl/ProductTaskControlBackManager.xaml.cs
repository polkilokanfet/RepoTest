using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlBackManager : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", 
            typeof(TaskViewModelBackManager), 
            typeof(ProductTaskControlBackManager), 
            new PropertyMetadata(default(TaskViewModelBackManager)));

        public TaskViewModelBackManager TaskViewModel
        {
            get => (TaskViewModelBackManager) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }
        public ProductTaskControlBackManager()
        {
            InitializeComponent();
        }
    }
}
