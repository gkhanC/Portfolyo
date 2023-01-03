using UnityEngine;

namespace HypeFire.Library.Utilities.ObjectPool.Abstract
{
    public interface IPool<T> where T : MonoBehaviour
    {
        /// <summary>
        /// Havuzdan bir örnek alır.
        /// </summary>
        /// <returns></returns>
        T Dequeue();

        /// <summary>
        /// Havuza bir örnek ekler.
        /// </summary>
        /// <param name="Value"></param>
        void Enqueue(T Value);

        /// <summary>
        /// Havuzda nesne örnepi var mı kontrol eder.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        bool Contains(T Value);
    }
}