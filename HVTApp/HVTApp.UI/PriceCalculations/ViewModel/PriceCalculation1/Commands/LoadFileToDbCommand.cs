using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
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
            var filePath = Container.Resolve<IGetFilePaths>().GetFilePath();
            if (string.IsNullOrEmpty(filePath)) return;

            //копируем файл
            var fileWrapper = new PriceCalculationFileWrapper(new PriceCalculationFile());
            var rootDirectoryPath = GlobalAppProperties.Actual.PriceCalculationsFilesPath;
            var destPath = $"{rootDirectoryPath}\\{fileWrapper.Id}{Path.GetExtension(filePath)}";
            try
            {
                File.Copy(filePath, destPath);
                ViewModel.PriceCalculationWrapper.Files.Add(fileWrapper);
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                return;
            }

            ViewModel.CalculationHasFileOnPropertyChanged();

            //костыль
            if (ViewModel.SaveCommand.CanExecute())
            {
                ViewModel.SaveCommand.Execute();
            }

            var dialogResult = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Скопировать данные из загруженного расчёта?", defaultYes:true);
            if (dialogResult == MessageDialogResult.Yes)
                LoadCostsFromFile(destPath);
        }

        private void LoadCostsFromFile(string path)
        {
            var sccs = ViewModel.PriceCalculationWrapper.PriceCalculationItems
                .SelectMany(x => x.StructureCosts)
                .ToList();

            var costs = Container.Resolve<IGetInformationFromExcelFileService>().GetCostsDictionaryFromCalculationFile(path);
            foreach (var cost in costs)
            {
                foreach (var scc in sccs.Where(x => cost.Key.ToLower().Equals(x.Number.ToLower())))
                {
                    scc.UnitPrice = cost.Value;
                }
            }

        }
    }
}