using System.Linq;
using System.Windows;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.MessageService
{
    public class MessageServiceWpf : IMessageService
    {
        public bool ConfirmationDialog(string title, string message, bool defaultYes = false, bool defaultNo = false)
        {
            var window = new YesNoWindow(title, message, defaultYes, defaultNo)
            {
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(window1 => window1.IsActive)
            };
            window.ShowDialog();
            return window.DialogResult.HasValue && window.DialogResult.Value;
        }

        public bool ConfirmationDialog(string message, bool defaultYes = false, bool defaultNo = false)
        {
            return this.ConfirmationDialog("Уведомление", message, defaultYes, defaultNo);
        }

        public void Message(string title, string message)
        {
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive);
            if (owner == null)
            {
                MessageBox.Show(message, title);
            }
            else
            {
                MessageBox.Show(owner, message, title);
            }
        }
    }
}