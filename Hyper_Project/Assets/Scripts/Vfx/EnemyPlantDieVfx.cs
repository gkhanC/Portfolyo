using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Vfx
{
    public class EnemyPlantDieVfx: EnemyDieVfx, IPoolObject<EnemyPlantDieVfx>
    {
        public override EnemyDieVfx TakeVFX()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyPlantDieVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}