using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer
{
    public class ObserverStorage<T> : IObserversStorage<T> where T : IObserverData
    {
        public UnityEvent<T> listeners { get; set; } = new UnityEvent<T>();
        public UnityEvent<T> errorListeners { get; set; } = new UnityEvent<T>();
    }
}