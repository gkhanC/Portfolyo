using HypeFire.Library.Utilities.Extensions.Object;
using JetBrains.Annotations;
using UnityEngine;

namespace HypeFire.Library.Utilities.Extensions.Initializers
{
    public static class MonoBehaviourInitializerExtensions
    {
        /// <summary>
        /// Nesne'ye componenti ekler veya refansı bulup out parametresine set eder.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="component">reference as MonoBehavior</param>
        /// <typeparam name="T">reference type</typeparam>
        /// <returns></returns>
        [CanBeNull]
        public static bool InitComponent<T>(this GameObject Value, out T component) where T : MonoBehaviour
        {
            if (Value.IsNull())
            {
                component = null;
                return false;
            }

            if (Value.TryGetComponent(out component))
            {
                return true;
            }

            component = Value.AddComponent<T>();
            return true;
        }
    }
}