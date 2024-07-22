using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.Specifications
{
    public partial class SpecificationsControl : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", 
            typeof(SpecificationsViewModelBase), 
            typeof(SpecificationsControl), 
            new PropertyMetadata(default(SpecificationsViewModelBase)));

        public SpecificationsViewModelBase ViewModel
        {
            get => (SpecificationsViewModelBase) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        public SpecificationsControl()
        {
            InitializeComponent();
        }
    }
}
