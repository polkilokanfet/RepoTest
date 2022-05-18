using System.Windows;
using System.Windows.Controls;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductBlockControl : UserControl
    {
        public static readonly DependencyProperty ProductBlockProperty = DependencyProperty.Register(
            "ProductBlock", typeof(ProductBlock), typeof(ProductBlockControl), new PropertyMetadata(default(ProductBlock)));

        public ProductBlock ProductBlock
        {
            get { return (ProductBlock) GetValue(ProductBlockProperty); }
            set { SetValue(ProductBlockProperty, value); }
        }

        public ProductBlockControl()
        {
            InitializeComponent();
        }
    }
}
