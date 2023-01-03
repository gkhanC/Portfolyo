using CharacterControllers;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Abstract;
using UnityEngine;

namespace Upgrades
{
    public class EnergyUpgrade : UpgradeBase, IPoolObject<EnergyUpgrade>
    {
        [field: SerializeField] public float energyBonus { get; private set; } = 100f;

        public override GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public override void MakeReady()
        {
            base.MakeReady();
            Sleep();
        }

        public EnergyUpgrade TakeInstance()
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
            player.SetEnergy(energyBonus);
            Destroy(this.gameObject);
        }
    }
}