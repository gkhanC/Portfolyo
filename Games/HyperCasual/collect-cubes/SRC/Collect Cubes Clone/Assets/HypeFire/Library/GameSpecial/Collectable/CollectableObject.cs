using HypeFire.Library.GameSpecial.Collectable.Abstract;
using UnityEngine;

namespace HypeFire.Library.GameSpecial.Collectable
{
	public abstract class CollectableObject : MonoBehaviour, ICollectableObject
	{
		public abstract GameObject collectableObject { get; protected set; }
		public abstract void Collect(ICollector collector);
	}
}