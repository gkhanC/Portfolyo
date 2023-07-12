using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Logger;
using Objects.Abstract;
using UnityEngine;
using Managers;
using Player;
using DG.Tweening;

namespace Objects
{
	[RequireComponent(typeof(Rigidbody))]
	public class ObjectController : MonoBehaviour, IObjectController
	{
		protected bool _isInDetector = false;
		private GameObject _gameObject;
		private Transform _transform;

		public bool isHandled;
		public bool isCollected;
		public bool isDiedTime;
		public int defaultLayer = 6;

		public new Collider collider;
		public Rigidbody rigidBody;

		public GameObject getGameObject => _gameObject;
		public Transform getTransform => _transform;

		private void Awake()
		{
			_gameObject = gameObject;
			_transform = transform;
		}

		private void Start()
		{
			gameObject.layer = defaultLayer;
			rigidBody = GetComponent<Rigidbody>();
			collider = GetComponent<Collider>();

			if (Collector.GlobalAccess.IsNotNull())
			{
				Collector.GlobalAccess.pool.Add(this);
			}
		}

		public virtual void Update()
		{
			if (LevelManager.GloballAccess.IsNull() || !LevelManager.GloballAccess.isLevelStarted)
				return;

			if (transform.position.y <= -1)
			{
				Sleep();
			}

			if (_isInDetector)
			{
				if (rigidBody.isKinematic)
					InDetector();
			}
			else
			{
				OutDetector();
			}
		}


		public void InDetector()
		{
			if (rigidBody.isKinematic) rigidBody.isKinematic = false;
		}

		public void OutDetector()
		{
			if (rigidBody.velocity.magnitude < .1f)
			{
				if (!rigidBody.isKinematic) rigidBody.isKinematic = true;
			}
		}

		public void SetColor(Color color)
		{
			var meshRenderer = GetComponent<MeshRenderer>();
			if (meshRenderer.IsNull())
			{
				getGameObject.LogWarning("MeshRenderer is null.The Game Object Color isn't changed.");
				return;
			}

			var newMaterial = new Material(meshRenderer.sharedMaterial);
			newMaterial.SetColor("_Color", color);


			meshRenderer.material = newMaterial;
		}


		public void SetLocalPosition(Vector3 position)
		{
			_transform.localPosition = position;
		}

		public void SetSize(float size)
		{
			_transform.localScale = Vector3.one * size;
		}

		public void SetSize(Vector3 size)
		{
			_transform.localScale = size;
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<ObjectDetector>(out var obj))
			{
				_isInDetector = true;
			}
		}

		protected virtual void OnTriggerStay(Collider other)
		{
			if (!isCollected)
			{
				if (other.TryGetComponent<Collector>(out var collector))
				{
					OutDetector();
					collector.Collect(this);

					getGameObject.layer = 9;
					isHandled = true;
					isCollected = true;

					return;
				}
			}

			if (gameObject.layer == 9) return;

			if (other.gameObject.layer == 16)
			{
				getGameObject.layer = 15;
				return;
			}

			if (other.gameObject.layer == 17)
			{
				getGameObject.layer = 12;
				return;
			}
		}

		public void Drop(Transform player)
		{
			var dist = Vector3.Distance(transform.position, player.position);
			if (dist >= .5f)
			{
				var target = player.position;
				target.y = transform.position.y;
				var direction = player.position - transform.position;
				rigidBody.AddForce(direction * .05f, ForceMode.Impulse);
			}
		}

		protected virtual void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<ObjectDetector>(out var obj))
			{
				_isInDetector = false;
			}

			if (gameObject.layer == 9) return;

			if (other.gameObject.layer == 16)
			{
				getGameObject.layer = defaultLayer;
				return;
			}

			if (other.gameObject.layer == 17)
			{
				getGameObject.layer = defaultLayer;
			}
		}

		public void Die(Vector3 position)
		{
			if (isDiedTime)
				return;

			transform.DOMove(position, 1f).OnStart(() =>
				{
					isDiedTime = true;
					SetColor(Player.PlayerController.GloballAccess.collectedColor);
					transform.DOScale(Vector3.zero, 1f);
				})
				.OnComplete(() =>
				{
					Collector.GlobalAccess.collectedObjects.Remove(this);
					Sleep();
				});
		}

		public virtual void Sleep()
		{
			gameObject.SetActive(false);
		}
	}
}