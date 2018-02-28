using System;
using System.Diagnostics;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Services.MessageService;
using Infragistics.Documents.Excel;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BindableBaseCanExportToExcel : BindableBase
    {
        private readonly IUnityContainer _container;
        public ICommand ExportToExcel { get; }

        protected BindableBaseCanExportToExcel(IUnityContainer container)
        {
            _container = container;
            ExportToExcel = new DelegateCommand<XamDataGrid>(ExportToExcel_Execute);
        }

        private void ExportToExcel_Execute(XamDataGrid grid)
        {
            var messageService = _container.Resolve<IMessageService>();

            string fileName = GetUnusedFileName();
            if (string.IsNullOrEmpty(fileName))
            {
                messageService.ShowYesNoMessageDialog("Ошибка", "Нет свободного имени файла.");
                return;
            }

            try
            {
                var exporter = new DataPresenterExcelExporter();
                exporter.Export(grid, fileName, WorkbookFormat.Excel2007, this.ExportOptions);
            }
            catch (Exception ex)
            {
                messageService.ShowYesNoMessageDialog("Ошибка экспорта в файл", ex.Message);
                return;
            }

            // Execute Excel to display the exported workbook.
            if (messageService.ShowYesNoMessageDialog("Экспорт успешно завершен", "Показать результаты экспорта?") ==
                MessageDialogResult.Yes)
            {
                try
                {
                    var p = new Process();
                    p.StartInfo.FileName = fileName;
                    p.Start();
                }
                catch (Exception ex)
                {
                    messageService.ShowYesNoMessageDialog("Ошибка", ex.Message);
                }
            }

        }

        private ExportOptions _exportOptions;
        public ExportOptions ExportOptions => _exportOptions ?? (_exportOptions = new ExportOptions());

        private string GetUnusedFileName()
        {
            for (int i = 1; i < 5000; i++)
            {
                string fileName = "DataPresenterExportToExcelTest" + i.ToString() + ".xlsx";
                if (false == System.IO.File.Exists(fileName))
                    return fileName;
            }

            return string.Empty;
        }
        
    }
}