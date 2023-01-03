using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Vfx
{
    public class EnemyBatDieVfx : EnemyDieVfx, IPoolObject<EnemyBatDieVfx>
    {
        public override EnemyDieVfx TakeVFX()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyBatDieVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}