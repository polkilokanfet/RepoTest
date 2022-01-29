using System;
using System.Collections.Generic;

namespace HVTApp.Model.Services
{
    class DictionarySpecial<T>
    {
        private readonly Dictionary<Guid, T> _dictionary = new Dictionary<Guid, T>();

        public T this[Guid id]
        {
            get => _dictionary[id];
            set 
            {            
                if (ContainsKey(id))
                {
                    _dictionary[id] = value;
                }
                else
                {
                    _dictionary.Add(id, value);
                }
            }
        }

        public bool ContainsKey(Guid id)
        {
            return _dictionary.ContainsKey(id);
        }

        public T RefreshAndReturnValue(Guid id, T value)
        {
            this[id] = value;
            return value;
        }
    }
}