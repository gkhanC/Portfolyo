using HypeFire.Library.Controllers.InputControllers.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace HypeFire.Library.Controllers.InputControllers
{
    /// <summary>
    /// Kullanıcı girdisi kontrolcüleri için temel sınıftır.
    /// </summary>
    public class InputController : MonoBehaviour , IInputController
    {
        /// <summary>
        /// Kullanıcı girdisini dinleyen abonelerdir.
        /// <para>Abone metotlar IInputResult arabiriminden parametre almalıdır.</para>
        /// </summary>
        public UnityEvent<IInputResult> listeners { get; set; } = new UnityEvent<IInputResult>();

        protected virtual void Awake()
        {
            InputManager.GetInstance().controller = this;
        }
    }
}