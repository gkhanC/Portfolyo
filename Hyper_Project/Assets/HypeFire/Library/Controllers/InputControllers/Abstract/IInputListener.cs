namespace HypeFire.Library.Controllers.InputControllers.Abstract
{
    /// <summary>
    /// Kontrolcü tarafından derlenen, kullanıcı girdisini dinler.
    /// </summary>
    public interface IInputListener
    {
        /// <summary>
        /// Giriş verisini Input Manager üzerindeki "Listeners event" e abone olarak dinler.
        /// Start yordamı içerisinde Input Manager aracılığıyla Listeners Event'e abone edilmelidir.
        /// </summary>
        /// <param name="result"> as IInputResult</param>
        void InputListening(IInputResult result);
    }
}