using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintBlankLetterService : IPrintBlankLetterService
    {
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;

        public PrintBlankLetterService(IUnityContainer container, IMessageService messageService)
        {
            _container = container;
            _messageService = messageService;
        }

        public void PrintBlankLetter(Document letter, string path)
        {
            var docWriter = GetWordDocumentWriter(letter, path);
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
                $"{letter.RegNumber}", 
                "от", 
                $"{letter.Date.ToShortDateString()} г.");

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                "на № ", 
                letter.RequestDocument == null ? string.Empty : $"{letter.RequestDocument.RegNumber}", 
                "от", 
                letter.RequestDocument == null ? string.Empty : $"{letter.RequestDocument.Date.ToShortDateString()} г.");

            docWriter.EndTable();
            docWriter.EndTableCell();

            var recipient = letter.RecipientEmployee;
            docWriter.PrintTableCell("Получатель: " + Environment.NewLine + $"{recipient.Position} {recipient.Company.Form.ShortName} \"{recipient.Company.ShortName}\"" + Environment.NewLine + $"{recipient.Person}");

            docWriter.EndTableRow();

            docWriter.PrintTableRow(docWriter.CreateTableCellProperties(), docWriter.CreateTableRowProperties(), docWriter.CreateParagraphProperties(), docWriter.CreateFont(),
                $"{letter.Comment}", string.Empty);

            docWriter.EndTable();

            #endregion

            #region Print Text Before Table

            docWriter.PrintParagraph(string.Empty);

            var paraFormat = docWriter.CreateParagraphProperties();
            paraFormat.Alignment = ParagraphAlignment.Center;
            var prefix = letter.RecipientEmployee.Person.IsMan ? "Уважаемый" : "Уважаемая";
            docWriter.PrintParagraph($"{prefix} {letter.RecipientEmployee.Person.Name} {letter.RecipientEmployee.Person.Patronymic}!", paraFormat, fontBold);

            var paraFormat1 = docWriter.CreateParagraphProperties();
            //paraFormat1.LeftIndent = 12;
            paraFormat1.Alignment = ParagraphAlignment.Both;
            docWriter.PrintParagraph("Some text...", paraFormat1);

            #endregion

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

            var part1 = "С уважением," + Environment.NewLine + $"{letter.SenderEmployee.Position}";
            docWriter.PrintTableCell(part1, tableCellProperties2);

            //подпись
            if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer?.Id)
            {
                var drt = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подпись", "Печать с подписью?", defaultNo:true);
                if (drt == MessageDialogResult.Yes)
                {
                    try
                    {
                        docWriter.StartTableCell(tableCellProperties2);

                        var prpr = docWriter.CreateParagraphProperties();
                        prpr.Alignment = ParagraphAlignment.Center;
                        docWriter.StartParagraph(prpr);
                        docWriter.AddInlinePicture(GetImage("sign_deev.png"));
                        docWriter.EndParagraph();

                        docWriter.EndTableCell();

                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.GetAllExceptions());
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
            var part3 = Environment.NewLine + $"{letter.SenderEmployee.Person.Name[0]}.{letter.SenderEmployee.Person.Patronymic?[0]}.{letter.SenderEmployee.Person.Surname}";
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
            writerSet.FooterWriterAllPages.AddTextRun("Исполнитель:" + Environment.NewLine + $"{letter.Author}" + Environment.NewLine + $"тел.: {letter.Author.PhoneNumber}; e-mail: {letter.Author.Email}; uetm.ru");
            writerSet.FooterWriterAllPages.AddTextRun(Environment.NewLine + $"{letter.RegNumber} от {letter.Date.ToShortDateString()} г. - стр. ");
            writerSet.FooterWriterAllPages.AddPageNumberField(PageNumberFieldFormat.Decimal);
            writerSet.FooterWriterAllPages.EndParagraph();
            writerSet.FooterWriterAllPages.Close();

            #endregion

            //docWriter.DefineSection(sp);

            docWriter.EndDocument();
            docWriter.Close();

            var dr = _messageService.ShowYesNoMessageDialog("Процесс завершен", "Формирование ТКП завершено. Открыть результат?", defaultYes:true);
            if (dr == MessageDialogResult.Yes)
                try
                {
                    System.Diagnostics.Process.Start(GetPath(letter, path));
                }
                catch (Exception e)
                {
                    _messageService.ShowOkMessageDialog("Error", e.GetAllExceptions());
                }
        }

        private string GetPath(Document document, string path)
        {
            var fileName = $"{document.RegNumber}-{DateTime.Today.ToShortDateString()}";
            fileName = fileName.ReplaceUncorrectSimbols("-").Replace('.', '-').Replace(' ', '_') + ".docx";
            return path == "" ? AppDomain.CurrentDomain.BaseDirectory + $"\\{fileName}" : path + $"\\{fileName}";            
        }

        private WordDocumentWriter GetWordDocumentWriter(Document document, string path)
        {
            WordDocumentWriter docWriter;
            try
            {
                docWriter = WordDocumentWriter.Create(GetPath(document, path));
            }
            catch (IOException e)
            {
                _messageService.ShowOkMessageDialog(e.GetType().Name, e.Message);
                return null;
            }
            docWriter.DefaultParagraphProperties.Alignment = ParagraphAlignment.Left;

            return docWriter;
        }


        private TableBorderProperties GetTableBorderProperties(WordDocumentWriter docWriter)
        {
            var borderProps = docWriter.CreateTableBorderProperties();
            borderProps.Color = Colors.Black;
            borderProps.Style = TableBorderStyle.Single;
            return borderProps;
        }

        private TableProperties GetTableProperties(WordDocumentWriter docWriter, TableBorderProperties borderProps)
        {
            var tableProps = docWriter.CreateTableProperties();
            tableProps.Alignment = ParagraphAlignment.Left;
            tableProps.BorderProperties.Color = borderProps.Color;
            tableProps.BorderProperties.Style = borderProps.Style;
            return tableProps;
        }

        #region GetImage
        private BitmapSource GetImage(string resourceName)
        {
            var uri = new Uri("pack://application:,,,/HVTApp.Services.PrintService;component/Images/" + resourceName);
            return new BitmapImage(uri);
            //var uri = new Uri(@"..\..\Images\" + resourceName, UriKind.Relative);
            //return new BitmapImage(uri);
            //return File.Exists(uri.AbsolutePath) ? new BitmapImage(uri) : null;
            //return new BitmapImage(new Uri(@"HVTApp.Services.PrintService;component/Images/" + resourceName));
        }

        #endregion GetImage
    }

}