using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.PrintService.Extensions;
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

        /// <summary>
        /// Печать на официальном бланке организации
        /// </summary>
        protected void PrintOnLetterhead(Document document, string path)
        {
            //полный путь к файлу (с именем файла)
            var fullPath = this.GetFullPath(document, path);
            var docWriter = GetWordDocumentWriter(fullPath);
            if (docWriter == null) return;
            docWriter.StartDocument();

            #region Print Header

            docWriter.StartParagraph();
            docWriter.AddInlinePicture(GetImage("header.jpg"));
            docWriter.EndParagraph();

            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            var noBordersTableBorderProperties = docWriter.CreateTableBorderProperties();
            noBordersTableBorderProperties.Style = TableBorderStyle.None;
            noBordersTableBorderProperties.Sides = TableBorderSides.None;

            var headTableProperties = GetTableProperties(docWriter, noBordersTableBorderProperties);
            headTableProperties.Alignment = ParagraphAlignment.Left;
            headTableProperties.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(2, headTableProperties);

            //таблица с номером ТКП
            docWriter.StartTableRow();
            TableCellProperties tableCellProperties1 = docWriter.CreateTableCellProperties();
            tableCellProperties1.PreferredWidthAsPercentage = 50;
            docWriter.StartTableCell(tableCellProperties1);
            docWriter.StartTable(4, headTableProperties);

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "исх.№ ",
                $"{document.RegNumber}",
                "от",
                $"{document.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "на № ",
                document.RequestDocument == null ? string.Empty : $"{document.RequestDocument.RegNumber}",
                "от",
                document.RequestDocument == null ? string.Empty : $"{document.RequestDocument.Date.ToShortDateString()} г.");

            docWriter.EndTable();
            docWriter.EndTableCell();

            var recipient = document.RecipientEmployee;
            var sb = new StringBuilder();
            sb.Append("Получатель: " + Environment.NewLine + $"{recipient.Position} {recipient.Company.Form.ShortName} \"{recipient.Company.ShortName}\"" + Environment.NewLine + $"{recipient.Person}");
            if (document.CopyToRecipients.Any())
            {
                sb.Append(Environment.NewLine + "Копия:");
                foreach (var copyToRecipient in document.CopyToRecipients)
                {
                    sb.Append(Environment.NewLine + copyToRecipient);
                }
            }
            docWriter.PrintTableCell(sb.ToString());

            docWriter.EndTableRow();

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                $"{document.Comment}", string.Empty);

            docWriter.EndTable();

            #endregion

            #region Print Text Before Body

            docWriter.PrintParagraph(string.Empty);

            var paraFormat = docWriter.CreateParagraphProperties();
            paraFormat.Alignment = ParagraphAlignment.Center;
            var prefix = document.RecipientEmployee.Person.IsMan ? "Уважаемый" : "Уважаемая";
            docWriter.PrintParagraph($"{prefix} {document.RecipientEmployee.Person.Name} {document.RecipientEmployee.Person.Patronymic}!", paraFormat, fontBold);

            #endregion

            this.PrintBody(document, docWriter);

            #region Sender

            docWriter.PrintParagraph(string.Empty);

            var bordProps = docWriter.CreateTableBorderProperties();
            bordProps.Style = TableBorderStyle.None;
            bordProps.Sides = TableBorderSides.None;

            TableProperties tableProperties2 = GetTableProperties(docWriter, bordProps);
            tableProperties2.PreferredWidthAsPercentage = 100;
            docWriter.StartTable(3, tableProperties2);

            TableCellProperties tableCellProperties2 = docWriter.CreateTableCellProperties();
            tableCellProperties2.PreferredWidthAsPercentage = 33;

            docWriter.StartTableRow();

            var part1 = "С уважением," + Environment.NewLine + $"{document.SenderEmployee.Position}";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            //подпись
            if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer?.Id)
            {
                var drt = Container.Resolve<IMessageService>().ConfirmationDialog("Подпись", "Печать с подписью?", defaultNo: true);
                if (drt)
                {
                    try
                    {
                        docWriter.StartTableCell(tableCellProperties2);

                        var paragraphProperties = docWriter.CreateParagraphProperties();
                        paragraphProperties.Alignment = ParagraphAlignment.Center;
                        docWriter.StartParagraph(paragraphProperties);
                        docWriter.AddInlinePicture(GetImage("sign_deev.png"));
                        docWriter.EndParagraph();

                        docWriter.EndTableCell();

                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.PrintAllExceptions());
                        docWriter.PrintTableCell(string.Empty);
                    }
                }
                else
                {
                    docWriter.PrintTableCell(string.Empty);
                }
            }
            else
            {
                docWriter.PrintTableCell(string.Empty);
            }

            //ФИО подписывающего лица
            var part3 = Environment.NewLine + $"{document.SenderEmployee.Person.Name[0]}.{document.SenderEmployee.Person.Patronymic?[0]}.{document.SenderEmployee.Person.Surname}";
            var pp = docWriter.CreateParagraphProperties();
            pp.Alignment = ParagraphAlignment.Right;
            docWriter.PrintTableCell(part3, tableCellProperties2, pp);

            docWriter.EndTableRow();

            docWriter.EndTable();

            #endregion

            #region Author Footer

            var parts = SectionHeaderFooterParts.FooterAllPages;
            var writerSet = docWriter.AddSectionHeaderFooter(parts);
            writerSet.FooterWriterAllPages.Open();
            writerSet.FooterWriterAllPages.StartParagraph();
            writerSet.FooterWriterAllPages.AddTextRun("Исполнитель:" + Environment.NewLine + $"{document.Author}" + Environment.NewLine + $"тел.: {document.Author.PhoneNumber}; e-mail: {document.Author.Email}; uetm.ru");
            writerSet.FooterWriterAllPages.AddTextRun(Environment.NewLine + $"{document.RegNumber} от {document.Date.ToShortDateString()} г. - стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();
            writerSet.FooterWriterAllPages.Close();

            #endregion

            //docWriter.DefineSection(sp);

            docWriter.EndDocument();
            docWriter.Close();

            OpenDocument(fullPath);
        }

        protected abstract string GetFullPath(Document document, string path);
        protected abstract void PrintBody(Document document, WordDocumentWriter docWriter);

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
                MessageService.Message(e.GetType().Name, e.Message);
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
            var dr = MessageService.ConfirmationDialog("Формирование документа завершено. Открыть результат?", defaultYes: true);
            if (dr)
            {
                try
                {
                    System.Diagnostics.Process.Start(fullPath);
                }
                catch (Exception e)
                {
                    MessageService.Message("Error", e.PrintAllExceptions());
                }
            }
        }
    }
}