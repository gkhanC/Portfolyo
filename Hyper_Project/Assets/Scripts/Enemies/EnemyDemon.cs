using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Enemies
{
    public class EnemyDemon : Enemy, IPoolObject<EnemyDemon>
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

        public EnemyDemon TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}