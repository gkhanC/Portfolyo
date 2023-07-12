using HypeFire.Library.Controllers.InputControllers.Abstract;
using HypeFire.Library.Controllers.Move;
using HypeFire.Library.Controllers.Rotate;
using HypeFire.Library.GameSpecial.Qualification.Abstract;

namespace HypeFire.Templates.Runner.CharacterController.Abstract
{
    public interface IRunnerCharacterController : IMoveAble, IJumpAble, IRotateAble, IStopAble, IInputListener
    {
        public  RigidbodyMove rigidbodyMove { get; }
        public  RotateController rotateController { get; }

        public void Initial();
    }
}