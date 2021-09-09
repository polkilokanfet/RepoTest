using System;

namespace EventServiceClient2.SyncEntities
{
    public interface ISyncUnit : IDisposable
    {
        /// <summary>
        /// Тип модели
        /// </summary>
        Type ModelType { get; }
        
        /// <summary>
        /// Тип события
        /// </summary>
        Type EventType { get; }

        /// <summary>
        /// Публикация события по модели
        /// </summary>
        /// <param name="model"></param>
        void Publish(object model);

        /// <summary>
        /// Хост сервиса теперь недоступен
        /// </summary>
        event Action ServiceHostDisabled;
    }
}