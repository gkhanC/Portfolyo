using HypeFire.Library.Controllers.InputControllers.Abstract;
using UnityEngine;

namespace HypeFire.Library.Controllers.InputControllers
{
    /// <summary>
    /// Kontrolcü taraflı derlenen kullanıcı girdisine ait veriyi saklar. 
    /// </summary>
    public class InputResult : IInputResult
    {
        [field: SerializeField] public InputData data { get; set; } = new InputData();

        public InputResult()
        {
        }

        public InputResult(InputData data)
        {
            this.data = data;
        }
        
        public InputResult(bool active, Vector2 direction)
        {
            data = new InputData(active, direction);
        }

        public InputResult(bool active, Vector3 direction)
        {
            data = new InputData(active, direction);
        }
    }
}