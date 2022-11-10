using System.Windows;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectProductWindow
    {
        public bool ShouldSelectComplect { get; private set; } = false;

        public SelectProductWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ButtonComplects_OnClick(object sender, RoutedEventArgs e)
        {
            ShouldSelectComplect = true;
            this.Close();
        }
    }
}
