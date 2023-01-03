using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Enemies
{
    public class EnemyRat : Enemy, IPoolObject<EnemyRat>
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

        public EnemyRat TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}