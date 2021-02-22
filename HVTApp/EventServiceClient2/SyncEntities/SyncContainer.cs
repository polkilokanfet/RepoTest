using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using Prism.Events;

namespace EventServiceClient2.SyncEntities
{
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
        }

        public void Publish<TModel, TEvent>(TModel model)
            where TModel : BaseEntity
            where TEvent : PubSubEvent<TModel>
        {
            _list.Single(x => x.ModelType == typeof(TModel) && x.EventType == typeof(TEvent)).Publish(model);
        }

        public void Dispose()
        {
            _list.ForEach(x => x.Dispose());
            _list.Clear();
        }
    }
}