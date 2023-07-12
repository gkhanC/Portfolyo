using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Player
{
	public class AICollectorHandle : MonoBehaviour
	{
		public List<ObjectController> objectControllers = new List<ObjectController>();
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<ObjectController>(out var obj))
			{
				if (!objectControllers.Contains(obj) && !obj.isHandled)
				{
					obj.isHandled = true;
					obj.Drop(transform);
					objectControllers.Add(obj);
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<ObjectController>(out var obj))
			{
				if (objectControllers.Contains(obj))
				{
					obj.isHandled = false;
					objectControllers.Remove(obj);
				}
			}
		}
	}
}