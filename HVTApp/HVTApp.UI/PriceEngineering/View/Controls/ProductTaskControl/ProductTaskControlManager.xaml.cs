using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlManager : UserControl
    {
        public static readonly DependencyProperty TaskViewModelManagerProperty = DependencyProperty.Register("TaskViewModelManager", typeof(object), typeof(ProductTaskControlManager), new PropertyMetadata(default(TaskViewModelManager)));

        public TaskViewModelManager TaskViewModelManager
        {
            get => (TaskViewModelManager) GetValue(TaskViewModelManagerProperty);
            set => SetValue(TaskViewModelManagerProperty, value);
        }

        public ProductTaskControlManager()
        {
            InitializeComponent();
        }
    }
}
