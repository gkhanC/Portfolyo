using System;
using System.Collections;
using System.Collections.Generic;
using HypeFire.Library.Patterns.Observer;
using HypeFire.Library.Utilities.Extensions.Object;
using Managers;
using Objects;
using UnityEngine;

namespace Player
{
	public class Collector : MonoBehaviour
	{
		public static Collector GlobalAccess { get; private set; }

		public int collectCount;
		public int collectedLayerId = 9;
		public List<ObjectController> collectedObjects = new List<ObjectController>();
		public List<ObjectController> pool = new List<ObjectController>();

		private void Awake()
		{
			GlobalAccess = this;
		}

		public void Collect(params ObjectController[] objs)
		{
			foreach (var objectController in objs)
			{
				if (!collectedObjects.Contains(objectController))
				{
					objectController.gameObject.layer = collectedLayerId;
					collectedObjects.Add(objectController);
					collectCount++;

					if (pool.Contains(objectController))
					{
						pool.Remove(objectController);
					}
					objectController.Die(transform.position);
					LevelCompleteCheck();
				}
			}
		}

		private void LevelCompleteCheck()
		{
			if (collectCount >= LevelManager.GloballAccess.objectCount)
			{
				LevelManager.GloballAccess.LevelCompleted(collectCount);
			}
		}

	}
}