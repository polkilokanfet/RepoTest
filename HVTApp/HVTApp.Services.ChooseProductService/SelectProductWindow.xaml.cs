using System.Windows;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectProductWindow
    {
        public SelectProductWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
