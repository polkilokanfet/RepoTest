using System.Windows;
using System.Windows.Controls;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectProductControl : UserControl
    {
        public SelectProductControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ProductSelectorProperty = DependencyProperty.Register(
            "ProductSelector", typeof(ProductSelector), typeof(SelectProductControl), new PropertyMetadata(default(ProductSelector)));

        public ProductSelector ProductSelector
        {
            get => (ProductSelector) GetValue(ProductSelectorProperty);
            set => SetValue(ProductSelectorProperty, value);
        }
    }
}
