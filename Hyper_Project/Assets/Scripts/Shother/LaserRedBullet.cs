using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace HyperProject.Shoother
{
    public class LaserRedBullet : Bullet, IPoolObject<LaserRedBullet>
    {
        public new LaserRedBullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}