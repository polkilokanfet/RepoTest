using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;

namespace HVTApp.Infrastructure
{
    //public class FilesNotFoundException : Exception { }
    public class FileNotSingleFoundException : Exception { }

    public static class FilesStorage
    {
        /// <summary>
        /// Пойск файла в директории по Id
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="storageDirectoryPath">Путь к директории</param>
        /// <returns></returns>
        public static FileInfo FindFile(Guid fileId, string storageDirectoryPath)
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

        /// <summary>
        /// Копирование файла из хранилища.
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="messageService"></param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="targetDirectoryPath">Куда копировать файл</param>
        /// <param name="addToFileName">Что добавить к имени файла</param>
        /// <param name="showTargetDirectory">Открыть после копирования целевую папку?</param>
        /// <returns>Путь к скопированному файлу</returns>
        public static string CopyFileFromStorage(Guid fileId, IMessageService messageService, string storageDirectoryPath, string targetDirectoryPath = null, string addToFileName = null, bool showTargetDirectory = true)
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = FindFile(fileId, storageDirectoryPath);
            }
            catch (FileNotFoundException)
            {
                messageService.ShowOkMessageDialog("Предупреждение", "Файл не найден в хранилище!");
                return string.Empty;
            }
            catch (FileNotSingleFoundException)
            {
                messageService.ShowOkMessageDialog("Предупреждение", "Файлов больше одного!");
                return string.Empty;
            }
            catch (IOException ioException)
            {
                messageService.ShowOkMessageDialog(ioException.GetType().ToString(), ioException.PrintAllExceptions());
                return string.Empty;
            }
            catch (Exception e)
            {
                messageService.ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                return string.Empty;
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
                        return string.Empty;
                    }
                }
            }


            var fileName = $"{fileId}";
            if (!string.IsNullOrWhiteSpace(addToFileName))
            {
                fileName = $"{fileName} {addToFileName}";
            }
            var destFileName = Path.Combine(targetDirectoryPath, $"{fileName}{fileInfo.Extension}");
            File.Copy(fileInfo.FullName, destFileName, true);

            if (showTargetDirectory)
            {
                Process.Start("explorer.exe", targetDirectoryPath);
            }

            return destFileName;
        }

        /// <summary>
        /// Открыть файл из хранилища.
        /// </summary>
        /// <param name="fileId">Id файла</param>
        /// <param name="messageService"></param>
        /// <param name="storageDirectoryPath">Путь к файловому хранилищу</param>
        /// <param name="addToFileName">Что добавить к имени файла</param>
        public static void OpenFileFromStorage(Guid fileId, IMessageService messageService, string storageDirectoryPath, string addToFileName = null)
        {
            var tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HVTAppTemp");
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            var filePath = CopyFileFromStorage(fileId, messageService, storageDirectoryPath, tempPath, addToFileName, showTargetDirectory: false);

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    Process.Start(filePath);
                }
                catch (Exception e)
                {
                    messageService.ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                }
            }
        }

        /// <summary>
        /// Полная копия директории со всеми файлами
        /// </summary>
        /// <param name="sourcePath">Путь к копируемой директории</param>
        /// <param name="destinationPath">Путь к директории, куда нужно скопировать</param>
        /// <returns>Успешно ли прошло копирование</returns>
        public static bool CopyDirectory(string sourcePath, string destinationPath)
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