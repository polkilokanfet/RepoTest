﻿using System;
using System.IO;

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
    }
}