namespace HypeFire.Library.Patterns.Observer.GameState.Abstract
{
    public interface IGameStateObserver
    {
        public GameStateObserver gameStateObserver { get; }
        public void GameStateListener(GameStateData data);
        public void GameStateErrorListener(GameStateData data);
    }
}