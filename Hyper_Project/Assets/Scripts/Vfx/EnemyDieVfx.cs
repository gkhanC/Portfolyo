using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Vfx;
using UnityEngine;

namespace Vfx
{
    public class EnemyDieVfx : MonoBehaviour
    {
        public float destroyTime = .5f;
        
        private bool _is_invoked;
        protected float _timer;
        

        public virtual void FixedUpdate()
        {
            if (_is_invoked)
            {
                _timer += Time.deltaTime;
                if (_timer >= destroyTime)
                    Sleep();
            }
        }

        public virtual EnemyDieVfx TakeVFX()
        {
            return this;
        }

        public virtual void WakeUp()
        {
            _timer = 0f;
            _is_invoked = true;
        }

        public virtual void Sleep()
        {
        }
    }
}