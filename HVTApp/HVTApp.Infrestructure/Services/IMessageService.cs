namespace HVTApp.Infrastructure.Services
{
    public interface IMessageService
    {
        MessageDialogResult ShowYesNoMessageDialog(string title, string message);
    }
}
