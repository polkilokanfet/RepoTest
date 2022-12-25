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
}