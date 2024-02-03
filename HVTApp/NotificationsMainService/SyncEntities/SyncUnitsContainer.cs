using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationsMainService.SyncEntities
{
    public class SyncUnitsContainer : IDisposable
    {
        private readonly List<ISyncUnit> _list = new List<ISyncUnit>();

        public void Add(ISyncUnit member)
        {
            if (_list.Any(syncUnit => syncUnit.ModelType == member.ModelType && syncUnit.EventType == member.EventType))
            {
                throw new ArgumentException("Попытка повторной регистрации типа");
            }

            _list.Add(member);
        }

        public ISyncUnit GetSyncUnit(Type modelType, Type eventType) => 
            _list.Single(syncUnit => syncUnit.ModelType == modelType && syncUnit.EventType == eventType);

        public void Dispose()
        {
            foreach (var member in _list)
            {
                member.Dispose();
            }
            _list.Clear();
        }
    }
}