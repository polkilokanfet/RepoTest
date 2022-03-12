using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Task.History
{
    /// <summary>
    /// Interaction logic for HistoryItemListControl.xaml
    /// </summary>
    public partial class HistoryItemListControl : UserControl
    {
        public HistoryItemListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items), typeof(IEnumerable), typeof(HistoryItemListControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Items
        {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

    }
}
