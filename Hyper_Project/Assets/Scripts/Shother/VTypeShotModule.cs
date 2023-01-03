using System;
using System.Collections.Generic;
using HyperProject.Shoother.Abstract;
using UnityEngine;

namespace HyperProject.Shoother
{
    [Serializable]
    public class VTypeShotModule : ShotModule
    {
       
        [field: SerializeField] public float angleDifference { get; protected set; } = 8f;

        private List<GameObject> _muzlles = new List<GameObject>();

        private int order = 1;
        private float multiplier = -1f;

        public override void Shot()
        {
            base.Shot();

            foreach (var VARIABLE in _muzlles)
            {
                var nBullet = shotController.TakeBullet();
                nBullet.transform.rotation = VARIABLE.transform.rotation;
            }
        }

        public void UpdateMuzzles()
        {
            var mRot = shotController._muzzle_object.transform.rotation;
            for (int i = 0; i < projectileCount && _muzlles.Count < projectileCount; i++)
            {
                var n = new GameObject();
                n.transform.SetParent(shotController._muzzle_object.transform);
                n.transform.position = Vector3.zero;

                n.transform.rotation =
                    Quaternion.Euler((mRot.eulerAngles + (Vector3.up * ((angleDifference * order) * multiplier))));
                n.name = "ShotModuleMuzzle";
                _muzlles.Add(n);

                multiplier *= -1;

                if (i != 0 && i % 2 == 0)
                    order += 1;
            }
        }

        public void OnEnable()
        {
            UpdateMuzzles();
        }

        public void UpdateProjectileCount(int bCount)
        {
            projectileCount = bCount;
            UpdateMuzzles();
        }

        public VTypeShotModule()
        {
        }

        public VTypeShotModule(ShotController shotC)
        {
            shotController = shotC;
        }
    }
}