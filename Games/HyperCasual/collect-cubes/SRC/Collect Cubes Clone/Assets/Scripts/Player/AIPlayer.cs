using HypeFire.Templates.Runner.Managers.GameSettings.Constraints;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Attributes;
using Random = UnityEngine.Random;
using UnityEngine;
using Managers;
using Objects;

namespace Player
{
	public class AIPlayer : MonoBehaviour
	{
		[OptionalValues(1f, 2f, 3f, 4f, 5f)] public float aiLogicLevel = 1f;

		private bool _isReleaseTime;
		public Vector3 pos;

		public Rigidbody rigidBody;
		public AICollectorHandle handle;

		private bool _isInCorner;
		private float _moveTimer = 0f;
		private readonly float _moveTimeThreshold = 3f;
		private readonly float _waitTime = 5f;
		private float _speed;
		private float _timer;
		private float getWaitTime => _waitTime - aiLogicLevel;

		private PositionalConstraint constraint;
		private ObjectController targetObject;

		private void Start()
		{
			pos = new Vector3(0f, transform.position.y, 3.88f);
			constraint = PlayerController.GloballAccess.moveAbleArea;
			pos = transform.position;
		}

		private void Update()
		{
			if (!LevelManager.GloballAccess.isLevelStarted)
				return;

			MovePosition();

			if (_timer < getWaitTime)
			{
				_timer += Time.deltaTime;
				return;
			}
		}

		private void MovePosition()
		{
			var dist = Vector3.Distance(transform.position, pos);
			if (dist > 0.1f)
			{
				IsInCorner();

				_speed = dist < 1f ? (dist + 1f) : (1.5f + (aiLogicLevel * .5f));
				transform.LookAt(pos);
				rigidBody.velocity = transform.TransformDirection(Vector3.forward) * _speed;
			}
			else
			{
				if (_isReleaseTime)
				{
					targetObject = null;
					handle.objectControllers.Clear();
					_isReleaseTime = false;
					_timer = 0;
				}

				rigidBody.velocity = Vector3.zero;
				TakePosition();
			}
		}

		private void IsInCorner()
		{
			if (_isInCorner)
			{
				_moveTimer += Time.deltaTime;

				if (targetObject.IsNotNull() && handle.objectControllers.Contains(targetObject))
				{
					pos = GetReleasePosition();
					_isReleaseTime = true;
					_isInCorner = false;
					_moveTimer = 0f;
				}
				else if (_moveTimeThreshold <= _moveTimer)
				{
					pos = GetRandomPosition();
					_isInCorner = false;
					_moveTimer = 0f;
				}
			}
		}

		private void TakePosition()
		{
			if (constraint.IsNull())
				return;

			if (Collector.GlobalAccess.pool.Count < aiLogicLevel)
			{
				if (handle.objectControllers.Count >= 1)
				{
					pos = GetReleasePosition();
					return;
				}
			}

			if (_timer < getWaitTime)
			{
				pos = GetRandomPosition();
				return;
			}

			if (IsReleaseTime(aiLogicLevel))
			{
				pos = GetReleasePosition();
			}
			else
			{
				pos = GetTargetPosition();
			}
		}

		private Vector3 GetRandomPosition()
		{
			var newPos = Vector3.up * transform.position.y;
			newPos.x = Random.Range(constraint.consX.min + 2f, constraint.consX.max - 2f);
			newPos.z = Random.Range(constraint.consZ.min + 2f, constraint.consZ.max - 2f);
			_timer = 0f;
			return newPos;
		}

		private Vector3 GetTargetPosition()
		{
			var poolCount = Collector.GlobalAccess.pool.Count;
			if (poolCount <= 0)
			{
				return GetRandomPosition();
			}

			var newPos = Vector3.up * transform.position.y;
			var targetId = Random.Range(0, poolCount);
			var target = Collector.GlobalAccess.pool[targetId];
			targetObject = target;

			var position = target.transform.position;
			newPos.x = position.x;
			newPos.z = position.z;

			if (pos == newPos)
			{
				newPos = GetRandomPosition();
			}

			return newPos;
		}

		private Vector3 GetReleasePosition()
		{
			var newPos = Vector3.up * transform.position.y;
			var position = Collector.GlobalAccess.transform.position;
			newPos.x = position.x;
			newPos.z = position.z;

			return newPos;
		}

		private bool IsReleaseTime(float objectCount = 1f)
		{
			if (_isReleaseTime) return _isReleaseTime;

			_isReleaseTime = handle.objectControllers.Count >= objectCount;
			return _isReleaseTime;
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.CompareTag("Wall"))
			{
				_isInCorner = true;
			}
		}
	}
}