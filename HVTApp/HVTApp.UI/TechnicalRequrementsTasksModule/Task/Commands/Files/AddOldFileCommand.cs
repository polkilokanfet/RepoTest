using System.Linq;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    /// <summary>
    /// ������� ���������� ������������� ����� � ����������
    /// </summary>
    public class AddOldFileCommand : AddFileBaseCommand
    {
        public AddOldFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var selectedRequirements = (TechnicalRequrements2Wrapper)ViewModel.SelectedItem;

            //��� ����� � �������
            var allFilesWrappers = ViewModel.TechnicalRequrementsTaskWrapper.Requrements
                .SelectMany(requrements2Wrapper => requrements2Wrapper.Files)
                .Distinct().ToList();

            //�����, ������� ����� �������� � ��������� ����������
            var files = allFilesWrappers
                .Select(fileWrapper => fileWrapper.Model)
                .Distinct()
                .Where(file => selectedRequirements.Model.Files.Contains(file) == false);

            //����� ������
            var filesToAdd = Container.Resolve<ISelectService>().SelectItems(files);
            if (filesToAdd == null) return;

            //��������� ���� � ��������� ����������
            foreach (var file in filesToAdd)
            {
                var fileWrapper = allFilesWrappers.Single(x => x.Id == file.Id);
                selectedRequirements.Files.Add(fileWrapper);
            }
        }
    }
}