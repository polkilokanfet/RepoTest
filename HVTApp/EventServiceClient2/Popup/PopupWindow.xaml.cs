using System;
using System.Windows;

namespace EventServiceClient2.Popup
{
    public partial class PopupWindow
    {
        private readonly Action _action;

        public PopupWindow(string text, string title = null, Action action = null)
        {
            _action = action;
            InitializeComponent();
            if (title != null)
            {
                this.Title = title;
            }
            TextBlock.Text = text;

            //для того, чтобы уведомление оставалось наверху
            this.Deactivated += (sender, args) =>
            {
                Window window = (Window) sender;
                window.Topmost = true;
            };
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
