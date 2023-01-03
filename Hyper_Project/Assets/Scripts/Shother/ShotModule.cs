using System;
using System.Collections.Generic;
using HyperProject.Shoother.Abstract;
using Unity.VisualScripting;
using UnityEngine;

namespace HyperProject.Shoother
{
    [Serializable]
    public class ShotModule : IShotModule
    {
        [field: SerializeField] public int projectileCount { get; protected set; } = 0;
        public ShotController shotController { get; protected set; } = null;

        public void SetShotController(IShotController shotCont)
        {
            this.shotController = (ShotController)shotCont;
        }

        public virtual void Shot()
        {
            shotController.TakeBullet();
        }

        public ShotModule()
        {
        }

        public ShotModule(IShotController shotCont)
        {
            SetShotController(shotCont);
        }
    }
}