using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using UnityEngine;

namespace HyperProject.Vfx
{
    public class PoolObjectVfx : MonoBehaviour, IPoolObject<PoolObjectVfx>
    {
        public float destroyTime = .5f;

        private bool _isInvoked;
        public float _timer;

        public virtual void FixedUpdate()
        {
            if (_isInvoked)
            {
                _timer += Time.deltaTime;
                if (_timer >= destroyTime)
                    Sleep();
            }
        }

        public virtual PoolObjectVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }

        public virtual void WakeUp()
        {
            _timer = 0f;
            _isInvoked = true;
        }

        public virtual void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }
    }
}