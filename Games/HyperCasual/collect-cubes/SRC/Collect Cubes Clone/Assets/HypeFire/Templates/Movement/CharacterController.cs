using System;
using UnityEngine;
using HypeFire.Templates.Movement.Abstract;

namespace HypeFire.Templates.Movement
{
	[Serializable]
	public class CharacterController : ICharacterController
	{
		[field: SerializeField] public MovementController movementController { get; private set; }

		public void SetController(MovementController controller)
		{
			movementController = controller;
		}

		public void Move(Transform objectTransform, Vector3 direction, float speed,
			out Vector3 currentVelocity) =>
			movementController.Move(objectTransform, direction, speed, out currentVelocity);
	}
}