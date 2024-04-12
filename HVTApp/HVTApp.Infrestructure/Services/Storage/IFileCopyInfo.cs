namespace HVTApp.Infrastructure.Services.Storage
{
    public interface IFileCopyInfo
    {
        /// <summary>
        /// Файл хранилища
        /// </summary>
        IFileStorage File { get; }

        /// <summary>
        /// Имя директории назначения
        /// </summary>
        string DestinationDirectoryPath { get; }

        /// <summary>
        /// Путь к файловому хранилищу
        /// </summary>
        string SourcePath { get; }

        FileCopyInfoType FileCopyInfoType { get; }
    }
}