using System;
using System.Collections.Generic;
using Guns.Abstract;
using HypeFire.Library.Utilities.Extensions.Object;
using Managers;
using UnityEngine;

namespace Guns
{
    public class GunStorage : MonoBehaviour, IGunStorage
    {
        public IGun currentGun;
        [field: SerializeField] private List<GunBase> _guns = new List<GunBase>();
        [field: SerializeField] private Dictionary<GunType, IGun> _gun_storage = new Dictionary<GunType, IGun>();

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            foreach (var VARIABLE in _guns)
            {
                if (currentGun.IsNull() && VARIABLE.GunPrefab.activeSelf)
                    currentGun = VARIABLE;
                else if (VARIABLE.GunPrefab.activeSelf)
                    VARIABLE.GunPrefab.SetActive(false);

                _gun_storage.Add(VARIABLE.GunType, VARIABLE);
            }
        }

        public void ChangeGun(GunType gunType)
        {
            var slc = _gun_storage[gunType];

            if (slc.IsNotNull())
            {
                if (currentGun.GunType != gunType)
                    currentGun.GunPrefab.SetActive(false);

                currentGun = slc;
                slc.GunPrefab.SetActive(true);
            }

            GameManager.GetInstance().player.characterAnimator.SetGun(currentGun);
        }
    }
}