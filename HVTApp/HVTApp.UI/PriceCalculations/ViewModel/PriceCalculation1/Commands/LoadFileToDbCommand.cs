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

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class LoadFileToDbCommand : BasePriceCalculationCommand
    {
        public LoadFileToDbCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var fileNames = Container.Resolve<IGetFilePaths>().GetFilePaths().ToList();
            if (fileNames.Any() == false) return;

            var rootDirectoryPath = GlobalAppProperties.Actual.PriceCalculationsFilesPath;

            //копируем каждый файл
            foreach (var fileName in fileNames)
            {
                var fileWrapper = new PriceCalculationFileWrapper(new PriceCalculationFile());
                try
                {
                    File.Copy(fileName, $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(fileName)}");
                    ViewModel.PriceCalculationWrapper.Files.Add(fileWrapper);
                }
                catch (Exception e)
                {
                    MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                }
            }

            ViewModel.CalculationHasFileOnPropertyChanged();

            //костыль
            if ((ViewModel.SaveCommand).CanExecute())
            {
                (ViewModel.SaveCommand).Execute();
            }
        }
    }
}