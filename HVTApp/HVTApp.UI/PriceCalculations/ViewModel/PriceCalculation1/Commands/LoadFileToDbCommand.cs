using System;
using System.IO;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
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
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var rootDirectoryPath = GlobalAppProperties.Actual.PriceCalculationsFilesPath;

                //�������� ������ ����
                foreach (var fileName in openFileDialog.FileNames)
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

                //�������
                if ((ViewModel.SaveCommand).CanExecute())
                {
                    (ViewModel.SaveCommand).Execute();
                }
            }
        }
    }
}