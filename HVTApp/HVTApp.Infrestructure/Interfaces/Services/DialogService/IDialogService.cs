namespace HVTApp.Infrastructure.Interfaces.Services.DialogService
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>() 
            where TViewModel : IDialogRequestClose
            where TView : IDialog;
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }
}