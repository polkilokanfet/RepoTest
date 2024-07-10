using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public partial class CommonInformationControl : UserControl
    {
        public static readonly DependencyProperty OriginalIsRequiredProperty = DependencyProperty.Register(
            "OriginalIsRequired", 
            typeof(bool), 
            typeof(CommonInformationControl), 
            new PropertyMetadata(default(bool)));

        public bool OriginalIsRequired
        {
            get => (bool) GetValue(OriginalIsRequiredProperty);
            set => SetValue(OriginalIsRequiredProperty, value);
        }


        public static readonly DependencyProperty CommentProperty = DependencyProperty.Register(
            "Comment", typeof(string), typeof(CommonInformationControl), new PropertyMetadata(default(string)));

        public string Comment
        {
            get => (string) GetValue(CommentProperty);
            set => SetValue(CommentProperty, value);
        }

        public CommonInformationControl()
        {
            InitializeComponent();
        }
    }
}
