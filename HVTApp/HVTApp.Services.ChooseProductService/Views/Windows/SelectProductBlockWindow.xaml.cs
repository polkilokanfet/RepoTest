using System.Windows;

namespace HVTApp.Services.GetProductService
{
    public partial class SelectProductBlockWindow
    {
        public bool ShoodCreateNew { get; private set; } = false;
        public bool ShoodSelectComplect { get; private set; } = false;
        public SelectProductBlockWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ButtonNew_OnClick(object sender, RoutedEventArgs e)
        {
            ShoodCreateNew = true;
            this.Close();
        }

        private void ButtonComplects_OnClick(object sender, RoutedEventArgs e)
        {
            ShoodSelectComplect = true;
            this.Close();
        }
    }
}
