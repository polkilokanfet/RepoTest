using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Services.MessageService
{
    public class MessageServiceWpf : IMessageService
    {
        public MessageDialogResult ShowYesNoMessageDialog(string title, string message, bool defaultYes = false, bool defaultNo = false)
        {
            var window = new YesNoWindow(title, message, defaultYes, defaultNo)
            {
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)
            };
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
                return MessageDialogResult.Yes;
            return MessageDialogResult.No;
        }

        public void ShowOkMessageDialog(string title, string message)
        {
            MessageBox.Show(Application.Current.Windows.OfType<Window>().Single(x => x.IsActive), message, title);
        }
    }
}