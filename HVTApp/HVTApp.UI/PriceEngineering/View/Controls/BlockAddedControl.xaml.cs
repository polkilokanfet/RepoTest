using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class BlockAddedControl : UserControl
    {
        public static readonly DependencyProperty ProductBlockAddedProperty = DependencyProperty.Register(
            "ProductBlockAdded", typeof(PriceEngineeringTaskProductBlockAddedWrapper1), typeof(BlockAddedControl), new PropertyMetadata(default(PriceEngineeringTaskProductBlockAddedWrapper1)));

        public PriceEngineeringTaskProductBlockAddedWrapper1 ProductBlockAdded
        {
            get => (PriceEngineeringTaskProductBlockAddedWrapper1) GetValue(ProductBlockAddedProperty);
            set => SetValue(ProductBlockAddedProperty, value);
        }
        public BlockAddedControl()
        {
            InitializeComponent();
        }
    }
}
