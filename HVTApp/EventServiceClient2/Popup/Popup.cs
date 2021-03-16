using System;
using System.Windows;

namespace EventServiceClient2.Popup
{
    static class Popup
    {
        public static void ShowPopup(string text, string title = null, Action action = null)
        {
            //переводим всплывающее окно в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    new PopupWindow(text, title, action).Show();
                });
        }
    }
}
