using System.Windows;
using System.Windows.Controls;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectParameterControl : UserControl
    {
        public SelectParameterControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ParameterSelectorProperty = DependencyProperty.Register(
            "ParameterSelector", typeof(ParameterSelector), typeof(SelectParameterControl), new PropertyMetadata(default(ParameterSelector)));

        public ParameterSelector ParameterSelector
        {
            get => (ParameterSelector) GetValue(ParameterSelectorProperty);
            set => SetValue(ParameterSelectorProperty, value);
        }
    }
}
