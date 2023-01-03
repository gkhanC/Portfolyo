using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Enemies
{
    public class EnemyKnight : Enemy, IPoolObject<EnemyKnight>
    {
        public override Enemy TakeEnemyInstance()
        {
            return TakeInstance();
        }

        public override void Sleep()
        {
            base.Sleep();
            PoolManager.GetInstance().AddObject(this);
        }

        public EnemyKnight TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}