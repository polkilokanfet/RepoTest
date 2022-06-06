namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IJsonService
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string text);
        void WriteJsonFile<T>(T obj, string path);
        T ReadJsonFile<T>(string path);
    }
}