using HypeFire.Library.GameSpecial.Collectable;
using HypeFire.Library.GameSpecial.Collectable.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace HypeFire.Library.GameSpecial.Reward
{
	public class LootObject : CollectableGeneric<LootObject.LootData>
	{
		public UnityEvent collectEvent = new UnityEvent();
		[field: SerializeField] public override LootData collectable { get; protected set; } = new LootData();

		public override void Collect(ICollector collector)
		{
			collectEvent.Invoke();
			collector.TakeCollectable(this);
		}

		public class LootData
		{
			public float bonusValue = 10f;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<ICollector>(out var collector))
			{
				Collect(collector);
			}
		}
	}
}