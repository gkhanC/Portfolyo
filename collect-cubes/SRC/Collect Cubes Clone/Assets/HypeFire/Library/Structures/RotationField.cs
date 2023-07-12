using System;
using UnityEngine;

namespace HypeFire.Library.Structures
{
    [Serializable]
    public struct RotationField
    {
        [field: SerializeField] public Vector3 euler;

        private Quaternion quaternion => Quaternion.Euler(euler);

        public RotationField(Vector3 euler)
        {
            this.euler = euler;
        }

        public RotationField(Quaternion quaternion)
        {
            euler = quaternion.eulerAngles;
        }

        public static implicit operator Quaternion(RotationField field)
        {
            return field.quaternion;
        }

        public static implicit operator Vector3(RotationField field)
        {
            return field.euler;
        }

        public static implicit operator string(RotationField field)
        {
            return field.euler.ToString("0.##");
        }

        public static implicit operator RotationField(Vector3 euler)
        {
            return new RotationField(euler);
        }

        public static implicit operator RotationField(Quaternion quaternion)
        {
            return new RotationField(quaternion);
        }

        public static RotationField operator *(RotationField rotationField, float f)
        {
            var v = rotationField.euler * f;
            return new RotationField(v);
        }

        public static RotationField operator *(RotationField rotationField, double d)
        {
            var v = rotationField.euler * (float)d;
            return new RotationField(v);
        }

        public static RotationField operator *(RotationField rotationField, int i)
        {
            var v = rotationField.euler * i;
            return new RotationField(v);
        }
    }
}