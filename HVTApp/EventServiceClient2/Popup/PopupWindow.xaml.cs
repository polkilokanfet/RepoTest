using System;
using System.Windows;

namespace EventServiceClient2.Popup
{
    public partial class PopupWindow
    {
        private readonly Action _action;

        public PopupWindow(string text, Action action = null)
        {
            _action = action;
            InitializeComponent();
            TextBlock.Text = text;
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            _action?.Invoke();
        }
    }
}
