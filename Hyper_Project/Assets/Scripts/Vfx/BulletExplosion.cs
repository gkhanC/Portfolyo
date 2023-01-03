using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using UnityEngine;

namespace Vfx
{
    public class BulletExplosion : MonoBehaviour, IPoolObject<BulletExplosion>
    {
        public float destroyTime = .5f;

        private bool is_invoked;
        public float _timer;

        public virtual void FixedUpdate()
        {
            if (is_invoked)
            {
                _timer += Time.deltaTime;
                if (_timer >= destroyTime)
                    Sleep();
            }
        }

        public virtual BulletExplosion TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }

        public virtual void WakeUp()
        {
            _timer = 0f;
            is_invoked = true;
        }

        public virtual void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }
    }
}