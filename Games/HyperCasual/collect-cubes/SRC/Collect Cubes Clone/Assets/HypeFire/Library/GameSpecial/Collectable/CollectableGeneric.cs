using HypeFire.Library.GameSpecial.Collectable.Abstract;
using UnityEngine;

namespace HypeFire.Library.GameSpecial.Collectable
{
	public abstract class CollectableGeneric<T> : MonoBehaviour, ICollectableGeneric<T>
	{
		public abstract T collectable { get; protected set; }
		public abstract void Collect(ICollector collector);
	}
}