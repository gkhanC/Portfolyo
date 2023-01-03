using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Vfx
{
    public class EnemyDragonDieVfx : EnemyDieVfx, IPoolObject<EnemyDragonDieVfx>
    {
        public override EnemyDieVfx TakeVFX()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyDragonDieVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}