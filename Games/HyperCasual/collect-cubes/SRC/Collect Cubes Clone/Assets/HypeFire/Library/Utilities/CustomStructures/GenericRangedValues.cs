namespace HypeFire.Utilities.CustomStructures
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Ranged türler için genel temel sınıftır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class GenericRangedValues<T> where T : struct
    {
        [SerializeField] public bool isMaxGreaterThanMin = false;

        /// <summary>
        /// value of min.
        /// </summary>
        /// <value></value>
        public T min
        {
            get => m_min;
            set => m_min = value;
        }

        [field: SerializeField] public T m_min;

        /// <summary>
        /// value of max
        /// </summary>
        /// <value></value>
        public T max
        {
            get => m_max;
            set => m_max = value;
        }

        [field: SerializeField] public T m_max;
    }
}