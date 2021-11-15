using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.PathService
{
    public class FileManagerService1 : IFileManagerService
    {
        public string OffersFolderName { get; } = "ТКП";
        public string CorrespondenceFolderName { get; } = "Переписка";
        public string TceTempFolderName { get; } = "TceTemp";
        public string HvtAppProjectsFolderName { get; } = "HVTAppProjects";

        /// <summary>
        /// Сохранить путь к папке с проектами
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns></returns>
        public bool SaveDefaultProjectsFolderPath(string path)
        {
            Properties.Settings.Default.ProjectsFolderPath = path;
            Properties.Settings.Default.Save();
            return true;
        }

        /// <summary>
        /// путь к папке документа
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public string GetPath(Document document)
        {
            var rootDirectory = GlobalAppProperties.Actual.IncomingRequestsPath;
            return GetPath(document.Id, rootDirectory, document.RegNumber);
        }

        public string GetPath(Offer offer)
        {
            string path = Path.Combine(GetPath(offer.Project), OffersFolderName);
            return CreateDirectoryPathIfNotExists(path)
                ? path
                : Environment.SpecialFolder.MyDocuments.ToString();
        }

        /// <summary>
        /// Путь к проекту
        /// </summary>
        /// <param name="project">Проект</param>
        /// <returns>Путь к проекту</returns>
        public string GetPath(Project project)
        {
            //проверка доступности папки
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath) &&
                !Directory.Exists(Properties.Settings.Default.ProjectsFolderPath))
            {
                Properties.Settings.Default.ProjectsFolderPath = string.Empty;
                Properties.Settings.Default.Save();
            }

            //путь к папке проекта
            var projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), HvtAppProjectsFolderName)
                : Properties.Settings.Default.ProjectsFolderPath;

            var path = GetPath(project.Id, projectsFolderPath, project.Name);

            //создание вспомогательных папок "ТКП" и "Переписка"
            //CreateDirectory(Path.Combine(path, OffersFolderName));
            CreateDirectoryPathIfNotExists(Path.Combine(path, CorrespondenceFolderName));

            return path;
        }

        public string GetPath(Guid guid, string rootDirectory, string addToFolderName = null)
        {
            var id = guid.ToString().Replace("-", string.Empty);

            //поиск существующей директории
            if (Directory.Exists(rootDirectory))
            {
                var targetDirectory = Directory.GetDirectories(rootDirectory, $"*{id}*", SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (targetDirectory != null)
                {
                    return targetDirectory;
                }
            }

            //создаём требуемую директорию
            if (!string.IsNullOrEmpty(addToFolderName))
            {
                addToFolderName = addToFolderName.ReplaceUncorrectSimbols("_");
            }
            var path = Path.Combine(rootDirectory, $"{addToFolderName.GetFirstSimbols(30)} {id}");

            //создаём, если директории не существует
            return CreateDirectoryPathIfNotExists(path)
                ? path
                : Environment.SpecialFolder.MyDocuments.ToString();
        }

        /// <summary>
        /// Создать директорию, если ее не существует.
        /// </summary>
        /// <param name="path">Путь к директории</param>
        public bool CreateDirectoryPathIfNotExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
