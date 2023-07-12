namespace HypeFire.Library.Patterns.Observer.Abstract
{
    public interface IObserver<T> where T : IObserverData
    {
        public void Listener(T data);
        public void ErrorListener(T data);
    }
}