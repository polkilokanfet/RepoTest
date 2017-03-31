using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyUserControls
{
    /// <summary>
    /// Interaction logic for ChooseItemToListControl.xaml
    /// </summary>
    public partial class ChooseItemToListControl
    {
        public ChooseItemToListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(ObservableCollection<object>), typeof(ChooseItemToListControl), new PropertyMetadata(default(ObservableCollection<object>)));

        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty AddItemProperty = DependencyProperty.Register(
            "AddItem", typeof (ICommand), typeof (ChooseItemToListControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItem
        {
            get { return (ICommand) GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); }
        }

        public static readonly DependencyProperty RemoveItemProperty = DependencyProperty.Register(
            "RemoveItem", typeof (ICommand), typeof (ChooseItemToListControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItem
        {
            get { return (ICommand) GetValue(RemoveItemProperty); }
            set { SetValue(RemoveItemProperty, value); }
        }
    }
}
