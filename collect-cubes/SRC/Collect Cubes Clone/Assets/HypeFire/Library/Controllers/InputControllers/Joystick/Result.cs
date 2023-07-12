using HypeFire.Library.Controllers.InputControllers.Joystick.Abstract;

namespace HypeFire.Library.Controllers.InputControllers.Joystick  
{
    using UnityEngine;

    /// <summary>
    /// Kontrolcü taraflı derlenen kullanıcı girdisine ait veriyi saklar. 
    /// </summary>
    public struct Result : IJoystickResult
    {
        public InputData data { get; set; }

        public Result(bool active, Vector2 direction)
        {
            data = new InputData(active, direction);
        }
        
        public Result(bool active, Vector3 position)
        {
            data = new InputData(active, position);
        }
    }
}