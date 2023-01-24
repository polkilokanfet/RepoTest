using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class BlocksAddedControl : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModel), typeof(BlocksAddedControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel)GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
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
