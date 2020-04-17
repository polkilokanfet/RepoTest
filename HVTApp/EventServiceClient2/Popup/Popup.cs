using System;

namespace EventServiceClient2.Popup
{
    static class Popup
    {
        public static void ShowPopup(string text, Action action = null)
        {
            new PopupWindow(text, action).Show();
        }
    }
}
