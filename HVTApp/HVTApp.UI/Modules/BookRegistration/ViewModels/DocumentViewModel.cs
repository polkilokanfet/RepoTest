using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class DocumentViewModel : DocumentDetailsViewModel
    {
        public DocumentDirection Direction { get; private set; } = DocumentDirection.Outgoing;

        public string DocType => Direction == DocumentDirection.Outgoing
            ? "Исходящий документ"
            : "Входящий документ";

        public ICommand OpenFolderCommand { get; }
        public ICommand AddFilesCommand { get; }

        public DocumentViewModel(IUnityContainer container) : base(container)
        {
            OpenFolderCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = PathGetter.GetPath(Item.Model);
                    Process.Start("explorer", $"\"{path}\"");
                });

            AddFilesCommand = new DelegateCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var rootDirectoryPath = PathGetter.GetPath(Item.Model);
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            File.Copy(fileName, $"{rootDirectoryPath}\\{Path.GetFileName(fileName)}");
                        }
                    }
                });
        }

        public void Load2(Document document, string direction)
        {
            this.Load(new Document());

            if(Equals(direction, DocumentDirection.Incoming.ToString()))
                Direction = DocumentDirection.Incoming;

            if (document.Author != null)
                Item.Author = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.Author.Id));

            if (document.SenderEmployee != null)
                Item.SenderEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.SenderEmployee.Id));

            if (document.RecipientEmployee != null)
                Item.RecipientEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.RecipientEmployee.Id));
        }
    }
}