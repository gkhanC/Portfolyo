using UnityEngine;

namespace HypeFire.Library.Controllers.Rotate.Abstract
{
    public interface IAutoRotator : IRotator
    {
        bool isAutoMoveEnabled { get; set; }
        Transform targetTransform { get; set; }

        /// <summary>
        /// Nesneyi durdur.
        /// </summary>
        void Stop();

        /// <summary>
        /// Nesneyi time ile durdur.
        /// Belirtilen süre boyunca yavaşlatarak.
        /// </summary>
        /// <param name="stopTime"></param>
        void Stop(float stopTime);
    }
}