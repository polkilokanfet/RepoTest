using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyUserControls
{
    /// <summary>
    /// Interaction logic for ChooseItemControl.xaml
    /// </summary>
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

        public static readonly DependencyProperty AddItemProperty = DependencyProperty.Register(
            "AddItem", typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItem
        {
            get { return (ICommand) GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); }
        }

        public static readonly DependencyProperty RemoveItemProperty = DependencyProperty.Register(
            "RemoveItem", typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItem
        {
            get { return (ICommand) GetValue(RemoveItemProperty); }
            set { SetValue(RemoveItemProperty, value); }
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
