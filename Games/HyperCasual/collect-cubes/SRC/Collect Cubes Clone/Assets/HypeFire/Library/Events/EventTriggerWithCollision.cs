using UnityEngine;

namespace HypeFire.Library.Events
{
	[RequireComponent(typeof(Rigidbody))]
	public class EventTriggerWithCollision : EventObject
	{
		public string triggerTag = "Unknow";

		public void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag(triggerTag))
			{
				objectEvent.Invoke();
			}
		}
	}
}