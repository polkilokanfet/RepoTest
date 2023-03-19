using System.IO;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
    public interface IFilePathContainer : IId
    {
        /// <summary>
        /// Путь к копируемому в хранилище файлу
        /// </summary>
        string Path { get; set; }
    }

    public static class LoadIFilePathContainerClass
    {
        /// <summary>
        /// Загрузка файла в хранилище
        /// </summary>
        public static bool LoadToStorage(this IFilePathContainer file, string storagePath)
        {
            if (string.IsNullOrWhiteSpace(file.Path))
                return false;

            var destFileName = $"{storagePath}\\{file.Id}{Path.GetExtension(file.Path)}";
            if (File.Exists(destFileName) == false)
            {
                File.Copy(file.Path, destFileName);
                file.Path = null;
            }

            return true;
        }

    }

    public interface IIsActual
    {
        bool IsActual { get; set; }
    }
}