using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestViewModel : IncomingRequestDetailsViewModel
    {
        private readonly IMessageService _messageService;

        //�������� ������
        public ICommand InstructRequestCommand { get; }
        public ICommand RequestIsNotActualCommand { get; }
        public ICommand OpenFolderCommand { get; }

        public IncomingRequestViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            InstructRequestCommand = new DelegateCommand(
                () =>
                {
                    Item.IsActual = true;
                    Item.InstructionDate = DateTime.Now;
                    //��������� ������
                    SaveCommand.Execute(null);
                    //��������� ������
                    GoBackCommand.Execute(null);
                },
                () => Item.IsValid && Item.IsChanged && Item.Performers.Any());

            RequestIsNotActualCommand = new DelegateCommand(
                () =>
                {
                    var dr = _messageService.ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ �� ��������?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    Item.IsActual = false;
                    Item.Performers.Clear();

                    //��������� ������
                    if (Item.IsChanged)
                        SaveCommand.Execute(null);
                    //��������� ������
                    GoBackCommand.Execute(null);
                },
                () => Item.IsValid);

            OpenFolderCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        _messageService.ShowOkMessageDialog("����������", "���� � ��������� ���������� �� ��������");
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