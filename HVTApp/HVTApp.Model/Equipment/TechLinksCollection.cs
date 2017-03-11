using System;
using System.Collections;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TechLinksCollection : BaseEntity, ICollection<TechLink>
    {
        private readonly List<TechLink> _links = new List<TechLink>();
        public IEnumerator<TechLink> GetEnumerator()
        {
            return _links.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _links).GetEnumerator();
        }

        public void Add(TechLink link)
        {
            if (link.ParentLink != null && !_links.Contains(link.ParentLink))
                throw new ArgumentException("Коллекция не содержит родительского звена для добавления этого");
            if (_links.Contains(link))
                throw new ArgumentException("Коллекция уже содержит такое звено");

            _links.Add(link);
        }

        public void Clear()
        {
            _links.Clear();
        }

        public bool Contains(TechLink item)
        {
            return _links.Contains(item);
        }

        public void CopyTo(TechLink[] array, int arrayIndex)
        {
            _links.CopyTo(array, arrayIndex);
        }

        public bool Remove(TechLink item)
        {
            return _links.Remove(item);
        }

        public int Count => _links.Count;

        public bool IsReadOnly => false;
    }
}