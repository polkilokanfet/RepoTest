using System;
using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IGetInformationFromExcelFileService
    {
        /// <summary>
        /// Подтягивает цены из файла R3
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Dictionary<string, double> GetCostsDictionaryFromR3File(string path);

        /// <summary>
        /// Подтягивает цены из файла расчёта ПЗ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Dictionary<string, double> GetCostsDictionaryFromCalculationFile(string path);

        /// <summary>
        /// Подтягивает даты комплектации из таблички отдела закупок
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Dictionary<string, Dictionary<int, DateTime>> GetPickingDatesFromFile(string path);
    }
}