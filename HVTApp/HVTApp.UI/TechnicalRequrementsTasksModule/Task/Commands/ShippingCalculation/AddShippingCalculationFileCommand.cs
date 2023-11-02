using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AddShippingCalculationFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public AddShippingCalculationFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var fileNames = Container.Resolve<IGetFilePaths>().GetFilePaths().ToList();

            if (fileNames.Any())
            {
                var rootDirectoryPath = GlobalAppProperties.Actual.ShippingCostFilesPath;

                //�������� ������ ����
                foreach (var fileName in fileNames)
                {
                    try
                    {
                        var fileWrapper = new ShippingCostFileWrapper(new ShippingCostFile());
                        File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                        ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Add(fileWrapper);

                        ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageService.Message("Exception", e.PrintAllExceptions());
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