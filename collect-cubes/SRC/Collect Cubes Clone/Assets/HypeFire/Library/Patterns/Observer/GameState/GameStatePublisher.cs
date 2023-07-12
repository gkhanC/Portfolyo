using HypeFire.Library.Patterns.Observer.Abstract;
using HypeFire.Library.Utilities.Extensions.Object;

namespace HypeFire.Library.Patterns.Observer.GameState
{
    public class GameStatePublisher : IPublisherContainer<GameStateData>
    {
        protected GameStateObserverStorage storage { get; set; } = new GameStateObserverStorage();

        public IObserversStorage<GameStateData> getStorage => storage;

        public void AddObserver(IObserverContainer<GameStateData> observerContainer)
        {
            if (observerContainer.listener.IsNotNull())
                storage.listeners.AddListener(observerContainer.listener);

            if (observerContainer.errorListener.IsNotNull())
                storage.errorListeners.AddListener(observerContainer.errorListener);
        }

        public void RemoveObserver(IObserverContainer<GameStateData> observerContainer)
        {
            if (observerContainer.listener.IsNotNull())
                storage.listeners.RemoveListener(observerContainer.listener);

            if (observerContainer.errorListener.IsNotNull())
                storage.errorListeners.RemoveListener(observerContainer.errorListener);
        }

        public void OnNotify(GameStateData data)
        {
            getStorage.listeners?.Invoke(data);
        }

        public void OnError(GameStateData data)
        {
            getStorage.errorListeners?.Invoke(data);
        }
    }
}