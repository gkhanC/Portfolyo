using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Enemies
{
    public class EnemyOrc : Enemy, IPoolObject<EnemyOrc>
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

        public EnemyOrc TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}