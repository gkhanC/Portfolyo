using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer.GameState
{
    public class GameStateObserver : IObserverContainer<GameStateData>
    {
        public IPublisherContainer<GameStateData> publisherContainer { get; private set; }
        public UnityAction<GameStateData> listener { get; set; }
        public UnityAction<GameStateData> errorListener { get; set; }

        public void Subscribe(IPublisherContainer<GameStateData> publisherContainer, UnityAction<GameStateData> listener,
            UnityAction<GameStateData> errorListener)
        {
            this.listener = listener;
            this.errorListener = errorListener;
            this.publisherContainer = publisherContainer;
            this.publisherContainer.AddObserver(this);
        }

        public void UnSubscribe()
        {
            publisherContainer.RemoveObserver(this);
        }
    }
}