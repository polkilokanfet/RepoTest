using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HVTApp.UI.Controls
{
    public partial class ChooseItemControl
    {
        public ChooseItemControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            "Item", typeof (object), typeof (ChooseItemControl), new PropertyMetadata(default(object)));

        public object Item
        {
            get { return (object) GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register(
            "AddItemCommand", typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItemCommand
        {
            get { return (ICommand) GetValue(AddItemCommandProperty); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register(
            "RemoveItemCommand", typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItemCommand
        {
            get { return (ICommand) GetValue(RemoveItemCommandProperty); }
            set { SetValue(RemoveItemCommandProperty, value); }
        }

        public static readonly RoutedEvent ItemChangedEvent = EventManager.RegisterRoutedEvent(
            "ItemChanged", RoutingStrategy.Bubble, typeof (RoutedPropertyChangedEventHandler<object>),
            typeof (ChooseItemControl));

        public event RoutedPropertyChangedEventHandler<Color> ItemChanged
        {
            add { AddHandler(ItemChangedEvent, value); }
            remove { RemoveHandler(ItemChangedEvent, value); }
        }

    }
}
