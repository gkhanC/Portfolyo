using System;
using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Object;
using HyperProject.Shoother.Abstract;
using Unity.Mathematics;
using UnityEngine;

namespace HyperProject.Shoother
{
    public class ShotController : MonoBehaviour, IShotController
    {
        [field: SerializeField] public Bullet bulletPrefab { get; private set; }
        [field: SerializeField] public ShotModule shotModule { get; private set; }

        [field: SerializeField] public GameObject _muzzle_object;

        [field: SerializeField] private GameObject _gun_object;

        [field: SerializeField] private ParticleSystem _gun_vfx;

        [field: SerializeField] private List<Bullet> bullets { get; set; } = new List<Bullet>();
        [field: SerializeField] private float bulletSpeedMultiplier { get; set; } = 0f;

        private GameObject _muzzle_current;
        private MuzzleController _muzzle_controller;
        private float _speed_multiplier = 0f;

        //public float getFireRate => bulletPrefab.fireRate;

        private void Awake()
        {
            if (bulletPrefab.IsNull())
            {
                if (bullets.Count > 0)
                {
                    bulletPrefab = bullets[0];
                }
            }


            shotModule.SetShotController(this);
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            _speed_multiplier = multiplier;
        }

        public Bullet TakeBullet()
        {
            var bullet = bulletPrefab.TakeInstance();
            var bulletTransform = bullet.transform;

            bulletTransform.position = _muzzle_object.transform.position;
            bulletTransform.rotation = _muzzle_object.transform.rotation;
            bullet.SetSpeedMultiplier(_speed_multiplier);
            bullet.WakeUp();

            if (_muzzle_current.IsNull() || bullet.muzzlePrefab != _muzzle_current)
            {
                _muzzle_current = bullet.muzzlePrefab;
                _muzzle_controller = Instantiate(bullet.muzzlePrefab, _muzzle_object.transform)
                    .GetComponent<MuzzleController>();
                //_muzzle_current.transform.SetParent(_muzzle_object.transform);
            }

            if (_muzzle_controller.IsNotNull())
            {
                _muzzle_controller.muzzle_launch?.Invoke();
            }

            if (ScreenShakeManager.GloballAccess.IsNotNull())
            {
               // ScreenShakeManager.GloballAccess.Shake(1f, 1f);
            }

            // bullet.LaunchMuzzle(_muzzle_object.transform);
            return bullet;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var v = new VTypeShotModule(this);
                v.UpdateProjectileCount(22);
                shotModule = v;
            }
        }

        public void Shot()
        {
            shotModule.Shot();
        }

        public void UpdateShotModule(ShotModule module)
        {
            shotModule = module;
        }
    }
}