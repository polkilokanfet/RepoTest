using System.Windows;

namespace HVTApp.Services.MessageService
{
    public partial class YesNoWindow : Window
    {
        public YesNoWindow(string title, string message, bool defaultYes = false, bool defaultNo = false)
        {
            InitializeComponent();

            this.Title = title;
            this.Message.Text = message;

            this.YesButton.IsDefault = defaultYes;
            this.NoButton.IsDefault = defaultNo;
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
