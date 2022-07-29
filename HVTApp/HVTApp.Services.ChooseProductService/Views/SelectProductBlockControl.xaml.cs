using System.Windows;
using System.Windows.Controls;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectProductBlockControl : UserControl
    {
        public SelectProductBlockControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProductBlockSelectorProperty = DependencyProperty.Register(
            "ProductBlockSelector", typeof(ProductBlockSelector), typeof(SelectProductBlockControl), new PropertyMetadata(default(ProductBlockSelector)));

        public ProductBlockSelector ProductBlockSelector
        {
            get => (ProductBlockSelector) GetValue(ProductBlockSelectorProperty);
            set => SetValue(ProductBlockSelectorProperty, value);
        }
    }
}
