using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintPriceEngineering
    {
        void PrintPriceEngineeringTasks(Guid id);

        /// <summary>
        /// Распечатать в файл историю технической проработки задачи в УП ВВА
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <param name="destDirectory">Целевая папка</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        string PrintHistoryPriceEngineeringTask(Guid id, string destDirectory = null, string fileName = null);
    }
}