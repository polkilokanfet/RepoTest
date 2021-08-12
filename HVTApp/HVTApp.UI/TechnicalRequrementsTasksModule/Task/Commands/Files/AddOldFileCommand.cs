using System.Linq;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AddOldFileCommand : AddFileBaseCommand
    {
        public AddOldFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var selectedRequirements = (TechnicalRequrements2Wrapper)ViewModel.SelectedItem;

            var files =
                ViewModel.TechnicalRequrementsTaskWrapper.Requrements
                    .Where(requrements2Wrapper => requrements2Wrapper.Model.Id != selectedRequirements.Model.Id)
                    .SelectMany(requrements2Wrapper => requrements2Wrapper.Files)
                    .Select(fileWrapper => fileWrapper.Model)
                    .Distinct();

            var selectService = Container.Resolve<ISelectService>();
            var file = selectService.SelectItem(files);

            //добавляем файл в выбранные требования
            if (file != null)
            {
                var fileWrapper = ViewModel.TechnicalRequrementsTaskWrapper.Requrements
                    .SelectMany(requrements2Wrapper => requrements2Wrapper.Files)
                    .Distinct()
                    .Single(technicalRequrementsFileWrapper => technicalRequrementsFileWrapper.Id == file.Id);

                selectedRequirements.Files.Add(fileWrapper);
            }
        }
    }
}