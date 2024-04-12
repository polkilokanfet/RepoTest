using System;
using System.Collections.Generic;
using System.IO;
using HVTApp.Infrastructure.Services.Storage;

namespace HVTApp.Model.Services
{
    public interface IFilesStorageService
    {
        /// <summary>
        /// Пойск файла в директории по Id
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="storageDirectoryPath">Путь к директории</param>
        /// <returns></returns>
        FileInfo FindFile(Guid fileId, string storageDirectoryPath);

        /// <summary>
        /// Копирование файла из хранилища с выбором целевой папки.
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="addToFileName">Что добавить к имени файла</param>
        /// <param name="showTargetDirectory">Открыть после копирования целевую папку?</param>
        /// <returns>Путь к скопированному файлу</returns>
        string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null, bool showTargetDirectory = true);

        /// <summary>
        /// Копирование файла из хранилища в целевую папку.
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="targetDirectoryPath">Куда копировать файл</param>
        /// <param name="addToFileName">Что добавить к имени файла</param>
        /// <param name="showTargetDirectory">Открыть после копирования целевую папку?</param>
        /// <returns>Путь к скопированному файлу</returns>
        string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string targetDirectoryPath, string addToFileName = null, bool showTargetDirectory = true);

        /// <summary>
        /// Копирование файлов из хранилища в целевую папку.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="addName">Что добавить к имени файла</param>
        /// <param name="showTargetDirectory">Открыть после копирования целевую папку?</param>
        /// <returns>Путь к скопированному файлу</returns>
        void CopyFilesFromStorage(IEnumerable<IFileStorage> files, string storageDirectoryPath, bool addName = true, bool showTargetDirectory = true);

        string GetDirectoryPath();

        /// <summary>
        /// Открыть файл из хранилища.
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="addToFileName">Что добавить к имени файла</param>
        void OpenFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null);

        /// <summary>
        /// Полная копия директории со всеми файлами
        /// </summary>
        /// <param name="sourcePath">Путь к копируемой директории</param>
        /// <param name="destinationPath">Путь к директории, куда нужно скопировать</param>
        /// <returns>Успешно ли прошло копирование</returns>
        bool CopyDirectory(string sourcePath, string destinationPath);

        /// <summary>
        /// Получить архив
        /// </summary>
        /// <param name="files">Файлы в архиве</param>
        /// <param name="zipFileName">Имя архива</param>
        /// <param name="destinationDirectoryPath">Путь к директории для архива</param>
        /// <returns>Полный путь к полученному архиву</returns>
        string GetZip(IEnumerable<IFileCopyInfo> files, string zipFileName, string destinationDirectoryPath);

        void AddFilesToZip(string zipPath, string[] files);
    }
}