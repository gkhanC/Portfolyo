using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace HyperProject.Shoother
{
    public class PinkGumBullet : Bullet, IPoolObject<PinkGumBullet>
    {
        public new PinkGumBullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}