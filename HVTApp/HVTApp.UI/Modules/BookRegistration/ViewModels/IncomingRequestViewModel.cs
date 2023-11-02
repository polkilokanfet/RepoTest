using System;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestViewModel : IncomingRequestDetailsViewModel
    {
        //�������� ������
        public DelegateLogCommand InstructRequestCommand { get; }
        public DelegateLogCommand RequestIsNotActualCommand { get; }
        public DelegateLogCommand OpenFolderCommand { get; }

        public IncomingRequestViewModel(IUnityContainer container) : base(container)
        {
            var messageService = container.Resolve<IMessageService>();
            var fileManagerService = container.Resolve<IFileManagerService>();

            InstructRequestCommand = new DelegateLogCommand(
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

            RequestIsNotActualCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = messageService.ConfirmationDialog("�������������", "�� �������, ��� ������ �� ��������?", defaultYes: true);
                    if (dr == false) return;

                    Item.IsActual = false;
                    Item.Performers.Clear();

                    //��������� ������
                    if (Item.IsChanged)
                        SaveCommand.Execute(null);
                    //��������� ������
                    GoBackCommand.Execute(null);
                },
                () => Item.IsValid);

            OpenFolderCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        messageService.Message("����������", "���� � ��������� ���������� �� ��������");
                        return;
                    }

                    var path = fileManagerService.GetPath(Item.Model.Document);
                    Process.Start($"\"{path}\"");
                });
        }
        
        protected override void AfterLoading()
        {
            base.AfterLoading();
            Item.Performers.CollectionChanged += (sender, args) =>
            {
                ( InstructRequestCommand).RaiseCanExecuteChanged();
            };
        }

        protected override void SaveItem()
        {
            base.SaveItem();
            //Container.Resolve<IEventAggregator>().GetEvent<AfterSaveIncomingRequestSyncEvent>().Publish(Item.Model);
        }
    }
}