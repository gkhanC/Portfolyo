using UnityEngine;

namespace HypeFire.Library.Controllers.Move.Abstract
{
    public interface IRigidbodyMoveController : IMoveController
    {
        Vector3 velocity { get; set; }

        /// <summary>
        /// Fizik tarafından yönetilmesine izin verilen vektör yönünü belirler
        /// <para>
        /// 1 değeri ilgili yön için fiziksel kuvvetleri aktif hale getirir
        /// </para>
        /// </summary>
        Vector3 physicalOverriding { get; set; }
    }
}