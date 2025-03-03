using HVTApp.Infrastructure.Services.Storage;

namespace HVTApp.Model.Services.Storage
{
    public class FileCopyInfoTechnicalSpecification : FileCopyInfo
    {
        public string TargetName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="targetName">Желаемое название файла</param>
        public FileCopyInfoTechnicalSpecification(IFileStorage file, string destinationDirectory, string targetName = null) : base(file, destinationDirectory)
        {
            TargetName = targetName;
        }

        public override string SourcePath => GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
        public override FileCopyInfoType FileCopyInfoType => FileCopyInfoType.TechnicalSpecification;
    }
}