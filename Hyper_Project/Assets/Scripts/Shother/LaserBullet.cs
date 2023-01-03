using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using UnityEngine;

namespace HyperProject.Shoother
{
    public class LaserBullet : Bullet, IPoolObject<LaserBullet>
    {
        public new LaserBullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}