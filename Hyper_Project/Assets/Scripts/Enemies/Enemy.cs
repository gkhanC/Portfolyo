using CharacterControllers;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Logger;
using HyperProject.Abstract;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Vfx;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody), typeof(RigidbodyMove), typeof(RotateController))]
    public class Enemy : MonoBehaviour, IDamageAble
    {
        [field: SerializeField] public int grade { get; private set; } = 1;
        [field: SerializeField] public float speed { get; private set; } = 500f;
        [field: SerializeField] public float distanceConstraint { get; private set; } = 3f;

        [field: SerializeField] public float attackDamage { get; set; } = 1f;

        [field: SerializeField] public EnemyDieVfx enemy_die_vfx { get; private set; } = null;
        [field: SerializeField] public Animator animator { get; private set; } = null;

        private RigidbodyMove _move_controller;
        private RotateController _rotate_controller;

        [field: SerializeField] private IPlayer player { get; set; } = null;
        [field: SerializeField] private int score { get; set; } = 10;
        public NavMeshAgent agent;

        private bool _is_added_to_object_pool;
        private Rect _screen = new Rect();

        public Camera cam;

        //Animations settings
        private static readonly int _run_anim = Animator.StringToHash("run");
        private bool _is_run_animation_enabled = false;
        [FormerlySerializedAs("_is_show")] public bool _is_shown;
        private bool _is_ready;

        private bool runAnimation
        {
            set
            {
                if (value != _is_run_animation_enabled)
                {
                    if (animator.IsNotNull())
                        animator.SetBool(_run_anim, value);
                }

                _is_run_animation_enabled = value;
            }
        }

        private static readonly int _attack_anim = Animator.StringToHash("attack");
        private bool _is_attack_animation_enabled = false;

        private bool attackAnimation
        {
            get => _is_attack_animation_enabled;
            set
            {
                if (value != _is_attack_animation_enabled)
                {
                    if (animator.IsNotNull())
                        animator.SetBool(_attack_anim, value);
                }

                _is_attack_animation_enabled = value;
            }
        }

        private void Awake()
        {
            FindComponents();
        }

        private void Start()
        {
            cam = Camera.main;
            PrepareOfUse();
        }

        private void FixedUpdate()
        {
            if (_is_ready && !attackAnimation)
            {
                var position = player.controllerObject.transform.position;
                var myPosition = transform.position;
                var d = Vector3.Distance(myPosition, position);
                var inDistances = Vector3.Distance(myPosition, position) >= distanceConstraint;

                agent.enabled = isGrounded;

                runAnimation = inDistances;

                if (inDistances)
                {
                    if (agent.enabled)
                    {
                        runAnimation = true;
                        agent.SetDestination(position);
                    }
                }
                else if (!_is_attack_animation_enabled)
                {
                    Attack();
                }

                if (!_is_added_to_object_pool)
                {
                    var screenPos = cam.WorldToScreenPoint(transform.position);
                    _is_shown = (_screen.Contains(screenPos));
                }
            }
        }

        private void FindComponents()
        {
            _move_controller = GetComponent<RigidbodyMove>();
            _move_controller.Init(speed, transform.TransformDirection(Vector3.forward));

            _rotate_controller = GetComponent<RotateController>();
            _rotate_controller.Init(180f, Vector3.zero, Vector3.up);
        }

        private void PrepareOfUse()
        {
            player = GameManager.GetInstance().player;
            _screen.width = Screen.width;
            _screen.height = Screen.height;
            _is_ready = true;
        }

        public virtual Enemy TakeEnemyInstance()
        {
            return this;
        }


        public virtual void WakeUp()
        {
            _is_added_to_object_pool = false;
            _is_shown = false;

            _is_run_animation_enabled = false;
            _is_attack_animation_enabled = false;

            transform.rotation = Quaternion.identity;
        }

        public virtual void Sleep()
        {
            if (this.gameObject.IsNull())
                return;

            if (ScreenShakeManager.GloballAccess.IsNotNull())
            {
                ScreenShakeManager.GloballAccess.Shake(3f, 1f);
            }

            _is_shown = false;
            isGrounded = false;
            agent.enabled = false;
            GameManager.GetInstance().enemyCount--;
            GameManager.GetInstance().enemyDetector.RemoveEnemy(this);
            gameObject.SetActive(false);
        }

        public void OnDieVFX()
        {
            var dieVfxInstance = enemy_die_vfx.TakeVFX();
            dieVfxInstance.transform.position = transform.position;
            dieVfxInstance.transform.rotation = transform.rotation;
            dieVfxInstance.WakeUp();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                isGrounded = true;
            }

            //todo: Check if machine area exists
        }

        public bool isGrounded { get; set; } = false;

        private void OnTriggerStay(Collider other)
        {
            if (_is_shown)
            {
                if (!_is_added_to_object_pool && other.TryGetComponent(out PlayerDetector dec))
                {
                    GameManager.GetInstance().enemyDetector.AddEnemy(this);
                    _is_added_to_object_pool = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_is_added_to_object_pool && other.TryGetComponent(out PlayerDetector dec))
            {
                dec.RemoveEnemy(this);
                _is_added_to_object_pool = false;
            }
        }

        public void Attack()
        {
            attackAnimation = true;
        }

        public void AttackComplete()
        {
            attackAnimation = false;
            GameManager.GetInstance().player.Damage(attackDamage);
            HFLogger.Log(this, "Player damage.");
        }

        public void TakeDamage(float damage = 0f)
        {
            if (!_is_shown)
                return;

            GameManager.GetInstance().player.AddScore(score);

            OnDieVFX();
            GameManager.GetInstance().upgradeManager.CallUpgrade(transform.position);
            GameManager.GetInstance().eventManager.EnemyKillReport(grade);
            Sleep();
        }
    }
}