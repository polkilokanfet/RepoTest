using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
                    messageService.ShowOkMessageDialog("Ошибка экспорта в файл", ex.GetAllExceptions());
                    return;
                }

                // Execute Excel to display the exported workbook.
                if (messageService.ShowYesNoMessageDialog("Экспорт успешно завершен", "Показать результаты экспорта?") == MessageDialogResult.Yes)
                {
                    try
                    {
                        var p = new Process {StartInfo = {FileName = fileName}};
                        p.Start();
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowOkMessageDialog("Ошибка", ex.GetAllExceptions());
                    }
                }
            });
        }

        private ExportOptions _exportOptions;
        public ExportOptions ExportOptions => _exportOptions ?? (_exportOptions = new ExportOptions());        
    }

    public abstract class LoadableBindableBaseCanExportToExcel : ViewModelBaseCanExportToExcel
    {

        private bool _isLoaded;

        protected LoadableBindableBaseCanExportToExcel(IUnityContainer container) : base(container)
        {
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                if (Equals(_isLoaded, value))
                    return;
                _isLoaded = value;
                OnPropertyChanged();
            }
        }


        public void Load()
        {
            IsLoaded = false;
            LoadedMethod();
            IsLoaded = true;
        }

        protected abstract void LoadedMethod();
    }

}