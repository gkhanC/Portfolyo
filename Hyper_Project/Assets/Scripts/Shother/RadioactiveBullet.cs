using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace HyperProject.Shoother
{
    public class RadioactiveBullet : Bullet, IPoolObject<RadioactiveBullet>
    {
        public new RadioactiveBullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}