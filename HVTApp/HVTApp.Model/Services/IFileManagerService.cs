using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IFileManagerService
    {
        /// <summary>
        /// Сохранить путь к папке с проектами
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns></returns>
        bool SaveDefaultProjectsFolderPath(string path);

        /// <summary>
        /// Путь к папке проекта
        /// </summary>
        /// <param name="project">Проект</param>
        /// <returns>Путь к папке проекта</returns>
        string GetPath(Project project);

        /// <summary>
        /// Путь к папке "Переписка" в папке проекта
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        string GetProjectCorrespondenceFolderName(Project project);

        string GetPath(Offer offer);

        /// <summary>
        /// путь к папке документа
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        string GetPath(Document document);

        /// <summary>
        /// Путь к директории
        /// </summary>
        /// <param name="guid">Id директории</param>
        /// <param name="rootDirectory">корневая папка</param>
        /// <param name="addToFolderName">добавить к имени директории</param>
        /// <returns></returns>
        string GetPath(Guid guid, string rootDirectory, string addToFolderName = null);

        /// <summary>
        /// Стандартное хранилище вложений
        /// </summary>
        /// <returns></returns>
        string GetDefaultStoragePath();

        string GetLettersDefaultStoragePath();

        /// <summary>
        /// Создать директорию, если ее не существует.
        /// </summary>
        /// <param name="path">Путь к директории</param>
        bool CreateDirectoryPathIfNotExists(string path);
    }
}