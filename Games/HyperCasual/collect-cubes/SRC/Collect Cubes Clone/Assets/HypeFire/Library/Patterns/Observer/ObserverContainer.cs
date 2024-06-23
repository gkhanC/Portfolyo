using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer
{
    public class ObserverContainer<T> : IObserverContainer<T> where T : IObserverData
    {
        public IPublisherContainer<T> publisherContainer { get; private set; }
        public UnityAction<T> listener { get; set; }
        public UnityAction<T> errorListener { get; set; }
        
        public void Subscribe(IPublisherContainer<T> publisherContainer, UnityAction<T> listener, UnityAction<T> errorListener)
        {
            this.publisherContainer = publisherContainer;
            this.listener = listener;
            this.errorListener = errorListener;
            this.publisherContainer.AddObserver(this);
        }

        public void UnSubscribe()
        {
            publisherContainer.RemoveObserver(this);
        }
    }
}