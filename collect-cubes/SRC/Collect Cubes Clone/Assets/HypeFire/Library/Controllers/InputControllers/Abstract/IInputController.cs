using UnityEngine.Events;

namespace HypeFire.Library.Controllers.InputControllers.Abstract
{
	public interface IInputController
	{
		public UnityEvent<IInputResult> listeners { get; set; }
	}
}