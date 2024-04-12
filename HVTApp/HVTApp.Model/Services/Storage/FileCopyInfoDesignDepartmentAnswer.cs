using HVTApp.Infrastructure.Services.Storage;

namespace HVTApp.Model.Services.Storage
{
    public class FileCopyInfoDesignDepartmentAnswer : FileCopyInfo
    {
        public FileCopyInfoDesignDepartmentAnswer(IFileStorage file, string destinationDirectory) : base(file, destinationDirectory)
        {
        }

        public override string SourcePath => GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
        public override FileCopyInfoType FileCopyInfoType => FileCopyInfoType.DesignDepartmentAnswer;
    }
}