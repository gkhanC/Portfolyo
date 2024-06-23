using UnityEngine;

namespace HypeFire.Library.GameSpecial.Qualification.Abstract
{
    public interface IRotateAble
    {
        /// <summary>
        /// is Character jump able
        /// </summary>
        public  bool isRotateAble { get; }
        
        /// <summary>
        /// Jumps to character
        /// </summary>
        /// <param name="direction">Vector3 => jumping direction</param>
        public void Rotate( Vector3 direction);

    }
}