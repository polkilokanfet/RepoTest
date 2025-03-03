using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.Services.Storage;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.FileManagerService
{
    public class FilesStorageService : IFilesStorageService
    {
        private readonly IUnityContainer _container;
        private readonly IMessageService _messageService;

        public FilesStorageService(IUnityContainer container)
        {
            _container = container;
            _messageService = container.Resolve<IMessageService>();
        }

        public bool FileContainsInStorage(Guid fileId, string storageDirectoryPath)
        {
            try
            {
                this.FindFile(fileId, storageDirectoryPath);
            }
            catch (FileNotFoundException)
            {
                return false;
            }

            return true;
        }

        public FileInfo FindFile(Guid fileId, string storageDirectoryPath)
        {
            var fileInfos = this.FindFiles(fileId, storageDirectoryPath).ToList();

            if (fileInfos.Any() == false)
            {
                throw new FileNotFoundException();
            }

            if (fileInfos.Count > 1)
            {
                throw new FileNotSingleFoundException();
            }

            return fileInfos.Single();
        }

        public IEnumerable<FileInfo> FindFiles(Guid fileId, string storageDirectoryPath)
        {
            var storageDirectory = new DirectoryInfo(storageDirectoryPath);
            FileInfo[] filesInDir;
            try
            {
                filesInDir = storageDirectory.GetFiles($"*{fileId}*.*");
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException);
                throw;
            }

            return filesInDir;
        }


        #region CopyFileFromStorage

        public string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null, bool showTargetDirectory = true)
        {
            var targetDirectoryPath = this.GetDirectoryPath();
            return string.IsNullOrEmpty(targetDirectoryPath) 
                ? string.Empty 
                : this.CopyFileFromStorage(fileId, storageDirectoryPath, targetDirectoryPath, addToFileName, showTargetDirectory);
        }

        public string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string targetDirectoryPath, string addToFileName = null, bool showTargetDirectory = true, bool createDirectoryIfNotExists = false)
        {
            //проверка наличия файла
            FileInfo fileInfo;
            try
            {
                fileInfo = FindFile(fileId, storageDirectoryPath);
            }
            catch (FileNotFoundException)
            {
                _messageService.Message("Предупреждение", "Файл не найден в хранилище!");
                return string.Empty;
            }
            catch (FileNotSingleFoundException)
            {
                _messageService.Message("Предупреждение", "Файлов больше одного!");
                return string.Empty;
            }
            catch (IOException ioException)
            {
                _messageService.Message(ioException.GetType().ToString(), ioException.PrintAllExceptions());
                return string.Empty;
            }
            catch (Exception e)
            {
                _messageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
                return string.Empty;
            }

            if (createDirectoryIfNotExists && Directory.Exists(targetDirectoryPath) == false)
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }

            var fileName = $"{fileId}";
            if (!string.IsNullOrWhiteSpace(addToFileName))
            {
                fileName = $"{fileName} {addToFileName}";
            }
            var destFilePath = Path.Combine(targetDirectoryPath, $"{fileName}{fileInfo.Extension}");

            File.Copy(fileInfo.FullName, destFilePath, true);

            if (showTargetDirectory)
            {
                Process.Start(targetDirectoryPath);
            }

            return destFilePath;
        }

        public string CopyFileFromStorage(string fileName, Guid fileId, string storageDirectoryPath, string targetDirectoryPath)
        {
            //проверка наличия файла
            FileInfo fileInfo;
            try
            {
                fileInfo = FindFile(fileId, storageDirectoryPath);
            }
            catch (FileNotFoundException)
            {
                _messageService.Message("Предупреждение", "Файл не найден в хранилище!");
                return string.Empty;
            }
            catch (FileNotSingleFoundException)
            {
                _messageService.Message("Предупреждение", "Файлов больше одного!");
                return string.Empty;
            }
            catch (IOException ioException)
            {
                _messageService.Message(ioException.GetType().ToString(), ioException.PrintAllExceptions());
                return string.Empty;
            }
            catch (Exception e)
            {
                _messageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
                return string.Empty;
            }

            if (Directory.Exists(targetDirectoryPath) == false)
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }

            fileName = fileName.ReplaceUncorrectSimbols();
            var destFilePath = Path.Combine(targetDirectoryPath, $"{fileName}{fileInfo.Extension}");

            File.Copy(fileInfo.FullName, destFilePath, true);

            return destFilePath;
        }

        public void CopyFilesFromStorage(IEnumerable<IFileStorage> files, string storageDirectoryPath, bool addName = true, bool showTargetDirectory = true)
        {
            var targetDirectory = this.GetDirectoryPath();
            if (string.IsNullOrEmpty(targetDirectory))
                return;

            foreach (var file in files)
            {
                string addToFileName = addName 
                    ? $"{file.Name.ReplaceUncorrectSimbols().LimitLength()}"
                    : string.Empty;

                CopyFileFromStorage(file.Id, storageDirectoryPath, targetDirectory, addToFileName, false);
            }

            if (showTargetDirectory)
            {
                Process.Start(targetDirectory);
            }
        }

        #endregion


        public string GetDirectoryPath()
        {
            using (var fdb = new FolderBrowserDialog())
            {
                var result = fdb.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                {
                    if (Directory.Exists(fdb.SelectedPath) == false)
                        Directory.CreateDirectory(fdb.SelectedPath);

                    return fdb.SelectedPath;
                }

                return string.Empty;
            }
        }

        public void OpenFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null)
        {
            string filePath;
            var targetDirectoryPath = Path.GetTempPath();

            try
            {
                filePath = CopyFileFromStorage(fileId, storageDirectoryPath, targetDirectoryPath, addToFileName.LimitLength(10).ReplaceUncorrectSimbols(), showTargetDirectory: false);

            }
            catch (UnauthorizedAccessException)
            {
                filePath = CopyFileFromStorage(fileId, storageDirectoryPath, targetDirectoryPath, Guid.NewGuid().ToString(), showTargetDirectory: false);
            }

            if (string.IsNullOrEmpty(filePath)) return;

            try
            {
                Process.Start(filePath);
            }
            catch (Exception e)
            {
                _messageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
            }
        }

        public bool CopyDirectory(string sourcePath, string destinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);

            return true;
        }

        /// <summary>
        /// Получить архив
        /// </summary>
        /// <param name="files">Файлы в архиве</param>
        /// <param name="zipFileName">Имя архива</param>
        /// <param name="destinationDirectoryPath">Путь к директории для архива</param>
        /// <returns>Полный путь к полученному архиву</returns>
        public string GetZip(IEnumerable<IFileCopyInfo> files, string zipFileName, string destinationDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(destinationDirectoryPath))
                throw new ArgumentException(nameof(destinationDirectoryPath));

            //путь ко временной директории
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            //копирование файлов во временные папки
            foreach (var file in files)
            {
                var tempFilePath = Path.Combine(tempDirectory, file.DestinationDirectoryPath);
                Directory.CreateDirectory(tempFilePath);
                this.CopyFileFromStorage(file.File.Id, file.SourcePath, tempFilePath, null, false);
            }

            //получение архива
            var zipFilePath = Path.Combine(destinationDirectoryPath, $"{zipFileName}.zip");
            ZipFile.CreateFromDirectory(tempDirectory, zipFilePath);
            Directory.Delete(tempDirectory, true);
            return zipFilePath;
        }

        public void AddFilesToZip(string zipPath, string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return;
            }

            using (var zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                }
            }
        }

        public bool LoadFileToStorage(string storagePath, string filePath, Guid fileId, bool overwrite = false)
        {
            try
            {
                File.Copy(filePath, $"{storagePath}\\{fileId}{Path.GetExtension(filePath)}", overwrite);
            }
            catch (Exception e)
            {
                _messageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
            }

            return true;
        }

        public bool LoadFileToStorage(string storagePath, Guid fileId, bool overwrite = false)
        {
            var filePath = _container.Resolve<IGetFilePaths>().GetFilePath();
            if (filePath == null) return false;
            return this.LoadFileToStorage(storagePath, filePath, fileId, overwrite);
        }

        public bool RemoveFiles(string storagePath, Guid fileId)
        {
            var files = this.FindFiles(fileId, storagePath);
            foreach (var file in files)
            {
                
                File.Delete(file.FullName);
            }
            return true;
        }
    }
}