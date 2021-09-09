using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
    /// <summary>
    /// Контейнер синхронизации
    /// </summary>
    public class SyncContainer : IDisposable
    {
        readonly List<ISync> _list = new List<ISync>();

        public void Add(ISync member)
        {
            if (_list.Any(x => x.ModelType == member.ModelType && x.EventType == member.EventType))
            {
                throw new ArgumentException("Попытка повторной регистрации типа");
            }

            _list.Add(member);

            member.ServiceHostDisabled += MemberOnServiceHostDisabled;
        }

        private void MemberOnServiceHostDisabled()
        {
            this.ServiceHostIsDisabled?.Invoke();
        }

        /// <summary>
        /// Публикация события синхронизации
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="model"></param>
        public void Publish<TModel, TEvent>(TModel model)
            where TModel : BaseEntity
            where TEvent : PubSubEvent<TModel>
        {
            //переводим в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    //публикуем событие
                    _list.Single(x => x.ModelType == typeof(TModel) && x.EventType == typeof(TEvent)).Publish(model);
                });
        }

        public void Dispose()
        {
            foreach (var member in _list)
            {
                member.ServiceHostDisabled -= MemberOnServiceHostDisabled;
                member.Dispose();
            }
            _list.Clear();
        }

        /// <summary>
        /// Хост сервиса стал недоступен
        /// </summary>
        public event Action ServiceHostIsDisabled;
    }
}