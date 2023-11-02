using System;
using System.Diagnostics;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Infragistics.Documents.Excel;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class ViewModelBaseCanExportToExcel : ViewModelBase
    {
        public ICommand ExportToExcel { get; }

        protected ViewModelBaseCanExportToExcel(IUnityContainer container) : base(container)
        {
            ExportToExcel = new DelegateCommand<XamDataGrid>(grid =>
            {
                var messageService = Container.Resolve<IMessageService>();

                var fileName = $"{this.GetType().Name}-{DateTime.Now.ToFileTime()}.xlsx";

                try
                {
                    var exporter = new DataPresenterExcelExporter();
                    exporter.Export(grid, fileName, WorkbookFormat.Excel2007, this.ExportOptions);
                }
                catch (Exception ex)
                {
                    messageService.Message("Ошибка экспорта в файл", ex.PrintAllExceptions());
                    return;
                }

                // Execute Excel to display the exported workbook.
                if (messageService.ConfirmationDialog("Экспорт успешно завершен", "Показать результаты экспорта?", defaultYes:true))
                {
                    try
                    {
                        var p = new Process {StartInfo = {FileName = fileName}};
                        p.Start();
                    }
                    catch (Exception ex)
                    {
                        messageService.Message("Ошибка", ex.PrintAllExceptions());
                    }
                }
            });
        }

        private ExportOptions _exportOptions;
        public ExportOptions ExportOptions => _exportOptions ?? (_exportOptions = new ExportOptions());        
    }
}