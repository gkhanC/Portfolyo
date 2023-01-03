using System;
using Enemies;
using HypeFire.Library.Controllers.InputControllers;
using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Health;
using HypeFire.Library.Utilities.Extensions.Object;
using HyperProject.Abstract;
using HyperProject.Shoother;
using Managers;
using UI;
using UnityEngine;

namespace CharacterControllers
{
    [RequireComponent(typeof(RotateController), typeof(RigidbodyMove), typeof(ShotController))]
    public class PlayerController : MonoBehaviour, IInputListener, IPlayer
    {
        public LayerMask enemyLayerMask;
        [field: SerializeField] public float speed { get; private set; } = 400f;
        [field: SerializeField] public int score { get; private set; } = 0;
        [field: SerializeField] public Health health { get; set; } = null;
        [field: SerializeField] private RotateController characterRotateController { get; set; }

        private bool _is_shot_module_busy;
        private bool _is_on_ınput;
        private bool _is_on_hit;
        private float _main_speed;
        private float speedMultiplier => speed / _main_speed;
        private float _target_animation_direction = -1f;
        private Vector3 _move_direction;
        private GameObject _target;
        private GameObject _last_hit;

        private RotateController _rotate_controller;
        private RigidbodyMove _move_controller;

        [SerializeField] private ParticleSystem _run_dust_vfx;

        private float speedPercent => (_move_direction.magnitude * speed) / _main_speed;

        [field: SerializeField] public ShotController shotController { get; private set; }
        [field: SerializeField] public CharacterAnimator characterAnimator { get; private set; } = null;

        [field: SerializeField] private float _enegery { get; set; } = 0f;
        [field: SerializeField] private ParticleSystem _speed_upgrade_feedback { get; set; } = null;
        [field: SerializeField] private ParticleSystem _attackSpeed_upgrade_feedback { get; set; } = null;
        [field: SerializeField] private ParticleSystem _hp_upgrade_feedback { get; set; } = null;
        [field: SerializeField] private ParticleSystem _energy_upgrade_feedback { get; set; } = null;

        public GameObject controllerObject { get; private set; }
        public float shotRate { get; private set; } = .25f;

        private void Awake()
        {
            controllerObject = gameObject;
            GameManager.GetInstance().player = this;
            _move_controller = GetComponent<RigidbodyMove>();
            _move_controller.Init(speed, transform.TransformDirection(Vector3.forward));
            _rotate_controller = GetComponent<RotateController>();
            _rotate_controller.Init(180f, Vector3.zero, Vector3.up);
            _main_speed = speed;
            shotController = GetComponent<ShotController>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            shotController.SetSpeedMultiplier(speedMultiplier);
            InputManager.GetInstance().Listeners.AddListener(this.InputListening);
        }

        private void Update()
        {
            if (health.isLive)
            {
                _is_on_ınput = Input.GetButton("Fire1");
            }
            else
            {
                _is_on_ınput = false;
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_is_shot_module_busy)
                    characterAnimator.PlayShotAnimation();
            }
        }


        private void FixedUpdate()
        {
            if (_is_on_ınput)
            {
                _move_controller.Move(speed, _move_direction);

                if (!_run_dust_vfx.isPlaying)
                    _run_dust_vfx.Play();

                _target_animation_direction = 1f;

                var position = shotController._muzzle_object.transform.position;
                var direction = characterRotateController.transform.TransformDirection(Vector3.forward);

                if (Physics.Raycast(position, direction, out var hit, 50f, enemyLayerMask))
                {
                    if (hit.transform.TryGetComponent<Enemy>(out var e))
                    {
                        _last_hit = hit.transform.gameObject;
                        _is_on_hit = e._is_shown;
                    }
                    else
                    {
                        _last_hit = null;
                        _is_on_hit = false;
                    }
                }
                else
                {
                    _last_hit = null;
                    _is_on_hit = false;
                }

                if (_target.IsNotNull() && _target.activeSelf)
                {
                    var e = _target.GetComponent<Enemy>();
                    if (e.IsNotNull() && e._is_shown)
                    {
                        characterRotateController.RotateToPosition(_target.transform.position);

                        if (_is_on_hit)
                        {
                            if (!_is_shot_module_busy)
                            {
                                characterAnimator.PlayShotAnimation();
                            }
                        }
                    }
                    else
                    {
                        if (_move_direction.magnitude > 0f)
                            characterRotateController.RotateWithDirection(_move_direction);
                    }
                }
                else
                {
                    if (_is_on_hit)
                    {
                        if (!_is_shot_module_busy)
                        {
                            characterAnimator.PlayShotAnimation();
                        }
                    }

                    if (_move_direction.magnitude > 0f)
                        characterRotateController.RotateWithDirection(_move_direction);
                }
            }
            else
            {
                if (_move_controller.isMoving)
                {
                    _move_controller.Stop();
                    _run_dust_vfx.Stop();
                }

                _target_animation_direction = 0f;
            }

            characterAnimator.SetRunAnimationParameter(_target_animation_direction);
            characterAnimator.SetRunSpeedAnimationParameter(speedPercent);
        }

        public void InputListening(IInputResult result)
        {
            _move_direction = new Vector3(result.data.direction.x, 0f, result.data.direction.y);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void SetHpUi() => UIController.GlobalAccess.hpText.text = "HP: " + health.hp.current.ToString("0.#");

        public void Damage(float damageValue)
        {
            health.Damage(damageValue);
            SetHpUi();

            if (DamageMaterialEffect.GlobalAccess != null)
            {
                DamageMaterialEffect.GlobalAccess.DamageEffect(.1f);
            }

            if (DamageVintage.GlobalAccess.IsNotNull())
            {
                DamageVintage.GlobalAccess.OnVintage();
            }

            if (ScreenShakeManager.GloballAccess != null)
                ScreenShakeManager.GloballAccess.Shake(3f, 1f, .1f);

            characterAnimator.PlayDamageAnimation(true);
            if (!health.isLive)
            {
                characterAnimator.PlayDieAnimation();
            }
        }

        public void AddScore(int scoreValue)
        {
            this.score += scoreValue;
            UIController.GlobalAccess.UpdateScore(score);
        }

        public void SetAttackSpeed(float attackSpeedValue)
        {
            shotRate *= attackSpeedValue;
            _attackSpeed_upgrade_feedback.Play();
        }

        public void SetCharacterSpeed(float characterSpeedValue)
        {
            speed += characterSpeedValue;
            _move_controller.speed = speed;
            _speed_upgrade_feedback.Play();
            shotController.SetSpeedMultiplier(speedMultiplier);
            UIController.GlobalAccess.UpdateSpeed(_move_controller.speed);
        }

        public void SetEnergy(float energyValue)
        {
            _enegery += energyValue;
            _energy_upgrade_feedback.Play();
            UIController.GlobalAccess.UpdateEnergy(_enegery);
        }

        public void SetHp(float hpValue)
        {
            var hp = health.hp;
            _hp_upgrade_feedback.Play();
            if (hp.current < hp.max)
            {
                health.Heal(hpValue);
                SetHpUi();
                return;
            }

            health.hp.current = health.hp.max;
            SetHpUi();
        }

        public void ShotInvoke()
        {
            _is_shot_module_busy = true;
        }

        public void OnShot()
        {
            var bullet = shotController.TakeBullet();
            if (_last_hit.IsNotNull())
            {
                bullet.transform.LookAt(_last_hit.transform);
            }
        }

        public void ShotCompleted()
        {
            _is_shot_module_busy = false;
        }
    }
}