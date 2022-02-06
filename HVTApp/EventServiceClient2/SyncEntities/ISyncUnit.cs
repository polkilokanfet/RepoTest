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
        /// Публикация события только внутри текущего приложения
        /// </summary>
        /// <param name="model"></param>
        void PublishWithinApp(object model);

        /// <summary>
        /// Хост сервиса теперь недоступен
        /// </summary>
        event Action ServiceHostDisabled;
    }
}