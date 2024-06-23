using System;
using Random = UnityEngine.Random;

namespace HypeFire.Utilities.CustomStructures
{
	[Serializable]
	public struct MinMaxInt : IMinMaxValue<int>
	{
		public int min;
		public int max;

		public int Min
		{
			get => min;
			set => min = value;
		}

		public int Max
		{
			get => max;
			set => max = value;
		}

		public int TakeRandom()
		{
			return Random.Range(min, max);
		}
		
		public MinMaxInt(float min, float max)
		{
			this.min = (int)min;
			this.max = (int)max;
		}
		
		public MinMaxInt(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		public static implicit operator MinMaxInt(RangedInt rangedInt)
		{
			var result = new MinMaxInt(rangedInt.min, rangedInt.max);
			return result;
		}
	}
}