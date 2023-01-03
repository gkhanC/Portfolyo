using HypeFire.Library.Controllers.Move.Abstract;
using HypeFire.Library.Structures;
using HypeFire.Library.Utilities.Extensions.Vector;
using UnityEngine;

namespace HypeFire.Library.Controllers.Move
{
    [RequireComponent(typeof(Rigidbody)), ExecuteInEditMode]
    public class RigidbodyMove : MonoBehaviour, IRigidbodyMoveController
    {
        #region Fields

        [field: SerializeField] private bool _isAutoMoveEnabled;

        [field: SerializeField] private float _speed = default;


        [Tooltip("0 değeri hareket esnasında, fizik tarafından vektör yönüne uygulanan kuvvetleri geçersiz kılar.")]
        [field: SerializeField]
        private Vector3 _physicalOverriding = new Vector3(0f, 1f, 0f);

        [field: SerializeField] private RotationField _rotationField = default;


        private bool _isStopTimeEnable = default;

        private float _stopTime = default;
        private float _stopTimer = default;

        private Rigidbody _rigidbody;

        #endregion

        #region Propertys

        protected float stopTimePercent => _stopTimer / _stopTime;

        public bool isMoving { get; private set; }

        public bool isAutoMoveEnabled
        {
            get => _isAutoMoveEnabled;
            set => _isAutoMoveEnabled = value;
        }

        public float speed
        {
            get => _speed;
            set => _speed = value;
        }

        public RotationField direction
        {
            get => _rotationField;
            set => _rotationField = value;
        }

        public Vector3 physicalOverriding
        {
            get => _physicalOverriding;
            set => _physicalOverriding = value;
        }

        public Vector3 velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }

        #endregion

        public void OnMove()
        {
            isAutoMoveEnabled = true;
        }

        public void Moving()
        {
            isMoving = true;

            velocity = ApplyDirectionConstraint() +
                       ((direction.euler.Multiplier(physicalOverriding.FlipWithBoolean())) * speed * Time.deltaTime);
        }

        public void Move(float speed = 0f, Vector3 direction = default)
        {
            this.speed = speed;
            this.direction = direction;
            Moving();
        }

        public void Stop()
        {
            isAutoMoveEnabled = false;
            isMoving = false;
            velocity = ApplyDirectionConstraint() + Vector3.zero;
        }

        public void Stop(float stopTime)
        {
            _stopTime = stopTime;
            _stopTimer = stopTime;
            _isStopTimeEnable = true;
        }

        public void UpdateDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        private Vector3 ApplyDirectionConstraint() => velocity.Multiplier(physicalOverriding);


        private void OnStopTime()
        {
            _stopTimer = _stopTimer > 0 ? _stopTimer - Time.deltaTime : 0f;
            velocity = ApplyDirectionConstraint() +
                       ((direction.euler.Multiplier(physicalOverriding.FlipWithBoolean())) *
                        (speed * stopTimePercent) * Time.deltaTime);

            if (_stopTimer <= 0f)
            {
                isMoving = false;
                velocity = ApplyDirectionConstraint() + Vector3.zero;
                isAutoMoveEnabled = false;
                _isStopTimeEnable = false;
                _stopTimer = 0f;
                _stopTime = 0f;
            }
        }

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isMoving && _isStopTimeEnable)
            {
                OnStopTime();
                return;
            }

            if (isAutoMoveEnabled)
            {
                Moving();
            }
        }

        public void Init(float speed = 0f, Vector3 direction = default)
        {
            this.speed = speed;
            this.direction = direction;
        }
    }
}