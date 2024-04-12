using HVTApp.Infrastructure.Services.Storage;

namespace HVTApp.Model.Services.Storage
{
    public class FileCopyInfoTechnicalSpecification : FileCopyInfo
    {
        public FileCopyInfoTechnicalSpecification(IFileStorage file, string destinationDirectory) : base(file, destinationDirectory)
        {
        }

        public override string SourcePath => GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
        public override FileCopyInfoType FileCopyInfoType => FileCopyInfoType.TechnicalSpecification;
    }
}