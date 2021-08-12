using System;
using System.IO;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AddNewFileAnswersCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public AddNewFileAnswersCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
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
                var rootDirectoryPath = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;

                //копируем каждый файл
                foreach (var fileName in openFileDialog.FileNames)
                {
                    try
                    {
                        var fileWrapper = new AnswerFileTceWrapper(new AnswerFileTce())
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50)
                        };
                        File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                        ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Add(fileWrapper);

                        ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                    }
                }
            }

            ViewModel.LoadAllFileAnswersCommand.RaiseCanExecuteChanged();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted &&
                   !ViewModel.IsFinished &&
                   !ViewModel.IsRejected;
        }
    }
}