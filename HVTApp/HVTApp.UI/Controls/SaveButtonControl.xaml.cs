using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.Controls
{
    public partial class SaveButtonControl : UserControl
    {
        public SaveButtonControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register(
            "Details", typeof(object), typeof(SaveButtonControl), new PropertyMetadata(default(object)));

        public object Details
        {
            get { return (object) GetValue(DetailsProperty); }
            set { SetValue(DetailsProperty, value); }
        }
    }
}
