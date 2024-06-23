namespace HypeFire.Utilities.CustomStructures
{
    /// <summary>
    /// Randomize edilmiş tür arabirimi.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRandomValue<out T>
    {
        /// <summary>
        /// Random bir deger getirir.
        /// </summary>
        /// <returns></returns>
        T GetRandomValue();
    }
}