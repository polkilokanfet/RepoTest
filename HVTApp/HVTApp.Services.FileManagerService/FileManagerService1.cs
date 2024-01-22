using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.FileManagerService
{
    public class FileManagerService1 : IFileManagerService
    {
        /// <summary>
        /// Путь к папке с проектами
        /// </summary>
        private string _projectsFolderPath;

        private string OffersFolderName { get; } = "ТКП";
        private string HvtAppProjectsFolderName { get; } = "HVTAppProjects";
        private string CorrespondenceFolderName { get; } = "Переписка";

        public FileManagerService1()
        {
            //путь к папке проектов
            _projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                //если есть сохраненния папка
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), HvtAppProjectsFolderName)
                //стандартная папка
                : Properties.Settings.Default.ProjectsFolderPath;

            //создаем папку для проектов, если она еще не создана
            CreateDirectoryPathIfNotExists(_projectsFolderPath);
        }

        public bool SaveDefaultProjectsFolderPath(string path)
        {
            CreateDirectoryPathIfNotExists(path);
            Properties.Settings.Default.ProjectsFolderPath = path;
            Properties.Settings.Default.Save();
            _projectsFolderPath = path;
            return true;
        }

        public string GetPath(Document document)
        {
            var rootDirectory = GlobalAppProperties.Actual.IncomingRequestsPath;
            return GetPath(document.Id, rootDirectory, document.RegNumber);
        }

        public string GetPath(Project project)
        {
            return GetPath(project.Id, _projectsFolderPath, project.Name);
        }

        public string GetProjectCorrespondenceFolderName(Project project)
        {
            string path = GetPath(project);
            var result = Path.Combine(path, CorrespondenceFolderName);
            CreateDirectoryPathIfNotExists(result);
            return result;
        }

        public string GetPath(Offer offer)
        {
            string path = Path.Combine(GetPath(offer.Project), OffersFolderName);
            return CreateDirectoryPathIfNotExists(path)
                ? path
                : Environment.SpecialFolder.MyDocuments.ToString();
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
