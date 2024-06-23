namespace HypeFire.Library.Patterns.Observer.Abstract
{
    /// <summary>
    /// IPublisher interface for Observer pattern.
    /// </summary>
    /// <typeparam name="T">is ObserverData </typeparam>
    public interface IPublisher<T> where T : IObserverData
    {
    }
}