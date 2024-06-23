using UnityEngine;

namespace HypeFire.Library.Utilities.ObjectPool.Abstract
{
    public interface IPoolObject<T> where T : MonoBehaviour
    {
        /// <summary>
        /// nesne örneğine erişir.
        /// </summary>
        /// <returns></returns>
        T TakeInstance();

        /// <summary>
        /// Nesne pooldan çıkarıldıktan sonraki işlemleri yürütür.
        /// </summary>
        void WakeUp();

        /// <summary>
        /// Nesneyi poola ekler ve öncesindeki işlemleri yürütür.
        /// </summary>
        void Sleep();
    }
}