using System;
using System.Linq;
using Enemies;
using Managers;

namespace CharacterControllers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerDetector : MonoBehaviour
    {
        public bool isSorting;
        public List<Enemy> enemies = new List<Enemy>();


        private void Start()
        {
            GameManager.GetInstance().enemyDetector = this;
            StartCoroutine(Sort());
        }

        public void AddEnemy(Enemy e)
        {
            if (enemies.Contains(e))
                return;

            enemies.Add(e);
        }

        public void RemoveEnemy(Enemy e)
        {
            if (!enemies.Contains(e))
                return;


            enemies.Remove(e);
            enemies.TrimExcess();
        }

        public IEnumerator Sort()
        {
            while (true)
            {
                if (enemies.Count > 0)
                {
                    enemies.Sort((x, y) =>
                    {
                        var dist1 = Vector3.Distance(x.transform.position,
                            GameManager.GetInstance().player.controllerObject.transform.position);
                        var dist2 = Vector3.Distance(y.transform.position,
                            GameManager.GetInstance().player.controllerObject.transform.position);

                        if (dist1 > dist2) return 1;
                        return -1;
                    });

                    GameManager.GetInstance().player.SetTarget(enemies[0].gameObject);
                }

                yield return new WaitForSeconds(.05f);
            }
        }
    }
}