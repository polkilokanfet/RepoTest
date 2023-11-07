using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace HVTApp.Services.MessageService
{
    public partial class YesNoWindow
    {
        public YesNoWindow(string title, string message, bool defaultYes = false, bool defaultNo = false)
        {
            InitializeComponent();

            this.Title = title;
            this.Message.Text = message;

            this.YesButton.IsDefault = defaultYes;
            this.NoButton.IsDefault = defaultNo;
            this.Loaded += OnLoaded;
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


        //Всё что ниже нужно для скрытия кнопки "Закрыть"
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            this.Loaded -= OnLoaded;
        }
    }
}
