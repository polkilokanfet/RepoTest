using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class LoadFileFromDbCommand : BasePriceCalculationCommand
    {
        public LoadFileFromDbCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var file = ViewModel.PriceCalculationWrapper.Files.First().Model;
            if (ViewModel.PriceCalculationWrapper.Files.Count > 1)
            {
                var selectService = Container.Resolve<ISelectService>();
                file = selectService.SelectItem(ViewModel.PriceCalculationWrapper.Files.Select(x => x.Model));
                if (file == null)
                    return;
            }

            var storageDirectory = GlobalAppProperties.Actual.PriceCalculationsFilesPath;
            string addToFileName = $"{file.CreationMoment.ToShortDateString()} {file.CreationMoment.ToShortTimeString()}";
            FilesStorage.CopyFileFromStorage(file.Id, MessageService, storageDirectory, addToFileName: addToFileName.ReplaceUncorrectSimbols("-"));
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.CalculationHasFile;
        }
    }
}