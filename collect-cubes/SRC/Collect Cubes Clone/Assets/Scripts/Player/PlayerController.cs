using HypeFire.Templates.Runner.Managers.GameSettings.Constraints;
using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Utilities.Extensions.Initializers;
using HypeFire.Library.Controllers.InputControllers;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Extensions.Vector;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Patterns.Observer;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Utilities.Logger;
using UnityEngine;
using Managers;

namespace Player
{
	[RequireComponent(typeof(RigidbodyMove), typeof(RotateController))]
	public class PlayerController : MonoBehaviour, IInputListener
	{
		public static PlayerController GloballAccess { get; private set; }
		
		public bool isLevelStarted;
		public Color collectedColor;
		public CollectorHandle collectorHandle;
		public PlayerData playerData;
		public PositionalConstraint moveAbleArea;
		
		private Vector3 _position;
		private Vector3 _inputDirection;
		
		private IInputResult _inputResult;
		private RigidbodyMove _rigidbodyMove;
		private RotateController _rotateController;

		private ObserverContainer<ObserverData<bool>> startedObserver =
			new ObserverContainer<ObserverData<bool>>();

		private void Awake()
		{
			gameObject.InitComponent(out _rigidbodyMove);
			gameObject.InitComponent(out _rotateController);
			GloballAccess = this;

			if (playerData.IsNull())
			{
				gameObject.LogError("Player data object not found!");
			}
		}

		private void Start()
		{
			InputManager.GetInstance().Listeners.AddListener(this.InputListening);
			_inputDirection = transform.TransformDirection(Vector3.forward);

			if (LevelManager.GloballAccess.IsNotNull())
			{
				startedObserver.Subscribe(LevelManager.GloballAccess.startedInfoPublisher,
					LevelStartedListener, LevelStartedErrorListener);
			}
		}

		private void FixedUpdate()
		{
			if (!isLevelStarted) return;

			if (_inputResult.IsNotNull() && _inputResult.data.isActive)
			{
				_inputDirection = _inputResult.data.direction.OrderChange(1, 3, 2);

				if (_inputDirection.magnitude > 0.05f)
					_rotateController.RotateWithDirection(_inputDirection);

				_rigidbodyMove.Move(playerData.speed, _inputDirection);
				ApplyConstraints();
			}
			else
			{
				_rigidbodyMove.Stop();
			}
		}

		public void ApplyConstraints()
		{
			_position = transform.position;

			_position.x = moveAbleArea.useXConstraint
				? Mathf.Clamp(_position.x, moveAbleArea.consX.min, moveAbleArea.consX.max)
				: _position.x;

			_position.y = 1f;
			_position.z = moveAbleArea.useZConstraint
				? Mathf.Clamp(_position.z, moveAbleArea.consZ.min, moveAbleArea.consZ.max)
				: _position.z;

			transform.position = _position;
		}


		public void LevelStartedListener
			(ObserverData<bool> data)
		{
			isLevelStarted = data.result;
		}

		public void LevelStartedErrorListener
			(ObserverData<bool> data)
		{
			isLevelStarted = data.result;
		}

		public void InputListening(IInputResult result)
		{
			_inputResult = result;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Collector>(out var collector))
			{
				collector.Collect(collectorHandle.objectControllers.ToArray());
			}
		}
	}
}