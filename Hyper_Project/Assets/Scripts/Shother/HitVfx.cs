using System;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace HyperProject.Shoother
{
    public class HitVfx : MonoBehaviour, IPoolObject<HitVfx>
    {
        public float lifeTime = 0f;
        private float _timer = 0f;

        public UnityEvent wakeEvent;
        public UnityEvent sleepEvent;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > lifeTime)
            {
                Sleep();
            }
        }

        public virtual GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public HitVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }

        public void WakeUp()
        {
            _timer = 0f;
            wakeEvent?.Invoke();
        }

        public void Sleep()
        {
            sleepEvent?.Invoke();
            PoolManager.GetInstance().AddObject(this);
        }
    }
}