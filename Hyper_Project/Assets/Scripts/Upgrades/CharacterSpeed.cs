using CharacterControllers;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Abstract;
using UnityEngine;

namespace Upgrades
{
    public class CharacterSpeed : UpgradeBase, IPoolObject<CharacterSpeed>
    {
        [field: SerializeField] public float speedBonus { get; private set; } = 100f;

        public override GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public override void MakeReady()
        {
            base.MakeReady();
            Sleep();
        }

        public CharacterSpeed TakeInstance()
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
            player.SetCharacterSpeed(speedBonus);
            Destroy(this.gameObject);
        }
    }
}