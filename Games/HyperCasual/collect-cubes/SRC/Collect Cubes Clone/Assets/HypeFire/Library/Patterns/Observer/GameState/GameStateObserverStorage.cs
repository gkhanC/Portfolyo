using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine.Events;

namespace HypeFire.Library.Patterns.Observer.GameState
{
    public class GameStateObserverStorage : IObserversStorage<GameStateData>
    {
        public UnityEvent<GameStateData> listeners { get; set; } = new UnityEvent<GameStateData>();
        public UnityEvent<GameStateData> errorListeners { get; set; } = new UnityEvent<GameStateData>();
    }
}