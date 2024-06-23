using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine;

namespace HypeFire.Library.Patterns.Observer
{
    public class ObserverData<T> : IObserverData
    {
        public T result { get; private set; }
        public GameObject eventSender { get; private set; }

        public ObserverData()
        {
        }

        public ObserverData(GameObject eventSender, T result)
        {
            this.eventSender = eventSender;
            this.result = result;
        }
    }
}