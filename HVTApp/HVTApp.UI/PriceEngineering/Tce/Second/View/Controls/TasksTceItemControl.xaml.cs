using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View
{
    public partial class TasksTceItemControl : UserControl
    {
        public static readonly DependencyProperty TasksTceItemProperty = DependencyProperty.Register(
            "TasksTceItem", typeof(TasksTceItem), typeof(TasksTceItemControl), new PropertyMetadata(default(TasksTceItem)));

        public TasksTceItem TasksTceItem
        {
            get => (TasksTceItem) GetValue(TasksTceItemProperty);
            set => SetValue(TasksTceItemProperty, value);
        }

        public TasksTceItemControl()
        {
            InitializeComponent();
        }
    }
}
