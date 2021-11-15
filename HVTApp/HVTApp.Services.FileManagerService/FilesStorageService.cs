using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Services;

namespace HVTApp.Services.FileManagerService
{
    public class FilesStorageService : IFilesStorageService
    {
        private readonly IMessageService _messageService;

        public FilesStorageService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public FileInfo FindFile(Guid fileId, string storageDirectoryPath)
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

            if (!filesInDir.Any())
            {
                throw new FileNotFoundException();
            }

            if (filesInDir.Length > 1)
            {
                throw new FileNotSingleFoundException();
            }

            return filesInDir.Single();
        }

        public string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null, bool showTargetDirectory = true)
        {
            string targetDirectoryPath;
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                var result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    targetDirectoryPath = folderBrowserDialog.SelectedPath;
                }
                else
                {
                    return string.Empty;
                }
            }

            return this.CopyFileFromStorage(fileId, storageDirectoryPath, targetDirectoryPath, addToFileName, showTargetDirectory);
        }

        public string CopyFileFromStorage(Guid fileId, string storageDirectoryPath, string targetDirectoryPath, string addToFileName = null, bool showTargetDirectory = true)
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = FindFile(fileId, storageDirectoryPath);
            }
            catch (FileNotFoundException)
            {
                _messageService.ShowOkMessageDialog("Предупреждение", "Файл не найден в хранилище!");
                return string.Empty;
            }
            catch (FileNotSingleFoundException)
            {
                _messageService.ShowOkMessageDialog("Предупреждение", "Файлов больше одного!");
                return string.Empty;
            }
            catch (IOException ioException)
            {
                _messageService.ShowOkMessageDialog(ioException.GetType().ToString(), ioException.PrintAllExceptions());
                return string.Empty;
            }
            catch (Exception e)
            {
                _messageService.ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                return string.Empty;
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

        public void OpenFileFromStorage(Guid fileId, string storageDirectoryPath, string addToFileName = null)
        {
            var filePath = CopyFileFromStorage(fileId, storageDirectoryPath, Path.GetTempPath(), addToFileName, showTargetDirectory: false);

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    Process.Start(filePath);
                }
                catch (Exception e)
                {
                    _messageService.ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                }
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
    }
}