using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class BlockAddedControl : UserControl
    {
        public static readonly DependencyProperty ProductBlockAddedProperty = DependencyProperty.Register(
            "ProductBlockAdded", typeof(TaskProductBlockAddedWrapperConstructor), typeof(BlockAddedControl), new PropertyMetadata(default(TaskProductBlockAddedWrapper)));

        public TaskProductBlockAddedWrapperConstructor ProductBlockAdded
        {
            get => (TaskProductBlockAddedWrapperConstructor) GetValue(ProductBlockAddedProperty);
            set => SetValue(ProductBlockAddedProperty, value);
        }
        public BlockAddedControl()
        {
            InitializeComponent();
        }
    }
}
