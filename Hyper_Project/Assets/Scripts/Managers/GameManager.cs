using System;
using HypeFire.Library.Utilities.Singleton;
using CharacterControllers;
using HyperProject.Abstract;
using Spawners;
using UnityEngine;
using Upgrades;

namespace Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [field: SerializeField] public int enemyConstraint { get; set; } = 100;
        [field: SerializeField] public int enemyCount { get; set; } = 0;
        public IPlayer player { get; set; }
        public PlayerDetector enemyDetector { get; set; }
        public Spawner playerSpawner { get; set; }
        public UpgradeManager upgradeManager { get; set; }
        public EventManager eventManager { get; set; }

        private float score { get; set; } = 0;
    }
}