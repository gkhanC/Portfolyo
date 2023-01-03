using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;

namespace Enemies
{
    public class EnemySpector : Enemy, IPoolObject<EnemySpector>
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

        public EnemySpector TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }
    }
}