using HypeFire.Library.Structures;
using UnityEngine;

namespace HypeFire.Library.Controllers.Rotate.Abstract
{
    public interface IRotator
    {
        bool isRotating { get; }
        bool useSmoothRotateWithTime { get; set; }

        float rotateSpeed { get; set; }

        Vector3 rotationUpwards { get; set; }
        Vector3 rotationConstraint { get; set; }
        RotationField rotationField { get; set; }
        
        /// <summary>
        /// Objeyi belirtilen yöne rotate eder.
        /// </summary>
        /// <param name="direction">Vector3</param>
        void RotateWithDirection(Vector3 direction);

        /// <summary>
        /// Objeyi belirtilen posizyona bakcak şekilde rotate eder.
        /// </summary>
        /// <param name="position">Vector3</param>
        void RotateToPosition(Vector3 position);
        
        /// <summary>
        /// Objeyi belirtilen yöne rotate eder.
        /// </summary>
        /// <param name="direction">Vector3</param>
        void RotateWithQuaternion(Quaternion direction);

        /// <summary>
        /// Belitilen pozisyona look at uygular.
        /// </summary>
        /// <param name="position">Vector3</param>
        void LookAtPosition(Vector3 position);

    }
}