using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlHeader : UserControl
    {
        public static readonly DependencyProperty TasksAreaProperty = DependencyProperty.Register(
            "TasksArea", typeof(object), typeof(ProductTaskControlHeader), new UIPropertyMetadata(null));

        public object TasksArea
        {
            get => (object) GetValue(TasksAreaProperty);
            set => SetValue(TasksAreaProperty, value);
        }

        public ProductTaskControlHeader()
        {
            InitializeComponent();
        }
    }
}
