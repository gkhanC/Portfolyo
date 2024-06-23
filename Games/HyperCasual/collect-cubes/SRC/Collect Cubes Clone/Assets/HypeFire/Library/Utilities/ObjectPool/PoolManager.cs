using System;
using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.ObjectPool.Abstract;
using HypeFire.Library.Utilities.Singleton;
using UnityEngine;

namespace HypeFire.Library.Utilities.ObjectPool
{
    public class PoolManager : MonoBehaviourSingleton<PoolManager>, IPoolManager
    {
        public Vector3 poolPosition = new Vector3(0f, -100f, 0f);

        [Header("Pool Info")] public List<PoolInfo> poolStash = new List<PoolInfo>();

        [field: SerializeField]
        private Dictionary<Type, Pool<MonoBehaviour>> pools { get; set; } =
            new Dictionary<Type, Pool<MonoBehaviour>>();


        public void Start()
        {
           transform.position = poolPosition;
        }

        public GameObject TakeInstanceAsObject<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>
        {
            var key = typeof(T);

            if (pools.ContainsKey(key))
            {
                if (pools[key].Count > 0)
                {
                    var result = pools[key].Dequeue();

                    GameObject o;

                    (o = result.gameObject).SetActive(true);
                    o.transform.SetParent(null);
                    return o;
                }
            }

            if (monoObject.IsNull())
                return null;

            return CreateObjetInstance(monoObject.gameObject);
        }

        public T TakeInstanceAsComponent<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>
        {
            if (monoObject.IsNull())
                return null;

            var result = TakeInstanceAsObject(monoObject).GetComponent<T>();
            return result;
        }

        public bool AddObject<T>(T monoObject) where T : MonoBehaviour, IPoolObject<T>
        {
            if (monoObject.IsNull() || monoObject.gameObject.IsNull())
                return false;

            var key = typeof(T);

            if (pools.ContainsKey(key))
            {
                if (pools[key].Contains(monoObject))
                    return false;

                pools[key].Enqueue(monoObject);

                HideObject(monoObject.gameObject);
                return true;
            }

            var q = new Pool<MonoBehaviour>(monoObject);
            pools.Add(key, q);

            poolStash.Add(q.info);

            HideObject(monoObject.gameObject);

            return true;
        }

        public bool CreateObjectPool<T>(T monoType) where T : MonoBehaviour, IPoolObject<T>
        {
            var key = typeof(T);
            var q = new Pool<MonoBehaviour>();
            pools.Add(key, q);
            return true;
        }

        private GameObject CreateObjetInstance(GameObject gObject)
        {
            var obj = Instantiate(gObject, transform.position, Quaternion.identity);
            return obj;
        }

        private void HideObject(GameObject gObject)
        {
            gObject.SetActive(false);
            var mTransform = transform;
            gObject.transform.position = mTransform.position;
            gObject.transform.SetParent(mTransform);
        }

        [Serializable]
        public class Pool<T> : IPool<T> where T : MonoBehaviour
        {
            public Queue<T> storage = new Queue<T>();
            public PoolInfo info;

            public int Count => storage.Count;

            public T Dequeue()
            {
                var result = storage.Dequeue();
                info.objectCount = storage.Count;
                return result;
            }

            public void Enqueue(T Value)
            {
                storage.Enqueue(Value);
                info.objectCount = storage.Count;
            }

            public bool Contains(T Value)
            {
                return storage.Contains(Value);
            }

            public Pool()
            {
                info = new PoolInfo();
                info.key = typeof(T);
                storage = new Queue<T>();
                info.objectCount = storage.Count;
                info.SetName(info.key.Name);
            }

            public Pool(T Value)
            {
                info = new PoolInfo();
                info.key = typeof(T);
                storage = new Queue<T>();
                storage.Enqueue(Value);

                info.objectCount = storage.Count;
                info.SetName(Value.gameObject.name);
            }
        }


        [Serializable]
        public class PoolInfo
        {
            [SerializeField] private string name = "";
            private Type _key = typeof(object);

            public Type key
            {
                get => _key;
                set { _key = value; }
            }

            public void SetName(string Name) => name = Name;

            public int objectCount = 0;
        }
    }
}