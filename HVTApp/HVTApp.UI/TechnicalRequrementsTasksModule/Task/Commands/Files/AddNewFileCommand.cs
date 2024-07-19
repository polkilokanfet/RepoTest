using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AddNewFileCommand : AddFileBaseCommand
    {
        public AddNewFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var filePaths = Container.Resolve<IGetFilePaths>().GetFilePaths().ToList();
            if (filePaths.Any() == false) return;

            var filesStorageService = Container.Resolve<IFilesStorageService>();
            var storagePath = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;

            //копируем каждый файл
            foreach (var filePath in filePaths)
            {
                var fileWrapper = new TechnicalRequrementsFileWrapper(new TechnicalRequrementsFile());
                filesStorageService.LoadFileToStorage(storagePath, filePath, fileWrapper.Model.Id);
                fileWrapper.Name = Path.GetFileNameWithoutExtension(filePath).LimitLength(50);
                ((TechnicalRequrements2Wrapper) ViewModel.SelectedItem).Files.Add(fileWrapper);
            }
        }
    }
}