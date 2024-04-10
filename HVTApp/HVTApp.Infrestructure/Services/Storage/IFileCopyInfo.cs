namespace HVTApp.Infrastructure.Services.Storage
{
    public interface IFileCopyInfo
    {
        /// <summary>
        /// Файл хранилища
        /// </summary>
        IFileStorage File { get; }

        /// <summary>
        /// Имя дериктории назначения
        /// </summary>
        string DestinationDirectoryPath { get; }

        /// <summary>
        /// Путь к файловому хранилищу
        /// </summary>
        string SourcePath { get; }
    }
}