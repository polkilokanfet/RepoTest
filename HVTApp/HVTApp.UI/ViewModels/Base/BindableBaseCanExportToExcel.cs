using System;
using System.Diagnostics;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Documents.Excel;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BindableBaseCanExportToExcel : ViewModelBase
    {
        public ICommand ExportToExcel { get; }

        protected BindableBaseCanExportToExcel(IUnityContainer container) : base(container)
        {
            ExportToExcel = new DelegateCommand<XamDataGrid>(ExportToExcel_Execute);
        }

        private void ExportToExcel_Execute(XamDataGrid grid)
        {
            var messageService = Container.Resolve<IMessageService>();

            string fileName = GetUnusedFileName();
            if (string.IsNullOrEmpty(fileName))
            {
                messageService.ShowYesNoMessageDialog("������", "��� ���������� ����� �����.");
                return;
            }

            try
            {
                var exporter = new DataPresenterExcelExporter();
                exporter.Export(grid, fileName, WorkbookFormat.Excel2007, this.ExportOptions);
            }
            catch (Exception ex)
            {
                messageService.ShowYesNoMessageDialog("������ �������� � ����", ex.Message);
                return;
            }

            // Execute Excel to display the exported workbook.
            if (messageService.ShowYesNoMessageDialog("������� ������� ��������", "�������� ���������� ��������?") ==
                MessageDialogResult.Yes)
            {
                try
                {
                    var p = new Process {StartInfo = {FileName = fileName}};
                    p.Start();
                }
                catch (Exception ex)
                {
                    messageService.ShowYesNoMessageDialog("������", ex.Message);
                }
            }

        }

        private ExportOptions _exportOptions;
        public ExportOptions ExportOptions => _exportOptions ?? (_exportOptions = new ExportOptions());

        private string GetUnusedFileName()
        {
            for (int i = 1; i < 50000; i++)
            {
                string fileName = "ExportToExcel" + i.ToString() + ".xlsx";
                if (false == System.IO.File.Exists(fileName))
                    return fileName;
            }

            return string.Empty;
        }
        
    }
}