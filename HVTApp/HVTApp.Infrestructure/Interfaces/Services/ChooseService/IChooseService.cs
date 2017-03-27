using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseService
    {
        void Register<TViewModel, TView, TChoosenItem>()
            where TViewModel : IChooseViewModel<TChoosenItem>
            where TView : IDialog;

        bool? ShowDialog<TViewModel, TChoosenItem>(TViewModel viewModel, out TChoosenItem choosenItem)
            where TViewModel : IChooseViewModel<TChoosenItem>;
    }
}