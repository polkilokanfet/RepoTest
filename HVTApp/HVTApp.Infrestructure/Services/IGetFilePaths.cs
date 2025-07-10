using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IGetFilePaths
    {
        string GetFolderPath();

        string GetFilePath();

        /// <summary>
        /// Возвращает список путей к файлам
        /// </summary>
        /// <returns>Список путей к файлам</returns>
        IEnumerable<string> GetFilePaths();
    }
}