using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer.Abstract
{
    public interface IObserversStorage<T> where T : IObserverData
    {
        public UnityEvent<T> listeners { get; set; }
        public UnityEvent<T> errorListeners { get; set; }
    }
}