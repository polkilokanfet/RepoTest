using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Services;

namespace HVTApp.UI
{
    public static class FilesStorage
    {
        public static bool CopyFileFromStorage(Guid fileId, IMessageService messageService, string storageDirectoryPath, string targetDirectoryPath = null, string addToFileName = null, bool showTargetDirectory = true)
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
                return false;
            }

            if (!filesInDir.Any())
            {
                messageService.ShowOkMessageDialog("Предупреждение", "Файл не найден в хранилище!");
                return false;
            }

            if (filesInDir.Length > 1)
            {
                messageService.ShowOkMessageDialog("Предупреждение", "Файлов больше одного!");
                return false;
            }

            //выбор папки назначения
            if (targetDirectoryPath == null)
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    var result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        targetDirectoryPath = folderBrowserDialog.SelectedPath;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            foreach (var fileInDir in filesInDir)
            {
                var fileName = $"{fileId}";
                if (!string.IsNullOrWhiteSpace(addToFileName))
                    fileName = $"{fileName} {addToFileName}";
                var path = Path.Combine(targetDirectoryPath, $"{fileName}{fileInDir.Extension}");
                File.Copy(fileInDir.FullName, path, true);
            }

            if(showTargetDirectory)
                Process.Start("explorer.exe", targetDirectoryPath);

            return true;
        }
    }
}