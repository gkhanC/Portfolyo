using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer.Abstract
{
    /// <summary>
    /// Publisher subscriber on Observer
    /// </summary>
    /// <typeparam name="T">Observer Data</typeparam>
    public interface IObserverContainer <T> where  T : IObserverData
    {
        /// <summary>
        /// Publisher on Observer
        /// </summary>
        public IPublisherContainer<T> publisherContainer { get; }

        /// <summary>
        /// Notify event function
        /// </summary>
        public UnityAction<T> listener { get; set; } 
        
        /// <summary>
        /// Error notify function
        /// </summary>
        public UnityAction<T> errorListener { get; set; }

        /// <summary>
        ///  Subscribe to publisher
        /// </summary>
        /// <param name="listener">notify action</param>
        /// <param name="errorListener">error action</param>
        public void Subscribe(IPublisherContainer<T> publisherContainer,UnityAction<T> listener, UnityAction<T> errorListener);

        /// <summary>
        /// UnSubscribe to publisher 
        /// </summary>
        public void UnSubscribe();

    }
}