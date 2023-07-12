using UnityEngine;

namespace HypeFire.Library.Patterns.Observer.Abstract
{
    public interface IObserverData
    {
        /// <summary>
        /// Event sender information
        /// </summary>
        public  GameObject eventSender { get; }
    }
}