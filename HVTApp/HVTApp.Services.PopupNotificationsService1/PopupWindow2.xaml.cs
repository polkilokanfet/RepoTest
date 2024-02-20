using System;
using System.Windows;

namespace HVTApp.Services.PopupNotificationsService1
{
    public partial class PopupWindow2
    {
        private readonly Action _action;

        public PopupWindow2(string text, string title = null, Action action = null)
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
            //закрываем уведомление
            this.Close();

            //запускаем действие
            _action?.Invoke();

            //делаем главное окно активным
            if (System.Windows.Application.Current.MainWindow != null)
            { 
                System.Windows.Application.Current.MainWindow.Dispatcher.Invoke(
                    () =>
                    {
                        var mainWindow = System.Windows.Application.Current.MainWindow;
                        mainWindow.Activate();
                        if (mainWindow.WindowState == WindowState.Minimized)
                        {
                            mainWindow.WindowState = System.Windows.WindowState.Normal;
                        }
                        //win.Topmost = true;
                        //win.Focus();
                    });
            }
        }
    }
}
