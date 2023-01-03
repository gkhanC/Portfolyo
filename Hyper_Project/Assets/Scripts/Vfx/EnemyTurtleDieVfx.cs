using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Vfx
{
    public class EnemyTurtleDieVfx : EnemyDieVfx, IPoolObject<EnemyTurtleDieVfx>
    {
        public override EnemyDieVfx TakeVFX()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyTurtleDieVfx TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}