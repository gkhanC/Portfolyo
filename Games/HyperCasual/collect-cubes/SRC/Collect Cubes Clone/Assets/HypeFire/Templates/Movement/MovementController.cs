using System;
using UnityEngine;

namespace HypeFire.Templates.Movement
{
	[Serializable]
	public abstract class MovementController
	{
		public Vector3 velocity { get; protected set; } = Vector3.zero;

		public abstract void Move(Transform objectTransform, Vector3 direction, float speed,
			out Vector3 currentVelocity);
	}
}