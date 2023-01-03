using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace HyperProject.Shoother
{
    public class VioletSpellBullet : Bullet, IPoolObject<VioletSpellBullet>
    {
        public new VioletSpellBullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}