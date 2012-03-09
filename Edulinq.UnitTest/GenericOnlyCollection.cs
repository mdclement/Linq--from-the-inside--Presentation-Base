using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Edulinq.UnitTests
{
    /// <summary>
    /// Class which implements ICollection[T] but not ICollection.
    /// </summary>
    public class GenericOnlyCollection<T> : ICollection<T>
    {
        private readonly List<T> backingList;

        public GenericOnlyCollection(IEnumerable<T> items)
        {
            backingList = new List<T>(items);
        }

        public void Add(T item)
        {
            backingList.Add(item);
        }

        public void Clear()
        {
            backingList.Clear();
        }

        public bool Contains(T item)
        {
            return backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            backingList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return backingList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)backingList).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return backingList.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return backingList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
