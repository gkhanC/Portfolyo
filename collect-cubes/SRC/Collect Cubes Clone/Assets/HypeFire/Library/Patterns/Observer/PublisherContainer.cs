using HypeFire.Library.Patterns.Observer.Abstract;

namespace HypeFire.Library.Patterns.Observer
{
    /// <summary>
    /// Observer's publisher
    /// </summary>
    /// <typeparam name="T">IobserverData</typeparam>
    public class PublisherContainer<T> : IPublisherContainer<T> where T : IObserverData
    {
        private ObserverStorage<T> storage { get; set; } = new ObserverStorage<T>();
        public IObserversStorage<T> getStorage => storage;

        public void AddObserver(IObserverContainer<T> observerContainer)
        {
            storage.listeners.AddListener(observerContainer.listener);
            storage.errorListeners.AddListener(observerContainer.errorListener);
        }

        public void RemoveObserver(IObserverContainer<T> observerContainer)
        {
            storage.listeners.RemoveListener(observerContainer.listener);

            storage.errorListeners.RemoveListener(observerContainer.errorListener);
        }

        public void OnNotify(T data)
        {
            getStorage.listeners?.Invoke(data);
        }

        public void OnError(T data)
        {
            getStorage.errorListeners?.Invoke(data);
        }
    }
}