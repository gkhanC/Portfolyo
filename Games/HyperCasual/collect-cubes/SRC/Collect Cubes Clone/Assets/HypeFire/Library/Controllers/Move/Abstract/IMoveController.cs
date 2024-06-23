using HypeFire.Library.Structures;
using UnityEngine;

namespace HypeFire.Library.Controllers.Move.Abstract
{
    public interface IMoveController
    {
        bool isMoving { get; }
        
        float speed { get; set; }

        RotationField direction { get; set; }

        /// <summary>
        /// Auto move özelliğini aktif eder ve move yöntemini uygular.
        /// </summary>
        void OnMove();

        /// <summary>
        /// Nesneye velocity uygular.
        /// </summary>
        void Moving();

        /// <summary>
        /// Nesneye velocity uygular.
        /// </summary>
        /// <param name="speed">Velocity kuvveti -> speed * Time.deltaTime</param>
        /// <param name="direction">Velocity yönü</param>
        void Move(float speed = 0f, Vector3 direction = default);

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

        /// <summary>
        /// direction bilgisini güncelle.
        /// </summary>
        /// <param name="direction"></param>
        void UpdateDirection(Vector3 direction);
    }
}