using System.Collections.Generic;
using CharacterControllers;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public float spawnOffset = 15f;

        [SerializeField] public List<EnemySpawner> _enemy_container = new List<EnemySpawner>();

        private void Start()
        {
            foreach (var enemySpawner in _enemy_container)
            {
                enemySpawner.Init(GameManager.GetInstance().player.controllerObject.transform);
                StartCoroutine(enemySpawner.main_coroutine);
            }
        }
    }
}