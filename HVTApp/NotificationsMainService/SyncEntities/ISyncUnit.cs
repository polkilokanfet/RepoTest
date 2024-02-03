using System;

namespace NotificationsMainService.SyncEntities
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
    }
}