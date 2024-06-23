using HypeFire.Library.Patterns.Observer.Abstract;
using UnityEngine;

namespace HypeFire.Library.Patterns.Observer.GameState
{
    public class GameStateData : IObserverData
    {
        public GameObject eventSender { get; }
        public  GameStateInfo info { get; }

        public GameStateData(GameObject eventSender, GameStateInfo info)
        {
            this.eventSender = eventSender;
            this.info = info;
        }
    }

    public class GameStateInfo
    {
        public string stateString { get; protected set; } = "Stopped";
        public bool isLive { get; protected set; } = false;
    }

    public class StopGameState : GameStateInfo
    {
        public StopGameState()
        {
            stateString = "Stopped";
            isLive = false;
        }
    }

    public class PauseGameState : GameStateInfo
    {
        public PauseGameState()
        {
            stateString = "Paused";
            isLive = false;
        }
    }

    public class StartGameState : GameStateInfo
    {
        public StartGameState()
        {
            stateString = "Start";
            isLive = true;
        }
    }
}