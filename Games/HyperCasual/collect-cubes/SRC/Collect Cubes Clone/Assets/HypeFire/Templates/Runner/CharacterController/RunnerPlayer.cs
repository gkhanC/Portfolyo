using System;
using HypeFire.Library.Controllers.InputControllers;
using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Controllers.InputControllers.ScreenToWorldPosition;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Controllers.Swerve;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Logger;
using HypeFire.Templates.Runner.CharacterController.Abstract;
using HypeFire.Templates.Runner.Managers;
using UnityEngine;

namespace HypeFire.Templates.Runner.CharacterController
{
	[RequireComponent(typeof(RigidbodyMove), typeof(RotateController), typeof(RigidbodyMove))]
	public class RunnerPlayer : MonoBehaviour, IRunnerCharacterController
	{
		private bool isInitial { get; set; } = false;
		public bool isMoveAble { get; private set; } = false;
		public bool isJumpAble { get; private set; } = false;
		public bool isRotateAble { get; private set; } = false;

		[field: SerializeField] private float speed { get; set; } = 250f;
		[field: SerializeField] private float horizontalLerpTime { get; set; } = .1f;
		[field: SerializeField] private float zOffset { get; set; } = 5f;

		private Vector3 _horizontalTarget = Vector3.zero;
		private Vector3 _horizontalVelocity = Vector3.zero;

		public RigidbodyMove rigidbodyMove { get; private set; }
		public RotateController rotateController { get; private set; }
		private Rigidbody _rigidbody;
		private RunnerManager _manager;

		public void Initial()
		{
			_manager = RunnerManager.GetInstance();

			if (_manager.IsNotNull())
			{
				isInitial = true;
			}
			else
			{
				_manager.LogError($"{nameof(RunnerManager)} Component cannot be found.");
			}
		}

		public void Awake()
		{
			Initial();

			if (isInitial)
			{
				this.LogSuccess($"{nameof(RunnerPlayer)} Component initialized.");
			}
			else
			{
				this.LogError($"{nameof(RunnerPlayer)} Component cannot be initialized.");
			}

			rotateController = GetComponent<RotateController>();
			rigidbodyMove = GetComponent<RigidbodyMove>();
		}

		private void Start()
		{
			if (STWReader.GloballAccess.IsNotNull())
			{
				STWReader.GloballAccess.Init(Camera.main, this.gameObject);
				InputManager.GetInstance().Listeners.AddListener(this.InputListening);
			}

			_rigidbody = GetComponent<Rigidbody>();
			rigidbodyMove.isAutoMoveEnabled = true;
			Move(speed, Vector3.forward);
		}

		private void Update()
		{
			if (Input.GetButton("Fire1"))
			{
				var rot = _horizontalTarget - transform.position;
				RotateWithDirection(rot);

				var position = transform.position;

				position = Vector3.SmoothDamp(position,
					new Vector3(_horizontalTarget.x, position.y, position.z),
					ref _horizontalVelocity,
					horizontalLerpTime);

				transform.position = position;
			}
			else
			{
				RotateWithDirection(Vector3.forward);
			}
		}

		public void Move(float speed, Vector3 direction)
		{
			rigidbodyMove.Init(speed, direction);
		}

		public void Rotate(Vector3 direction)
		{
			rotateController.RotateToPosition(direction);
		}

		public void RotateWithDirection(Vector3 direction)
		{
			rotateController.RotateWithDirection(direction);
		}

		public void Jump(float force, Vector3 direction)
		{
			_rigidbody.AddForce(direction * force, ForceMode.Impulse);
		}

		public void Stop()
		{
			rigidbodyMove.Stop();
		}

		public void InputListening(IInputResult result)
		{
			_horizontalTarget = result.data.position;
			var position = transform.position;
			_horizontalTarget.z = position.z + zOffset;
			_horizontalTarget.y = position.y;
		}
	}
}