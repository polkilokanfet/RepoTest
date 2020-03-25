using System.IO;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public static class PathGetter
    {
        public static string GetPath(Document document)
        {
            //путь к папке проекта
            var projectsFolderPath = GlobalAppProperties.Actual.IncomingRequestsPath;

            var id = document.Id.ToString().Replace("-", string.Empty);
            if (Directory.Exists(projectsFolderPath))
            {
                var directoryInfo = new DirectoryInfo(projectsFolderPath);
                var targetDirectoryInfo = directoryInfo.GetDirectories().FirstOrDefault(x => x.Name.Contains(id));
                if (targetDirectoryInfo != null)
                {
                    return targetDirectoryInfo.FullName;
                }
            }

            var path = projectsFolderPath + $"\\{document.RegNumber}_{id}";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
        
    }
}