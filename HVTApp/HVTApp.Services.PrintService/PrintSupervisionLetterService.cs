using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.PrintService.Extansions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintSupervisionLetterService : PrintServiceBase, IPrintSupervisionLetterService
    {
        public PrintSupervisionLetterService(IUnityContainer container) : base(container) { }

        public void PrintSupervisionLetter(IEnumerable<Supervision> supervisions1, Document letter, string path = "")
        {
            var supervisions = supervisions1.OrderBy(x => x.SalesUnit.SerialNumber).ToList();
            //полный путь к файлу (с именем файла)
            var fullPath = letter.GetPath(path);

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
                $"О шеф-монтаже на объектах: {supervisions.Select(x => x.SalesUnit.Facility).ToStringEnum(", ")}", string.Empty);

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
            var specification = supervisions.First().SalesUnit.Specification;
            docWriter.PrintParagraph($"В соответствии с договором 0401-21-0050 от 01.07.2021 г., прошу Вас организовать выезд специалиста для проведения шеф-монтажных работ (согласно спецификации {specification?.Number} к договору {specification?.Contract.Number} от {specification?.Contract.Date.ToShortDateString()}) на следующем оборудовании:", paraFormat1);

            #endregion

            #region Print Main Table

            var colorTableHeader = Colors.AliceBlue;

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            var tableProperties = GetTableProperties(docWriter, tableBorderProperties);
            tableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(7, tableProperties);

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold, 
                "№", "Тип оборудования", "Обозначение оборудования", "зав.№", "Заказ клиента", "Сервисный заказ", "Требуемая дата монтажа");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            ParagraphProperties parPropRight = docWriter.CreateParagraphProperties();
            parPropRight.Alignment = ParagraphAlignment.Right;

            int num = 0;
            foreach (var supervisionsGroupsByFacility in supervisions.GroupBy(x => x.SalesUnit.Facility))
            {

                //Название объекта
                docWriter.StartTableRow();

                tableCellProperties.ColumnSpan = 7;
                var facility = supervisionsGroupsByFacility.Key;
                var address = facility.Address?.ToString() ?? facility.GetRegion().ToString();
                docWriter.PrintTableCell($"{facility} ({address})", tableCellProperties, font: fontBold); //объект

                docWriter.EndTableRow();

                tableCellProperties.ColumnSpan = 1;
                foreach (var supervision in supervisionsGroupsByFacility)
                {
                    docWriter.StartTableRow();

                    num++;
                    docWriter.PrintTableCell(num.ToString(), tableCellProperties); //номер строки
                    docWriter.PrintTableCell(supervision.SalesUnit.Product.ProductType?.Name, tableCellProperties);
                    //тип оборудования
                    docWriter.PrintTableCell(supervision.SalesUnit.Product.Designation, tableCellProperties); //обозначение
                    docWriter.PrintTableCell($"{supervision.SalesUnit.SerialNumber}", tableCellProperties);
                    docWriter.PrintTableCell($"{supervision.ClientOrderNumber}", tableCellProperties);
                    docWriter.PrintTableCell($"{supervision.ServiceOrderNumber}", tableCellProperties);
                    var dateReq = supervision.DateRequired?.ToShortDateString() ?? "по согл.";
                    docWriter.PrintTableCell(dateReq, tableCellProperties);

                    docWriter.EndTableRow();
                }
            }
            docWriter.EndTable();

            #endregion

            #region Print Text After Table

            docWriter.PrintParagraph("Оплата стоимости шеф-монтажных работ будет произведена в соответствии с договором 0401-21-0050 от 01.07.2021 г.", paraFormat1);
            var manager = supervisions.First().SalesUnit.Project.Manager.Employee;
            docWriter.PrintParagraph($"Ответственный менеджер: {manager.Person}, тел.: {manager.PhoneNumber}; e-mail: {manager.Email}", paraFormat1);
            docWriter.PrintParagraph("Приложение: письмо Заказчика.", paraFormat1);

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
                var drt = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подпись", "Печать с подписью?", defaultNo:true);
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
                        Container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
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

            OpenDocument(fullPath);
        }

    }

}