namespace HypeFire.Library.Controllers.InputControllers.Abstract
{
    /// <summary>
    /// Kontrolcü taraflı derlenen kullanıcı girdisine ait veriyi saklar. 
    /// </summary>
    public interface IInputResult
    {
        /// <summary>
        /// Girdi verisinin erişim anahtarı.
        /// </summary>
        InputData data { get; set; }
    }
}