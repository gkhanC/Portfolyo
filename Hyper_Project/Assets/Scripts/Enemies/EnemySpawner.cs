using System;
using System.Collections;
using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Object;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [Serializable]
    public class EnemySpawner
    {
        [Range(0f, 20f)] public float spawnRate = .5f;

        public float range = 1.5f;

        public List<Enemy> enemyPrefabs;
        public Transform spawnCenter;

        public bool isInıt;

#nullable enable
        private Enemy? instance = null;
#nullable disable
        private int index = 0;

        public IEnumerator main_coroutine;
        private IEnumerator _coroutine;

        private float corutineTime;

        public void Init(Transform sCenter)
        {
            corutineTime = (spawnRate - .5f) / (float)enemyPrefabs.Count;
            isInıt = true;
            spawnCenter = sCenter;
            main_coroutine = SpawnCoroutine();
            _coroutine = SpawnAll();
        }

        public Vector3 GetSpawnPosition()
        {
            return GameManager.GetInstance().playerSpawner.GetSpawnPosition();
        }

        private IEnumerator SpawnCoroutine()
        {
            while (isInıt)
            {
                if (GameManager.GetInstance().enemyCount <= GameManager.GetInstance().enemyConstraint)
                {
                    instance = null;
                    var spawnPos = GetSpawnPosition();
                    var pos = Vector3.zero;

                    while (index < enemyPrefabs.Count)
                    {
                        instance = enemyPrefabs[index].TakeEnemyInstance();
                        pos = Random.insideUnitCircle * range;

                        if (instance.IsNotNull())
                        {
                            instance.transform.position = spawnPos + pos;
                            GameManager.GetInstance().enemyCount++;
                            instance.WakeUp();
                        }

                        index++;

                        yield return new WaitForSeconds(.2f);
                    }
                }

                index = 0;
                yield return new WaitForSeconds(spawnRate);
            }

            yield return null;
        }

        public IEnumerator SpawnAll()
        {
            yield return null;
        }
    }
}