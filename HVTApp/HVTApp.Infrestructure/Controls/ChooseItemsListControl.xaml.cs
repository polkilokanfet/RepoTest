using System.Collections;
using System.Windows;
using System.Windows.Input;

namespace HVTApp.Infrastructure.Controls
{
    public partial class ChooseItemsListControl
    {
        public ChooseItemsListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items), typeof(IEnumerable), typeof(ChooseItemsListControl), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Items
        {
            get => (IEnumerable) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }




        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem), typeof(object), typeof(ChooseItemsListControl), new PropertyMetadata(default(object)));

        public object SelectedItem
        {
            get => (object) GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }



        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register(
            nameof(AddItemCommand), typeof (ICommand), typeof (ChooseItemsListControl), new PropertyMetadata(default(ICommand)));

        public ICommand AddItemCommand
        {
            get => (ICommand) GetValue(AddItemCommandProperty);
            set => SetValue(AddItemCommandProperty, value);
        }



        public static readonly DependencyProperty RemoveItemCommandProperty = DependencyProperty.Register(
            nameof(RemoveItemCommand), typeof (ICommand), typeof (ChooseItemsListControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveItemCommand
        {
            get => (ICommand) GetValue(RemoveItemCommandProperty);
            set => SetValue(RemoveItemCommandProperty, value);
        }
    }
}
