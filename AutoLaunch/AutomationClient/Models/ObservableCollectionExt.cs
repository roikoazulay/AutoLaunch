using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace ObservableCollectionExtention
{
    public class NotifyCollectionChangedEventArgsEx : NotifyCollectionChangedEventArgs
    {
        public NotifyCollectionChangedEventArgsEx(NotifyCollectionChangedAction action)
            : base(action)
        {
        }

        public NotifyCollectionChangedEventArgsEx(NotifyCollectionChangedAction action, IList items)
            : base(action, items)
        {
        }

        public new IList OldItems
        {
            get { return base.OldItems; }
            set
            {
                typeof(NotifyCollectionChangedEventArgs).GetField("_oldItems", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, value);
            }
        }
    }

    public class ObservableCollectionExt<T> : ObservableCollection<T>, INotifyCollectionChanged
    {
        public ObservableCollectionExt()
        {
        }

        public ObservableCollectionExt(IEnumerable<T> items)
            : base(items)
        {
        }

        public void AddRange(IEnumerable<T> items)
        {
            bool isEditing = IsEditing;
            IsEditing = true;
            foreach (T item in items)
                Add(item);
            IsEditing = isEditing;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToList()));
        }

        protected override void ClearItems()
        {
            var removedItems = this.ToList();
            bool isEditing = IsEditing;
            IsEditing = true;
            base.ClearItems();
            IsEditing = isEditing;
            OnCollectionChanged(new NotifyCollectionChangedEventArgsEx(NotifyCollectionChangedAction.Reset) { OldItems = removedItems });
        }

        public bool IsEditing { get; private set; }

        public void BeginEdit()
        {
            if (!IsEditing)
                IsEditing = true;
        }

        public void EndEdit()
        {
            if (IsEditing)
            {
                IsEditing = false;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (CollectionChanged != null && !IsEditing)
                CollectionChanged(this, e);
        }

        public new event NotifyCollectionChangedEventHandler CollectionChanged;

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { CollectionChanged += value; }
            remove { CollectionChanged -= value; }
        }
    }
}