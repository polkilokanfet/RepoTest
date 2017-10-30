using System.Windows;

namespace HVTApp.Services.MessageService
{
    public partial class YesNoWindow : Window
    {
        public YesNoWindow(string title, string message)
        {
            InitializeComponent();

            this.Title = title;
            this.Message.Text = message;
        }

        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
