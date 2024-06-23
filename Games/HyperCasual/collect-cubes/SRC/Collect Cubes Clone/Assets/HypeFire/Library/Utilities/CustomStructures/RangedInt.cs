namespace HypeFire.Utilities.CustomStructures
{
    using System;

    [Serializable]
    public class RangedInt : GenericRangedValues<int>, IRandomValue<int>
    {
        public int GetRandomValue()
            => UnityEngine.Random.Range(min, max);

        public RangedInt(bool condition)
        {
            this.isMaxGreaterThanMin = condition;
        }

        public RangedInt(int min = 0, int max = 0, bool isMaxGreaterThanMin = false)
        {
            this.isMaxGreaterThanMin = isMaxGreaterThanMin;
            this.min = min;
            this.max = max;
        }
        
        public static implicit operator RangedInt(MinMaxInt minMaxInt)
        {
            var result = new RangedInt(minMaxInt.min, minMaxInt.max);
            return result;
        }
        
    }
}