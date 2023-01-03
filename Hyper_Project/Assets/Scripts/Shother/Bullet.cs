using System;
using System.Collections;
using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.ObjectPool;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HyperProject.Abstract;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using Vfx;
using Random = UnityEngine.Random;

namespace HyperProject.Shoother
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPoolObject<Bullet>
    {
        [field: SerializeField, Range(0f, 100f)]
        private float accuracy { get; set; } = 50f;

        [field: SerializeField] private float fireRate { get; set; } = 1f;

        [field: SerializeField] private float _speed = 50f;
        [field: SerializeField] private float _speedMultiplier = 0f;

        private float speed
        {
            get => _speed + (_speedMultiplier * _speed);
            set => _speed = value;
        }

        [field: SerializeField] private Rigidbody _rigidbody;
        [field: SerializeField] public GameObject muzzlePrefab { get; set; } = null;

        [field: SerializeField] private bool isHitEffectEnableWhenEnemyHit { get; set; } = false;
        [field: SerializeField] private HitVfx hitVFXPrefab { get; set; } = null;
        public List<GameObject> trails;

        private float _check_delay = 2f;
        private bool _is_woke;
        private float _check_timer;

        private Vector3 offset { get; set; } = new Vector3();
        private bool _is_show;
        private Rect _screen = new Rect();

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        protected void Start()
        {
            if (_rigidbody.IsNotNull())
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            _check_timer = _check_delay;
            _is_woke = true;

            CalcAccuracy();

            _screen.width = Screen.width;
            _screen.height = Screen.height;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.TransformDirection(Vector3.forward) * speed;

            if (_is_woke)
            {
                _check_timer -= Time.deltaTime;

                if (_check_timer <= 0f)
                {
                    var screenPos = _cam.WorldToScreenPoint(transform.position);
                    _is_show = (_screen.Contains(screenPos));

                    if (!_is_show) Sleep();
                }
            }
        }

        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = multiplier;


        public void CalcAccuracy()
        {
            if (accuracy != 100f)
            {
                accuracy = 1f - (accuracy / 100f);

                for (int i = 0; i < 2; i++)
                {
                    var rand = 1f - Random.Range(-accuracy, accuracy);
                    var index = Random.Range(0, 2);
                    if (i == 0)
                    {
                        if (index == 0)
                        {
                            offset = new Vector3(0, -rand, 0);
                        }
                        else
                        {
                            offset = new Vector3(0, rand, 0);
                        }
                    }
                    else
                    {
                        if (index == 0)
                        {
                            offset = new Vector3(0, offset.y, -rand);
                        }
                        else
                        {
                            offset = new Vector3(0, offset.y, rand);
                        }
                    }
                }
            }
        }

        public void CreatePool()
        {
            PoolManager.GetInstance().CreateObjectPool(this);
        }

        public Bullet TakeInstance()
        {
            return PoolManager.GetInstance().TakeInstanceAsComponent(this);
        }

        public void Explode(ContactPoint contact, bool isContactEnemy = false)
        {
            _rigidbody.isKinematic = true;

            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (hitVFXPrefab != null)
            {
                if (isContactEnemy && !isHitEffectEnableWhenEnemyHit)
                    return;

                var hitVFX = PoolManager.GetInstance().TakeInstanceAsComponent(hitVFXPrefab);
                var transform1 = hitVFX.transform;
                transform1.position = pos;
                transform1.rotation = rot;
                hitVFX.WakeUp();

                var ps = hitVFX.GetComponent<ParticleSystem>();
                if (ps == null)
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    hitVFX.lifeTime = psChild.main.duration;
                }
                else
                {
                    hitVFX.lifeTime = ps.main.duration;
                }
            }
        }


        public void LaunchMuzzle(Transform muzzleT)
        {
            if (muzzlePrefab.IsNotNull())
            {
                var vfx = Instantiate(muzzlePrefab, muzzleT.position, quaternion.identity);
                vfx.transform.SetParent(muzzleT);
                vfx.transform.forward = transform.forward + offset;

                var particle = vfx.GetComponent<ParticleSystem>();
                if (particle.IsNotNull())
                    Debug.Log("not null çaılştı");

                if (particle != null)
                {
                    Debug.Log("muzzke" + particle.gameObject.name);
                    //Destroy(vfx, particle.main.duration);
                }
                else
                {
                    var particleChild = vfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(vfx, particleChild.main.duration);
                }
            }
        }

        public void WakeUp()
        {
            _rigidbody.isKinematic = false;
            _check_timer = _check_delay;
            _is_woke = true;

            if (trails.Count > 0)
            {
                for (int i = 0; i < trails.Count; i++)
                {
                    var trail = trails[i].GetComponent<TrailRenderer>();
                    trail.enabled = true;
                }
            }
        }

        public void Sleep()
        {
            _is_woke = false;
            TrailOff();
            PoolManager.GetInstance().AddObject(this);
        }

        public void TrailOff()
        {
            if (trails.Count > 0)
            {
                for (int i = 0; i < trails.Count; i++)
                {
                    var trail = trails[i].GetComponent<TrailRenderer>();
                    trail.Clear();
                    trail.enabled = false;

                    var particle = trails[i].GetComponent<ParticleSystem>();
                    if (particle != null)
                    {
                        particle.Stop();
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals("Bullet"))
            {
                var isEnemy = false;
                if (collision.gameObject.TryGetComponent(out IDamageAble iDamageAble))
                {
                    iDamageAble.TakeDamage();
                    isEnemy = true;
                }


                Explode(collision.contacts[0], isEnemy);
                Sleep();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}