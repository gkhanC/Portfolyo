using UnityEngine;

namespace HypeFire.Library.Utilities.ObjectPool.Abstract
{
    public interface IPoolManager
    {
        /// <summary>
        /// Havuzdan paremetre ile belirtilen obje örneğini döndürür.
        /// </summary>
        /// <param name="monoObject">Obje bilgisi</param>
        /// <typeparam name="T">IPoolObject arayüzünü uygulayan nesne örneği</typeparam>
        /// <returns></returns>
        GameObject TakeInstanceAsObject<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>;

        /// <summary>
        /// Parametre ile belirtilen nesne örneğini döndürür.
        /// </summary>
        /// <param name="monoObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T TakeInstanceAsComponent<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>;
        
        /// <summary>
        /// Havuza nesne ekler.
        /// </summary>
        /// <param name="monoObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool AddObject<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>;
    }
}