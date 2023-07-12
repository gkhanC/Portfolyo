using System;
using Random = UnityEngine.Random;

namespace HypeFire.Utilities.CustomStructures
{
	[Serializable]
	public class MinMaxFloat : IMinMaxValue<float>
	{
		public float min;
		public float max;

		public float Min
		{
			get => min;
			set => min = value;
		}

		public float Max
		{
			get => max;
			set => max = value;
		}

		public float TakeRandom()
		{
			return Random.Range(min, max);
		}

		public MinMaxFloat(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		public MinMaxFloat(int min, int max)
		{
			this.min = (float)min;
			this.max = (float)max;
		}

		public static implicit operator MinMaxFloat(RangedFloat rangedFloat)
		{
			var result = new MinMaxFloat(rangedFloat.min, rangedFloat.max);
			return result;
		}
	}
}