using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Player
{
	public class CollectorHandle : MonoBehaviour
	{
		public int count;
		public List<ObjectController> objectControllers = new List<ObjectController>();

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<ObjectController>(out var obj))
			{
				if (!objectControllers.Contains(obj))
				{
					obj.isHandled = true;
					obj.Drop(transform);
					objectControllers.Add(obj);
					count += 1;
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