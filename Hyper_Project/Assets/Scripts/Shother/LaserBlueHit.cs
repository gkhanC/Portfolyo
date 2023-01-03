using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using UnityEngine;

namespace HyperProject.Shoother
{
    public class LaserBlueHit : HitVfx, IPoolObject<LaserBlueHit>
    {
        public override GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public new LaserBlueHit TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }

        public new void Sleep()
        {
            sleepEvent.Invoke();
            PoolManager.GetInstance().AddObject(this);
        }
    }
}