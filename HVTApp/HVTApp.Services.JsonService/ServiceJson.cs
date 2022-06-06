using System.IO;
using HVTApp.Infrastructure.Interfaces.Services;
using Newtonsoft.Json;

namespace HVTApp.Services.JsonService
{
    public class ServiceJson : IJsonService
    {
        public string Serialize<T>(T obj)
        {
            string result = JsonConvert.SerializeObject(obj);
            return result;
        }

        public T Deserialize<T>(string s)
        {
            T result = JsonConvert.DeserializeObject<T>(s);
            return result;
        }

        public void WriteJsonFile<T>(T obj, string path)
        {
            File.WriteAllText(path, this.Serialize(obj));
        }

        public T ReadJsonFile<T>(string path)
        {
            var text = File.ReadAllText(path);
            T result = this.Deserialize<T>(text);
            return result;
        }
    }
}
