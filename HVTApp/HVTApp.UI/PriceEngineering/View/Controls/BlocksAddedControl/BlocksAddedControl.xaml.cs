using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class BlocksAddedControl : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(TaskViewModel), typeof(BlocksAddedControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel ViewModel
        {
            get => (TaskViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public static readonly DependencyProperty ButtonsAreaProperty = DependencyProperty.Register(
            "ButtonsArea", typeof(object), typeof(BlocksAddedControl), new UIPropertyMetadata(default(object)));

        public object ButtonsArea
        {
            get => (object) GetValue(ButtonsAreaProperty);
            set => SetValue(ButtonsAreaProperty, value);
        }

        public BlocksAddedControl()
        {
            InitializeComponent();
        }
    }
}
