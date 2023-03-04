using System.Windows;
using System.Windows.Markup;

namespace HVTApp.UI.PriceEngineering.View
{
    [ContentProperty("Products")]
    public partial class TasksControl
    {
        #region Products

        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register(
            "Products", 
            typeof(object), 
            typeof(TasksControl), 
            new PropertyMetadata(default(object)));

        public object Products
        {
            get => (object) GetValue(ProductsProperty);
            set => SetValue(ProductsProperty, value);
        }

        #endregion

        #region CommonInformation

        public static readonly DependencyProperty CommonInformationProperty = DependencyProperty.Register(
            "CommonInformation", 
            typeof(object), 
            typeof(TasksControl), 
            new PropertyMetadata(default(object)));

        public object CommonInformation
        {
            get => (object) GetValue(CommonInformationProperty);
            set => SetValue(CommonInformationProperty, value);
        }

        #endregion

        public TasksControl()
        {
            InitializeComponent();
        }
    }
}
