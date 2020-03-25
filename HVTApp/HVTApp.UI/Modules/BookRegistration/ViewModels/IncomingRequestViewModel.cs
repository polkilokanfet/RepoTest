using System;
using System.Diagnostics;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestViewModel : IncomingRequestDetailsViewModel
    {
        //поручить запрос
        public ICommand InstructRequestCommand { get; }
        public ICommand OpenFolderCommand { get; }

        public IncomingRequestViewModel(IUnityContainer container) : base(container)
        {
            InstructRequestCommand = new DelegateCommand(
                () =>
                {
                    //сохраняем запрос
                    SaveCommand.Execute(null);
                    //закрываем запрос
                    GoBackCommand.Execute(null);
                },
                () => Item.IsValid && Item.IsChanged);

            OpenFolderCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Путь к хранилищу приложений не пазначен");
                        return;
                    }

                    var path = PathGetter.GetPath(Item.Model.Document);
                    Process.Start("explorer", $"\"{path}\"");
                });
        }
        
        protected override void AfterLoading()
        {
            base.AfterLoading();
            Item.Performers.CollectionChanged += (sender, args) =>
            {
                ((DelegateCommand) InstructRequestCommand).RaiseCanExecuteChanged();
            };
        }
    }
}