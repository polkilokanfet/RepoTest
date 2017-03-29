using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls.ChooseControl
{
    /// <summary>
    /// Interaction logic for ChooseControl.xaml
    /// </summary>
    public partial class ChooseControl : UserControl
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

        public static readonly RoutedEvent ItemChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ItemChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(ChooseControl));

        public event RoutedPropertyChangedEventHandler<Color> ItemChanged
        {
            add { AddHandler(ItemChangedEvent, value); }
            remove { RemoveHandler(ItemChangedEvent, value); }
        }
    }
}
