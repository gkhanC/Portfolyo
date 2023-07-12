using System;

namespace HypeFire.Library.Patterns.Observer.Abstract
{
    /// <summary>
    /// Publisher on Observer
    /// </summary>
    /// <typeparam name="T">Observer Data</typeparam>
    public interface IPublisherContainer<T> where T : IObserverData
    {
        public IObserversStorage<T> getStorage { get; }

        public void AddObserver(IObserverContainer<T> observerContainer);
        public void RemoveObserver(IObserverContainer<T> observerContainer);

        /// <summary>
        /// On notify event
        /// </summary>
        /// <param name="data"></param>
        public void OnNotify(T data);
        
        /// <summary>
        /// On error event
        /// </summary>
        /// <param name="data"></param>
        public void OnError(T data);
    }
}