using System;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using Managers;
using Player;
using UnityEngine;

namespace Objects
{
	public class ObjectControllerWithPool : ObjectController, IPoolObject<ObjectControllerWithPool>
	{
		private bool _isStarted;

		public override void Update()
		{
			if (!_isStarted) return;

			base.Update();
		}

		private void OnCollisionEnter(Collision other)
		{
			if (!_isStarted)
			{
				if (other.gameObject.layer is 0 or 6)
				{
					rigidBody.isKinematic = false;
					_isStarted = true;
				}
			}
		}

		protected override void OnTriggerEnter(Collider other)
		{
			if (!_isStarted) return;

			base.OnTriggerEnter(other);
		}

		protected override void OnTriggerStay(Collider other)
		{
			if (!_isStarted) return;

			base.OnTriggerStay(other);
		}

		protected override void OnTriggerExit(Collider other)
		{
			if (!_isStarted) return;

			base.OnTriggerExit(other);
		}

		public ObjectControllerWithPool TakeInstance()
		{
			return PoolManager.GetInstance().TakeInstanceAsComponent(this);
		}

		public void WakeUp()
		{
			_isStarted = false;
			_isInDetector = false;
			isHandled = false;
			isCollected = false;
			isDiedTime = false;
			gameObject.layer = defaultLayer;
			rigidBody.isKinematic = false;
			gameObject.SetActive(true);
		}

		public override void Sleep()
		{
			PoolManager.GetInstance().AddObject(this);
		}
	}
}