using HVTApp.Infrastructure;

namespace HVTApp.Services.MessageService
{
    public interface IMessageService
    {
        MessageDialogResult ShowYesNoMessageDialog(string title, string message);
    }
}
