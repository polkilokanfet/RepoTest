using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Model.Wrapper;

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
            nameof(Items), typeof(IEnumerable<IHistoryElementWrapper>), typeof(HistoryItemListControl), new PropertyMetadata(default(IEnumerable<IHistoryElementWrapper>)));

        public IEnumerable<IHistoryElementWrapper> Items
        {
            get => (IEnumerable<IHistoryElementWrapper>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

    }
}
