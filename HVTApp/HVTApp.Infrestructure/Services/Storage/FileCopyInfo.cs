using System.IO;

namespace HVTApp.Infrastructure.Services.Storage
{
    public abstract class FileCopyInfo : IFileCopyInfo
    {
        private readonly string _destinationDirectory;

        /// <summary>
        /// Файл хранилища
        /// </summary>
        public IFileStorage File { get; }

        /// <summary>
        /// Путь к файловому хранилищу
        /// </summary>
        public abstract string SourcePath { get; }

        /// <summary>
        /// Имя директории назначения
        /// </summary>
        public string DestinationDirectoryPath => Path.Combine(_destinationDirectory, this.FileCopyInfoType.ToString());

        public abstract FileCopyInfoType FileCopyInfoType { get; }

        protected FileCopyInfo(IFileStorage file, string destinationDirectory)
        {
            _destinationDirectory = destinationDirectory;
            File = file;
        }

        public override bool Equals(object obj)
        {
            if (obj is IFileCopyInfo other)
            {
                return this.File.Id == other.File.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (File != null ? File.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DestinationDirectoryPath != null ? DestinationDirectoryPath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SourcePath != null ? SourcePath.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public enum FileCopyInfoType
    {
        TechnicalSpecification,
        DesignDepartmentAnswer
    }
}