using JetBrains.Annotations;

namespace LeveGenerator.Builders.Abstract
{
	public abstract class BuilderBase
	{
		public bool isBuilt { get; set; } = false;
		[CanBeNull] public abstract ObjectData[] Build();
	}
}