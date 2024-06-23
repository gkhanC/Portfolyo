namespace HypeFire.Utilities.CustomStructures
{
    using System;
    
    /// <summary>
    /// Ranged ve Randomize edilmiş Float tipidir.
    /// </summary>
    [Serializable]
    public class RangedFloat : GenericRangedValues<float>, IRandomValue<float>
    {
        public float GetRandomValue()
        => UnityEngine.Random.Range(min, max);

        public RangedFloat(bool condition)
        {
            this.isMaxGreaterThanMin = condition;
        }

        public RangedFloat(float min = 0f, float max = 0f, bool isMaxGreaterThanMin = false)
        {
            this.isMaxGreaterThanMin = isMaxGreaterThanMin;
            this.min = min;
            this.max = max;
        }
        
        public static implicit operator RangedFloat(MinMaxFloat minMaxFloat)
        {
            var result = new RangedFloat(minMaxFloat.min, minMaxFloat.max);
            return result;
        }
    }
}