using CharacterControllers;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Abstract;
using UnityEngine;

namespace Upgrades
{
    public class HPUpgrade : UpgradeBase, IPoolObject<HPUpgrade>
    {
        [field: SerializeField] public float healthBonus { get; private set; } = 1f;

        public override GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public override void MakeReady()
        {
            base.MakeReady();
            Sleep();
        }

        public HPUpgrade TakeInstance()
        {
            var takeInstance = PoolManager.GetInstance().TakeInstanceAsComponent(this);
            takeInstance.WakeUp();
            return takeInstance;
        }

        public void WakeUp()
        {
            DissolveEnable();
        }

        public void Sleep()
        {
            PoolManager.GetInstance().AddObject(this);
        }

        public override void Apply(IPlayer player)
        {
            player.SetHp(healthBonus);
            Destroy(this.gameObject);
        }
    }
}