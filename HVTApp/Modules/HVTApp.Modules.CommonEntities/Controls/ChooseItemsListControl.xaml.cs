using System.Collections;
using System.Windows;
using System.Windows.Input;

namespace HVTApp.UI.Controls
{
    /// <summary>
    /// Interaction logic for ChooseItemsListControl.xaml
    /// </summary>
    public partial class ChooseItemsListControl
    {
        public ChooseItemsListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable), typeof(ChooseItemsListControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Items
        {
            get { return (IEnumerable) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }




        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(object), typeof(ChooseItemsListControl), new PropertyMetadata(default(object)));

        public object SelectedItem
        {
            get { return (object) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register(
            "AddItemCommand", typeof (ICommand), typeof (ChooseItemsListControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItemCommand
        {
            get { return (ICommand) GetValue(AddItemCommandProperty); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register(
            "RemoveItemCommand", typeof (ICommand), typeof (ChooseItemsListControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItemCommand
        {
            get { return (ICommand) GetValue(RemoveItemCommandProperty); }
            set { SetValue(RemoveItemCommandProperty, value); }
        }
    }
}
