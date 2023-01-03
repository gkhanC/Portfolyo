using System;
using System.Collections.Generic;
using HyperProject.Shoother;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        [Range(0f, 1f)] public float upgradeSpawnChance = 0.5f;
        public int upgradeChanceCondition = 3;

        [field: SerializeField]
        public List<UpgradeBase> upgradesWithChance { get; private set; } = new List<UpgradeBase>();

        [field: SerializeField]
        public List<ConditionalUpgradeSlot> conditionalUpgrades { get; private set; } =
            new List<ConditionalUpgradeSlot>();

        private int _condition = 0;

        private void Start()
        {
            GameManager.GetInstance().upgradeManager = this;
        }

        public void CallUpgrade(Vector3 position)
        {
            if (conditionalUpgrades.Count > 0)
            {
                if (conditionalUpgrades[0].condition <= _condition)
                {
                    var up = conditionalUpgrades[0].upgrade.TakeGameObject();
                    up.transform.position = position;
                    conditionalUpgrades.RemoveAt(0);
                    _condition++;
                    return;
                }

                return;
            }

            if (upgradesWithChance.Count <= 0)
                return;

            _condition++;

            if (upgradeSpawnChance > _condition)
                return;

            var chance = Random.Range(0f, 1f);  

            if (chance > upgradeSpawnChance)
                return;

            var rand = Random.Range(0, upgradesWithChance.Count);
            var upgrade = upgradesWithChance[rand].TakeGameObject();

            upgrade.transform.position = position;
            _condition = 0;
        }

        [Serializable]
        public class ConditionalUpgradeSlot
        {
            public int condition = 0;
            public UpgradeBase upgrade;
        }
    }
}