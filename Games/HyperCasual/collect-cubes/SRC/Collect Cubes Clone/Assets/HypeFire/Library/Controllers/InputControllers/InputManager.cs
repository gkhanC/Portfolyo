using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Utilities.Singleton;
using UnityEngine;
using UnityEngine.Events;

namespace HypeFire.Library.Controllers.InputControllers
{
    /// <summary>
    /// <code>
    ///  public class ExampleClass : MonoBehaviour, IInputListener
    ///  {
    ///     public void Start()
    ///     {
    ///         InputManager.GetInstance().Listeners.AddListener(this.InputListening);
    ///     }
    ///
    ///     public void InputListening(IInputResult result)
    ///     {
    ///     }
    /// 
    ///   } 
    /// </code>
    /// </summary>
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {
        [field: SerializeField] public InputController controller { get; set; }

        public UnityEvent<IInputResult> Listeners
        {
            get => controller.listeners;
            private set => controller.listeners = value;
        }

        private InputManager() : base()
        {
            //controller = new InputController();
        }
    }
}