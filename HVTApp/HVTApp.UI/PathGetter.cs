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
        public const string OffersFolderName = "���";
        public const string CorrespondenceFolderName = "���������";
        public const string TceTempFolderName = "TceTemp";
        public const string HVTAppProjectsFolderName = "HVTAppProjects";

        /// <summary>
        /// ���� � ����� ����� ���.����������
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetPath(TechnicalRequrementsFile file)
        {
            var rootDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
            return GetPath(file.Id, rootDirectory);
        }

        /// <summary>
        /// ����� ��� ���������� �������� ����� ����������
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
        /// ���� � ����� ������ ��������
        /// </summary>
        /// <param name="directumTaskGroup"></param>
        /// <returns></returns>
        public static string GetPath(DirectumTaskGroup directumTaskGroup)
        {
            var rootDirectory = GlobalAppProperties.Actual.DirectumAttachmentsPath;
            return GetPath(directumTaskGroup.Id, rootDirectory);
        }

        /// <summary>
        /// ���� � ����� ���������
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
            return CreateDirectoryPathIfNotExists(path) 
                ? path 
                : Environment.SpecialFolder.MyDocuments.ToString();
        }

        /// <summary>
        /// ���� � �������
        /// </summary>
        /// <param name="project">������</param>
        /// <returns>���� � �������</returns>
        public static string GetPath(Project project)
        {
            //�������� ����������� �����
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath) && 
                !Directory.Exists(Properties.Settings.Default.ProjectsFolderPath))
            {
                Properties.Settings.Default.ProjectsFolderPath = string.Empty;
                Properties.Settings.Default.Save();
            }

            //���� � ����� �������
            var projectsFolderPath = string.IsNullOrEmpty(Properties.Settings.Default.ProjectsFolderPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), HVTAppProjectsFolderName)
                : Properties.Settings.Default.ProjectsFolderPath;

            var path = GetPath(project.Id, projectsFolderPath, project.Name);

            //�������� ��������������� ����� "���" � "���������"
            //CreateDirectory(Path.Combine(path, OffersFolderName));
            CreateDirectoryPathIfNotExists(Path.Combine(path, CorrespondenceFolderName));

            return path;
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

            if (!string.IsNullOrEmpty(addToFolderName))
            { 
                addToFolderName = addToFolderName.ReplaceUncorrectSimbols("_"); 
            }
            var path = Path.Combine(rootDirectory, $"{addToFolderName} {id}");
            
            //������, ���� ���������� �� ����������
            return CreateDirectoryPathIfNotExists(path)
                ? path
                : Environment.SpecialFolder.MyDocuments.ToString();
        }

        /// <summary>
        /// ������� ����������, ���� �� �� ����������.
        /// </summary>
        /// <param name="path">���� � ����������</param>
        private static bool CreateDirectoryPathIfNotExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}