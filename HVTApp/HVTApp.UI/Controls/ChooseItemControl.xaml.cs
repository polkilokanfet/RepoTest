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

        #region Item       

        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            nameof(Item), typeof (object), typeof (ChooseItemControl), new PropertyMetadata(default(object)));

        public object Item
        {
            get => (object) GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        #endregion

        #region AddItem       

        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register(
            nameof(AddItemCommand), typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItemCommand
        {
            get => (ICommand) GetValue(AddItemCommandProperty);
            set => SetValue(AddItemCommandProperty, value);
        }

        #endregion

        #region RemoveItem       

        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register(
            nameof(RemoveItemCommand), typeof (ICommand), typeof (ChooseItemControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItemCommand
        {
            get => (ICommand) GetValue(RemoveItemCommandProperty);
            set => SetValue(RemoveItemCommandProperty, value);
        }

        #endregion

        #region ItemChanged       

        public static readonly RoutedEvent ItemChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ItemChanged), RoutingStrategy.Bubble, typeof (RoutedPropertyChangedEventHandler<object>),
            typeof (ChooseItemControl));

        public event RoutedPropertyChangedEventHandler<Color> ItemChanged
        {
            add => AddHandler(ItemChangedEvent, value);
            remove => RemoveHandler(ItemChangedEvent, value);
        }

        #endregion
    }
}
