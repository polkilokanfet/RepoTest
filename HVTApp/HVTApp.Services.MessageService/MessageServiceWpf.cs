using System.Windows;
using HVTApp.Infrastructure;

namespace HVTApp.Services.MessageService
{
    public class MessageServiceWpf : IMessageService
    {
        public MessageDialogResult ShowYesNoMessageDialog(string title, string message)
        {
            var window = new YesNoWindow(title, message);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
                return MessageDialogResult.Yes;
            return MessageDialogResult.No;
        }
    }
}