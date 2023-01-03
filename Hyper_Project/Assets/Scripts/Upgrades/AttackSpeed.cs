using System;
using CharacterControllers;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Abstract;
using Managers;

namespace Upgrades
{
    using UnityEngine;

    public class AttackSpeed : UpgradeBase, IPoolObject<AttackSpeed>
    {
        [Range(.1f, .9f), SerializeField] private float _rate_bonus = .1f;
        public float rateBonus => (1f - _rate_bonus);

        public override GameObject TakeGameObject()
        {
            return TakeInstance().gameObject;
        }

        public override void MakeReady()
        {
            base.MakeReady();
            Sleep();
        }

        public AttackSpeed TakeInstance()
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
            player.SetAttackSpeed(rateBonus);
            Destroy(this.gameObject);
        }
    }
}