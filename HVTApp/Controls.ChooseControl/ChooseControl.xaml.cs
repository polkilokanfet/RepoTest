using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyControls
{
    /// <summary>
    /// Interaction logic for ChooseControl.xaml
    /// </summary>
    public partial class ChooseControl
    {
        public ChooseControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            "Item", typeof(object), typeof(ChooseControl), new PropertyMetadata(default(object)));

        public object Item
        {
            get { return (object) GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty AddItemProperty = DependencyProperty.Register(
            "AddItem", typeof (ICommand), typeof (ChooseControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItem
        {
            get { return (ICommand) GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); }
        }

        public static readonly DependencyProperty RemoveItemProperty = DependencyProperty.Register(
            "RemoveItem", typeof (ICommand), typeof (ChooseControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItem
        {
            get { return (ICommand) GetValue(RemoveItemProperty); }
            set { SetValue(RemoveItemProperty, value); }
        }

        public static readonly RoutedEvent ItemChangedEvent = EventManager.RegisterRoutedEvent(
            "ItemChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(ChooseControl));

        public event RoutedPropertyChangedEventHandler<Color> ItemChanged
        {
            add { AddHandler(ItemChangedEvent, value); }
            remove { RemoveHandler(ItemChangedEvent, value); }
        }
    }
}
