using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI
{
    public static class PathGetter
    {
        public const string OffersFolderName = "ТКП";
        public const string CorrespondenceFolderName = "Переписка";
        public const string TceTempFolderName = "TceTemp";
        public const string HVTAppProjectsFolderName = "HVTAppProjects";

        /// <summary>
        /// путь к папке файла тех.требований
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetPath(TechnicalRequrementsFile file)
        {
            var rootDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
            return GetPath(file.Id, rootDirectory);
        }

        /// <summary>
        /// Папка для временного хранения файла требований
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetPathToCopyTemp(TechnicalRequrementsFile file, string reqPath = null)
        {
            var rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), TceTempFolderName);
            if (reqPath != null)
            {
                rootDirectory = Path.Combine(rootDirectory, reqPath);
            }
            return GetPath(file.Id, rootDirectory);
        }

        public static string GetPathToCopyTemp(TechnicalRequrementsTask technicalRequrementsTask)
        {
            var rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), TceTempFolderName);
            return GetPath(technicalRequrementsTask.Id, rootDirectory);
        }

        /// <summary>
        /// путь к папке задачи Директум
        /// </summary>
        /// <param name="directumTaskGroup"></param>
        /// <returns></returns>
        public static string GetPath(DirectumTaskGroup directumTaskGroup)
        {
            var rootDirectory = GlobalAppProperties.Actual.DirectumAttachmentsPath;
            return GetPath(directumTaskGroup.Id, rootDirectory);
        }

        /// <summary>
        /// путь к папке документа
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetPath(Document document)
        {
            var rootDirectory = GlobalAppProperties.Actual.IncomingRequestsPath;
            return GetPath(document.Id, rootDirectory, document.RegNumber);
        }

        public static string GetPath(Offer offer)
        {
            string path = Path.Combine(GetPath(offer.Project), OffersFolderName);
            CreateDirectory(path);
            return path;
        }

        public static string GetPath(Project project)
        {
            //путь к папке проекта
            var projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), HVTAppProjectsFolderName)
                : Properties.Settings.Default.ProjectsFolderPath;

            return GetPath(project.Id, projectsFolderPath, project.Name);
        }

        private static string GetPath(Guid guid, string rootDirectory, string addToFolderName = null)
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

            //создаём, если директории не существует
            if (!string.IsNullOrEmpty(addToFolderName))
            { 
                addToFolderName = addToFolderName.ReplaceUncorrectSimbols("_"); 
            }
            var path = Path.Combine(rootDirectory, $"{addToFolderName} {id}");
            CreateDirectory(path);

            //создание вспомогательных папок "ТКП" и "Переписка"
            CreateDirectory(Path.Combine(path, OffersFolderName));
            CreateDirectory(Path.Combine(path, CorrespondenceFolderName));

            return path;
        }

        /// <summary>
        /// Создать директорию, если ее не существует.
        /// </summary>
        /// <param name="path"></param>
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}