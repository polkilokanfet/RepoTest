using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public abstract class PrintServiceBase
    {
        protected readonly IUnityContainer Container;
        protected readonly IMessageService MessageService;

        protected PrintServiceBase(IUnityContainer container)
        {
            Container = container;
            MessageService = Container.Resolve<IMessageService>();
        }

        protected WordDocumentWriter GetWordDocumentWriter(string fullPath)
        {
            WordDocumentWriter docWriter;
            try
            {
                docWriter = WordDocumentWriter.Create(fullPath);

                docWriter.DefaultParagraphProperties.Alignment = ParagraphAlignment.Left;
                docWriter.Unit = UnitOfMeasurement.Centimeter;

                docWriter.FinalSectionProperties.PageSize = new Size(21.0, 29.7);
                docWriter.FinalSectionProperties.PageMargins = new Padding(2.5f, 1.5f, 1.3f, 1.5f);
                docWriter.FinalSectionProperties.FooterMargin = 0.7f;

                docWriter.DocumentProperties.Author = GlobalAppProperties.User.Employee.Person.ToString();
                docWriter.DocumentProperties.Company = "УЭТМ";
            }
            catch (IOException e)
            {
                MessageService.ShowOkMessageDialog(e.GetType().Name, e.Message);
                return null;
            }

            return docWriter;
        }

        protected TableProperties GetTableProperties(WordDocumentWriter docWriter, TableBorderProperties borderProps)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Left;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;
            return tableProps;
        }

        protected TableBorderProperties GetTableBorderProperties(WordDocumentWriter docWriter)
        {
            var borderProps = docWriter.CreateTableBorderProperties();
            borderProps.Color = Colors.Black;
            borderProps.Style = TableBorderStyle.Single;
            return borderProps;
        }

        #region GetImage

        protected BitmapSource GetImage(string resourceName)
        {
            var uri = new Uri("pack://application:,,,/HVTApp.Services.PrintService;component/Images/" + resourceName);
            return new BitmapImage(uri);
            //var uri = new Uri(@"..\..\Images\" + resourceName, UriKind.Relative);
            //return new BitmapImage(uri);
            //return File.Exists(uri.AbsolutePath) ? new BitmapImage(uri) : null;
            //return new BitmapImage(new Uri(@"HVTApp.Services.PrintService;component/Images/" + resourceName));
        }

        #endregion GetImage

        protected void OpenDocument(string fullPath)
        {
            var dr = MessageService.ShowYesNoMessageDialog("Процесс формирования документа завершен",
                "Формирование документа завершено. Открыть результат?", defaultYes: true);
            if (dr == MessageDialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(fullPath);
                }
                catch (Exception e)
                {
                    MessageService.ShowOkMessageDialog("Error", e.PrintAllExceptions());
                }
            }
        }


    }
}