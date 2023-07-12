using UnityEngine;

namespace HypeFire.Library.GameSpecial.Qualification.Abstract
{
    public interface IMoveAble
    {
        /// <summary>
        /// is Character move able 
        /// </summary>
        public  bool isMoveAble { get; }
        
        /// <summary>
        /// Move to character
        /// </summary>
        /// <param name="speed">float => character speed</param>
        /// <param name="direction">Vector3 => character moving direction</param>
        public void Move(float speed, Vector3 direction);

    }
}