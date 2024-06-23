using HypeFire.Library.Controllers.Rotate.Abstract;
using HypeFire.Library.Structures;
using HypeFire.Library.Utilities.Extensions.Vector;
using UnityEngine;

namespace HypeFire.Library.Controllers.Rotate
{
    [ExecuteInEditMode]
    public class RotateController : MonoBehaviour, IRotator
    {
        #region Fields

        [field: SerializeField] private bool _useSmoothRotateWithTime = true;

        [field: SerializeField] private float _rotateSpeed = 180f;

        [field: SerializeField] private Vector3 _rotationUpwards = Vector3.up;
        [field: SerializeField] private Vector3 _rotationConstraint = Vector3.up;

        private RotationField _rotationField = default;

        private bool _isStopTimeEnable = default;
        private float _stopTimer = default;
        private float _stopTime = default;

        #endregion

        #region Propertys

        protected float stopTimePercent => _stopTimer / _stopTime;

        protected float speedMultiplier
        {
            get
            {
                if (_isStopTimeEnable)
                    return stopTimePercent;

                return 1f;
            }
        }

        public bool isRotating { get; protected set; }

        public bool useSmoothRotateWithTime
        {
            get => _useSmoothRotateWithTime;
            set => _useSmoothRotateWithTime = value;
        }

        public float rotateSpeed
        {
            get => _rotateSpeed;
            set => _rotateSpeed = value;
        }

        public Vector3 rotationUpwards
        {
            get => _rotationUpwards;
            set => _rotationUpwards = value;
        }

        public Vector3 rotationConstraint
        {
            get => _rotationConstraint.FlipWithBoolean();
            set => _rotationConstraint = value;
        }

        public RotationField rotationField
        {
            get => _rotationField;
            set => _rotationField = value;
        }

        #endregion

        protected void Rotate(RotationField rot)
        {
            var speed = useSmoothRotateWithTime ? rotateSpeed * Time.deltaTime : rotateSpeed;

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(rot.euler.Multiplier(rotationConstraint), rotationUpwards),
                speed * speedMultiplier);

            isRotating = false;
        }

        public void RotateWithDirection(Vector3 direction)
        {
            rotationField = direction;
            Rotate(rotationField);
        }

        public void RotateToPosition(Vector3 position)
        {
            var direction = position - transform.position;
            rotationField = direction;
            Rotate(rotationField);
        }

        public void RotateWithQuaternion(Quaternion direction)
        {
            rotationField = direction;
            Rotate(rotationField);
        }

        public void LookAtPosition(Vector3 position)
        {
            var direction = position - transform.position;
            rotationField = direction;
            transform.LookAt(rotationField);
        }

        public void Init(float speed = 0f, Vector3 direction = default,
            Vector3 rotateUpwards = default)
        {
            rotateSpeed = speed;
            rotationField = direction;
            rotationUpwards = rotateUpwards;
        }
    }
}