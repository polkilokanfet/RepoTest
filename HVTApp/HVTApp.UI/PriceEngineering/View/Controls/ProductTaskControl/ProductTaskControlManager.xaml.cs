using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlManager : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register("TaskViewModel", typeof(object), typeof(ProductTaskControlManager), new PropertyMetadata(default(TaskViewModelManager)));

        public TaskViewModelManager TaskViewModel
        {
            get => (TaskViewModelManager) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public ProductTaskControlManager()
        {
            InitializeComponent();
        }
    }
}
