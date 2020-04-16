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
        public static string GetPath(DirectumTaskGroup directumTaskGroup)
        {
            //���� � ����� �������
            var rootDirectory = GlobalAppProperties.Actual.DirectumAttachmentsPath;
            return GetPath(directumTaskGroup.Id, rootDirectory);
        }

        public static string GetPath(Document document)
        {
            //���� � ����� �������
            var rootDirectory = GlobalAppProperties.Actual.IncomingRequestsPath;
            return GetPath(document.Id, rootDirectory, document.RegNumber);
        }

        public static string GetPath(Project project)
        {
            //���� � ����� �������
            var projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "HVTAppProjects")
                : Properties.Settings.Default.ProjectsFolderPath;

            return GetPath(project.Id, projectsFolderPath, project.Name);
        }

        private static string GetPath(Guid guid, string rootDirectory, string addToFolderName = null)
        {
            var id = guid.ToString().Replace("-", string.Empty);

            //����� ������������ ����������
            if (Directory.Exists(rootDirectory))
            {
                var targetDirectory = Directory.GetDirectories(rootDirectory, $"*{id}*", SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (targetDirectory != null)
                {
                    return targetDirectory;
                }
            }

            //������, ���� ���������� �� ����������
            if (!string.IsNullOrEmpty(addToFolderName))
                addToFolderName = addToFolderName.ReplaceUncorrectSimbols("_") + " ";
            var path = Path.Combine(rootDirectory, $"{addToFolderName}{id}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}