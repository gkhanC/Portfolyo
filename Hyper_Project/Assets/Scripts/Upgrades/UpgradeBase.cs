using HypeFire.Library.Utilities.Extensions.Object;
using HyperProject.Abstract;
using Managers;
using UnityEngine;
using Upgrades.Abstract;


namespace Upgrades
{
    public class UpgradeBase : MonoBehaviour, IUpgrade
    {
        public GameObject upgradeObject;
        public GameObject uiCanvas;

        private float _timer;
        private Vector3 _upgrade_object_position;

        [field: SerializeField] public ParticleSystem particle { get; set; } = null;
        [field: SerializeField] private float dissolveTime { get; set; } = 2.5f;
        [field: SerializeField] public AnimationCurve dissolveCurve { get; set; } = new AnimationCurve();

        private ParticleSystemRenderer _particle_system_renderer;

        private bool _is_dissolve_enable;

        [field: SerializeField] private bool useDissolve { get; set; } = false;
        [field: SerializeField] private float dissolveDelayTime { get; set; } = 5f;
        private float _dissolve_delay_timer;
        private float _dissolve_timer;
        private Material _dissolve;

        [field: SerializeField] private float moveSpeed { get; set; } = 35f;
        [field: SerializeField] private AnimationCurve moveSpeedCurve;
        private float _move_speed_timer;

        private void FixedUpdate()
        {
            RotateCanvas();

            _timer += Time.deltaTime * 3f;
            _upgrade_object_position = upgradeObject.transform.position;
            _upgrade_object_position.y = .5f + (.25f * Mathf.Sin(_timer));
            upgradeObject.transform.position = _upgrade_object_position;

            if (useDissolve)
            {
                if (_is_dissolve_enable)
                {
                    _dissolve_delay_timer += Time.deltaTime;

                    if (_dissolve_delay_timer > dissolveDelayTime)
                    {
                        _dissolve_timer += Time.deltaTime;
                        var amount = _dissolve_timer / dissolveTime;
                        _dissolve.SetFloat("dissolve_amount", dissolveCurve.Evaluate(amount));

                        if (_dissolve.GetFloat("dissolve_amount") >= 1f)
                        {
                            MakeReady();
                        }
                    }
                }
            }
            else
            {
                MoveToPlayer();
            }
        }

        private Vector3 dir = Vector3.zero;
        private Transform target;

        public void MoveToPlayer()
        {
            if (target != GameManager.GetInstance().player.controllerObject.transform)
            {
                target = GameManager.GetInstance().player.controllerObject.transform;
            }

            if (transform.parent != target)
            {
                transform.SetParent(target);
            }

            _move_speed_timer += _move_speed_timer < 1f ? Time.deltaTime * .7f : 1f;
            dir = target.position - transform.position;
            transform.Translate(dir.normalized * (moveSpeedCurve.Evaluate(_move_speed_timer) * moveSpeed) *
                                Time.deltaTime);

            var distance = Vector3.Distance(transform.position, target.position);

            if (distance <= 1.5f)
            {
                Apply(GameManager.GetInstance().player);
            }
        }

        public void RotateCanvas()
        {
            var campos = Camera.main.transform.position;

            if (GameManager.GetInstance().player.controllerObject.transform.position.z > transform.position.z - 12f)
            {
                campos.z = transform.position.z - 12f;
            }

            var rot = campos - transform.position;
            uiCanvas.transform.rotation =
                Quaternion.RotateTowards(uiCanvas.transform.rotation, Quaternion.LookRotation(rot, Vector3.up),
                    180f);
        }

        public virtual void DissolveEnable()
        {
            if (particle.IsNull())
                particle = GetComponent<ParticleSystem>();

            _particle_system_renderer = particle.GetComponent<ParticleSystemRenderer>();
            _dissolve = _particle_system_renderer.material;

            if (_dissolve.IsNotNull())
            {
                _dissolve_timer = 0f;
                _dissolve_delay_timer = 0f;
                _dissolve.SetFloat("dissolve_amount", dissolveCurve.Evaluate(0f));
                _is_dissolve_enable = true;
            }
        }

        public virtual void MakeReady()
        {
            _is_dissolve_enable = false;
            _move_speed_timer = 0f;
            target = GameManager.GetInstance().player.controllerObject.transform;
        }

        public virtual GameObject TakeGameObject()
        {
            return this.gameObject;
        }

        public virtual void Apply(IPlayer player)
        {
            return;
        }
    }
}