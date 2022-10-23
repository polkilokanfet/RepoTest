namespace HVTApp.Model.Services
{
    public interface IFileCopyStorage
    {
        IFileStorage File { get; }
        string TargetPath { get; }
        string SourcePath { get; }
    }
}