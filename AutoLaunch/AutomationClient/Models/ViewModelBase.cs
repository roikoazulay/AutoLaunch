using System;
using System.ComponentModel;
using AutomationCommon;

namespace AutomationClient
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        internal SaveData SaveData = Singleton.Instance<SaveData>();

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property has a new value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the event
        /// </summary>
        /// <param name="propertyName">Property name that has new value</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion INotifyPropertyChanged Members

        #region IDisposable Members

        /// <summary>
        /// Implementation of the dispose method
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// The child class should implement a personal dispose procedure
        /// </summary>
        protected virtual void OnDispose()
        {
            //do nothing because abstract
        }

        #endregion IDisposable Members
    }
}