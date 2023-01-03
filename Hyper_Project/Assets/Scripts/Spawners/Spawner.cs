using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        public List<Transform> spawnPoints;

        private void Awake()
        {
            GameManager.GetInstance().playerSpawner = this;
            spawnPoints = transform.GetComponentsInChildren<Transform>().ToList();
            spawnPoints.Remove(this.transform);
        }

        public Vector3 GetSpawnPosition()
        {
            var randIndex = Random.Range(0, spawnPoints.Count);
            var pos = spawnPoints[randIndex].position;

            return pos;
        }
    }
}