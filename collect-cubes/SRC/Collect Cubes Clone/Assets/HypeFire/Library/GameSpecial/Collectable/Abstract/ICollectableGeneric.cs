using UnityEngine;

namespace HypeFire.Library.GameSpecial.Collectable.Abstract
{
	public interface ICollectableGeneric<T>
	{
		public T collectable { get; }
		public void Collect(ICollector collector);
	}
}