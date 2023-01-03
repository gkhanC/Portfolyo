using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        private int _enemy_kill_count = 0;

        /// <summary>
        /// Enemy kill sayısı dinleyen event
        /// parametre toplam kill sayısını ifade eder
        /// </summary>
        [field: SerializeField]
        public UnityEvent<int> enemyKillCountListeners { get; set; } = new UnityEvent<int>();


        private void Start()
        {
            GameManager.GetInstance().eventManager = this;
        }

        public void EnemyKillReport(int killCount)
        {
            _enemy_kill_count++;
            enemyKillCountListeners.Invoke(_enemy_kill_count);
        }
    }
}