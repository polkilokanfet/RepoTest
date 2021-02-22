using System;

namespace EventServiceClient2.Popup
{
    static class Popup
    {
        public static void ShowPopup(string text, string title = null, Action action = null)
        {
            new PopupWindow(text, title, action).Show();
        }
    }
}
