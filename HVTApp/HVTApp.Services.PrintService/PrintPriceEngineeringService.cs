﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.Services.Storage;
using HVTApp.Model.POCOs;
using HVTApp.Services.PrintService.Extensions;
using Infragistics.Documents.Word;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PrintService
{
    public class PrintPriceEngineeringService : PrintServiceBase, IPrintPriceEngineering
    {
        public PrintPriceEngineeringService(IUnityContainer container) : base(container)
        {
        }

        public void PrintPriceEngineeringTasks(Guid id)
        {
            this.PrintPriceEngineeringTasksInformation(Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(id));
        }

        public string PrintHistoryPriceEngineeringTask(Guid id, string destDirectory = null, string fileName = null)
        {
            destDirectory = destDirectory ?? Path.GetTempPath();
            fileName = fileName ?? Guid.NewGuid().ToString();
            string documentPath = Path.Combine(destDirectory, $"{fileName}.docx");

            WordDocumentWriter docWriter = WordDocumentWriter.Create(documentPath);
            docWriter.StartDocument();

            docWriter.PrintParagraph("Информация о технической проработке задачи в УП ВВА.");
            docWriter.PrintParagraph($"Момент выгрузки справки: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");

            this.PrintPriceEngineeringTaskInformation(Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(id), docWriter);

            docWriter.EndDocument();
            docWriter.Close();
            //System.Diagnostics.Process.Start(documentPath);
            return documentPath;
        }


        private void PrintPriceEngineeringTasksInformation(PriceEngineeringTasks priceEngineeringTasks)
        {
            //string offerDocumentPath = Path.GetTempPath() + "\\TestPriceEngineeringTasks.docx";
            string offerDocumentPath = @"C:\Users\kosol\Desktop\Новая папка\TestPriceEngineeringTasks1.docx";
            
            WordDocumentWriter docWriter = WordDocumentWriter.Create(offerDocumentPath);
            docWriter.StartDocument();

            docWriter.PrintParagraph("Информация о технической проработке задач в УП ВВА.");
            foreach (var childTask in priceEngineeringTasks.ChildPriceEngineeringTasks)
            {
                this.PrintPriceEngineeringTaskInformation(childTask, docWriter);
            }

            docWriter.EndDocument();
            docWriter.Close();
            System.Diagnostics.Process.Start(offerDocumentPath);
        }

        private void PrintPriceEngineeringTaskInformation(PriceEngineeringTask priceEngineeringTask, WordDocumentWriter docWriter)
        {
            Font fontBold = docWriter.CreateFont();
            fontBold.Bold = true;

            docWriter.PrintParagraph("");
            if (priceEngineeringTask.ParentPriceEngineeringTasksId.HasValue)
            {
                var tsks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTask.ParentPriceEngineeringTasksId.Value);
                docWriter.PrintParagraph($"Менеджер: {tsks.UserManager.Employee.Person}");
                docWriter.PrintParagraph($"Объект: {tsks.ChildPriceEngineeringTasks.First().SalesUnits.First().Facility}");
                docWriter.PrintParagraph($"Продукт: {priceEngineeringTask.ProductBlock}");
                docWriter.PrintParagraph($"Количество: {tsks.ChildPriceEngineeringTasks.First().SalesUnits.Count}");
                docWriter.PrintParagraph($"Номер задачи в ТСЕ: {tsks.TceNumber}");
            }

            docWriter.PrintParagraph("----------");
            docWriter.PrintParagraph($"Id задачи в УП ВВА: {priceEngineeringTask.Number:D4}");
            docWriter.PrintParagraph($"Блок: {priceEngineeringTask.ProductBlock}", null, fontBold);
            docWriter.PrintParagraph($"Исполнитель от ОГК: {priceEngineeringTask.UserConstructor?.Employee.Person}");

            var files = priceEngineeringTask.GetFileCopyInfoEntities().ToList();
            var colorTableHeader = Colors.AliceBlue;

            var tableBorderProperties = GetTableBorderProperties(docWriter);
            var tableRowProperties = docWriter.CreateTableRowProperties();
            var tableCellProperties = docWriter.CreateTableCellProperties();

            tableCellProperties.BorderProperties = tableBorderProperties;

            var tableProperties = GetTableProperties(docWriter, tableBorderProperties);
            tableProperties.Alignment = ParagraphAlignment.Left;

            var paragraphProps = docWriter.CreateParagraphProperties();
            paragraphProps.Alignment = ParagraphAlignment.Left;

            #region Таблица файлов ТЗ

            var filesTechnicalSpecification = files
                .Where(fileCopyInfo => fileCopyInfo.FileCopyInfoType == FileCopyInfoType.TechnicalSpecification)
                .Where(fileCopyInfo => fileCopyInfo.File.IsActual)
                .ToList();

            if (filesTechnicalSpecification.Any())
            {
                docWriter.PrintParagraph("Файлы технического задания от ОП ВВА:");

                docWriter.StartTable(3, tableProperties);

                tableRowProperties.IsHeaderRow = true;
                tableCellProperties.BackColor = colorTableHeader;

                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                    "Дата", "Название файла", $"Id файла (путь в архиве: [{filesTechnicalSpecification.First().DestinationDirectoryPath}])");

                // Reset the cell properties, so that the cell properties are different from the header cells.
                tableCellProperties.Reset();
                tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
                // Reset the row properties
                tableRowProperties.Reset();

                foreach (var fileCopyInfo in filesTechnicalSpecification.OrderBy(x => x.File.CreationMoment))
                {
                    docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, null,
                        $"{fileCopyInfo.File.CreationMoment.ToShortDateString()} {fileCopyInfo.File.CreationMoment.ToShortTimeString()}", $"{fileCopyInfo.File.Name}", $"{fileCopyInfo.File.Id}");
                }

                docWriter.EndTable();
            }

            #endregion

            #region Таблица файлов ОГК

            var filesDesignDepartmentAnswer = files
                .Where(fileCopyInfo => fileCopyInfo.FileCopyInfoType == FileCopyInfoType.DesignDepartmentAnswer)
                .Where(fileCopyInfo => fileCopyInfo.File.IsActual)
                .ToList();

            if (filesDesignDepartmentAnswer.Any())
            {
                docWriter.PrintParagraph("Файлы-ответы ОГК НВВА:");

                tableCellProperties.BorderProperties = tableBorderProperties;

                docWriter.StartTable(3, tableProperties);

                tableRowProperties.IsHeaderRow = true;
                tableCellProperties.BackColor = colorTableHeader;
                paragraphProps.Alignment = ParagraphAlignment.Left;

                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                    "Дата", "Название файла", $"Id файла (в папке \"{filesDesignDepartmentAnswer.First().DestinationDirectoryPath}\")");

                // Reset the cell properties, so that the cell properties are different from the header cells.
                tableCellProperties.Reset();
                tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
                // Reset the row properties
                tableRowProperties.Reset();

                foreach (var file in filesDesignDepartmentAnswer.Select(x => x.File).OrderBy(x => x.CreationMoment))
                {
                    docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, null,
                        $"{file.CreationMoment.ToShortDateString()}", $"{file.Name}", $"{file.Id}");
                }

                docWriter.EndTable();
            }

            #endregion

            #region Таблица стракчакостов

            docWriter.PrintParagraph("Стракчакосты:");

            tableCellProperties.BorderProperties = tableBorderProperties;

            tableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(3, tableProperties);

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            paragraphProps.Alignment = ParagraphAlignment.Left;

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                "Название", "Кол.", "На блок", "Scc в УП ВВА", "Scc в ТСЕ");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            var scc1 = priceEngineeringTask.StructureCostVersions
                .FirstOrDefault(x =>
                    x.PriceEngineeringTaskId == priceEngineeringTask.Id &&
                    x.OriginalStructureCostNumber == priceEngineeringTask.ProductBlock.StructureCostNumber);
            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, null,
                $"{priceEngineeringTask.ProductBlock}", "1", "+", $"{priceEngineeringTask.ProductBlock.StructureCostNumber}", $"V{scc1?.Version:D2}");

            foreach (var pba in priceEngineeringTask.ProductBlocksAdded.Where(x => x.IsRemoved == false))
            {
                var onBlock = pba.IsOnBlock ? "+" : "-";
                var scc = pba.StructureCostVersions
                    .FirstOrDefault(x => 
                        x.PriceEngineeringTaskProductBlockAddedId == pba.Id &&
                        x.OriginalStructureCostNumber == pba.ProductBlock.StructureCostNumber);
                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, null,
                    $"{pba.ProductBlock}", $"{pba.Amount}", onBlock, $"{pba.ProductBlock.StructureCostNumber}", $"V{scc?.Version:D2}");
            }

            docWriter.EndTable();

            #endregion

            #region Таблица событий

            docWriter.PrintParagraph("События проработки задачи:");

            tableCellProperties.BorderProperties = tableBorderProperties;

            tableProperties.Alignment = ParagraphAlignment.Left;
            docWriter.StartTable(3, tableProperties);

            tableRowProperties.IsHeaderRow = true;
            tableCellProperties.BackColor = colorTableHeader;
            paragraphProps.Alignment = ParagraphAlignment.Left;

            docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, fontBold,
                "Момент", "Событие");

            // Reset the cell properties, so that the cell properties are different from the header cells.
            tableCellProperties.Reset();
            tableCellProperties.VerticalAlignment = TableCellVerticalAlignment.Top;
            // Reset the row properties
            tableRowProperties.Reset();

            foreach (var message in priceEngineeringTask.GetMessages())
            {
                var content = message.Message;
                if (message is PriceEngineeringTaskMessage msg)
                {
                    content = $"Сообщение от {msg.Author.Employee.Person}: {msg.Message}";
                }

                docWriter.PrintTableRow(tableCellProperties, tableRowProperties, paragraphProps, null,
                    $"{message.Moment.ToShortDateString()} {message.Moment.ToShortTimeString()}", content);
            }

            docWriter.EndTable();

            #endregion

            docWriter.PrintParagraph("");
            if (priceEngineeringTask.ChildPriceEngineeringTasks.Any())
            {
                docWriter.PrintParagraph("Вложенные блоки:");
                foreach (var childTask in priceEngineeringTask.ChildPriceEngineeringTasks)
                {
                    this.PrintPriceEngineeringTaskInformation(childTask, docWriter);
                }
            }
        }

        protected override string GetFullPath(Document document, string path)
        {
            throw new System.NotImplementedException();
        }

        protected override void PrintBody(Document document, WordDocumentWriter docWriter)
        {
            throw new System.NotImplementedException();
        }
    }
}