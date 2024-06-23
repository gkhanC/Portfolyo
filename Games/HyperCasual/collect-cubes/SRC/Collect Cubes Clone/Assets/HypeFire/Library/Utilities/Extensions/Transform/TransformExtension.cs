using UnityEngine;

namespace HypeFire.Library.Utilities.Extensions.Transform
{
    public static class TransformExtension
    {
        public static Vector3 GetDirection(this UnityEngine.Transform origin, Vector3 targetPosition) => (targetPosition - origin.position).normalized;
        public static Vector3 GetDirection(this UnityEngine.Transform origin,  UnityEngine.Transform targetPosition) => (targetPosition.position - origin.position).normalized;
    }
}