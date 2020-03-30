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
        public static string GetPath(Document document)
        {
            //путь к папке проекта
            var motherFolder = GlobalAppProperties.Actual.IncomingRequestsPath;
            return GetPath(document.Id, motherFolder, document.RegNumber);
        }

        public static string GetPath(Project project)
        {
            //путь к папке проекта
            var projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "HVTAppProjects")
                : Properties.Settings.Default.ProjectsFolderPath;

            return GetPath(project.Id, projectsFolderPath, project.Name);
        }

        private static string GetPath(Guid guid, string motherFolder, string addToFolderName)
        {
            var id = guid.ToString().Replace("-", string.Empty);
            if (Directory.Exists(motherFolder))
            {
                var directoryInfo = new DirectoryInfo(motherFolder);
                var targetDirectoryInfo = directoryInfo.GetDirectories().FirstOrDefault(x => x.Name.Contains(id));
                if (targetDirectoryInfo != null)
                {
                    return targetDirectoryInfo.FullName;
                }
            }

            var path = Path.Combine(motherFolder, $"{addToFolderName.ReplaceUncorrectSimbols("_")} {id}");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}