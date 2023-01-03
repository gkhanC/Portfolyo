using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Vfx
{
    public class EnemyEvilMageDieVfx : EnemyDieVfx, IPoolObject<EnemyEvilMageDieVfx>
    {
        public override EnemyDieVfx TakeVFX()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyEvilMageDieVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}