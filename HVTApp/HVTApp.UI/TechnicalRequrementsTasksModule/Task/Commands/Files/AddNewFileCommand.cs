using System;
using System.IO;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
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
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var rootDirectoryPath = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;

                //�������� ������ ����
                foreach (var fileName in openFileDialog.FileNames)
                {
                    var fileWrapper = new TechnicalRequrementsFileWrapper(new TechnicalRequrementsFile());
                    try
                    {
                        File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                        fileWrapper.Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50);
                        ((TechnicalRequrements2Wrapper)ViewModel.SelectedItem).Files.Add(fileWrapper);
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                    }
                }
            }
        }
    }
}