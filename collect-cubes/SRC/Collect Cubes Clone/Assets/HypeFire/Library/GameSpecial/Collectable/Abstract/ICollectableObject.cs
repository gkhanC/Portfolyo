using UnityEngine;

namespace HypeFire.Library.GameSpecial.Collectable.Abstract
{
	public interface ICollectableObject
	{
		public GameObject collectableObject { get; }
		public void Collect(ICollector collector);
	}
}