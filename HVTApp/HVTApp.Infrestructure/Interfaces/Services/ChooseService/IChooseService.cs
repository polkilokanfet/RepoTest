using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseService
    {
        void Register<TViewModel, TView>()
            where TViewModel : IChooseViewModel 
            where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IChooseViewModel;
    }
}