using HypeFire.Library.Utilities.Singleton;

namespace LeveGenerator.Abstract
{
	public abstract class LevelGeneratorBase : MonoBehaviourSingleton<LevelGeneratorBase>
	{
		public abstract void Generate();
		public abstract void Clean();
	}
}