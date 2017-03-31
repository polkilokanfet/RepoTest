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
            "Items", typeof (List<object>), typeof (ChooseItemToListControl), new PropertyMetadata(default(List<object>)));

        public List<object> Items
        {
            get { return (List<object>) GetValue(ItemsProperty); }
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
