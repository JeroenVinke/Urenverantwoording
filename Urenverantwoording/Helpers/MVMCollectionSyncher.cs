using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urenverantwoording.Helpers
{
    public class MVMCollectionSyncher<T> : ICollection<T>,
        IDisposable, INotifyCollectionChanged
    {
        #region fields
        private ObservableCollection<T> _wrappedCollection;
        #endregion

        public MVMCollectionSyncher(ObservableCollection<T> wrappedCollection)
        {
            if (wrappedCollection == null)
                throw new ArgumentNullException(
                 "wrappedCollection",
                 "wrappedCollection must not be null.");
            _wrappedCollection = wrappedCollection;
        }

        #region ICollection<T> Members
        public void Add(T item)
        {
            _wrappedCollection.Add(item);
            FireCollectionChanged(
             new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            FireCollectionChanged(
             new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            _wrappedCollection.Clear();
        }

        public bool Contains(T item)
        {
            return _wrappedCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _wrappedCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _wrappedCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }


        public bool Remove(T item)
        {
            if (_wrappedCollection.Remove(item))
            {
                FireCollectionChanged(
                  new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, _wrappedCollection.IndexOf(item)));
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return _wrappedCollection.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _wrappedCollection.GetEnumerator();
        }
        #endregion

        #region INotifyCollectionChanged Members
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void FireCollectionChanged(NotifyCollectionChangedEventArgs eventArg)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null) handler.Invoke(this, eventArg);
        }
        #endregion

        #region IDisposable Members
        public void Dispose() { _wrappedCollection = null; }
        #endregion
    }
}
