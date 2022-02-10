using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace EventServiceClient2.SyncEntities
{
    public interface ITargetUser<TModel>
        where TModel : BaseEntity
    {

        /// <summary>
        /// Является ли предложенный пользователь адресатом уведомлений этого контейнера и этой сущности
        /// </summary>
        /// <param name="user">Предложенный пользователь</param>
        /// <param name="model">Сущность</param>
        /// <returns></returns>
        bool IsTargetUser(User user, TModel model);

        /// <summary>
        /// Является ли пользователь текущего приложения адресатом уведомлений при текущей роли
        /// </summary>
        /// <param name="model">Сущность</param>
        /// <returns></returns>
        bool CurrentUserIsTargetForNotification(TModel model);
    }
}