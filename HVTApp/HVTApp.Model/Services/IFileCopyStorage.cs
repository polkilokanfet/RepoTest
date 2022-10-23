namespace HVTApp.Model.Services
{
    public interface IFileCopyStorage
    {
        IFileStorage File { get; }
        string DestinationDirectoryName { get; }
        string SourcePath { get; }
    }
}