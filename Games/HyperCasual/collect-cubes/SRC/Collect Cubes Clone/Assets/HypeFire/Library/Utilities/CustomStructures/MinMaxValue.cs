using System;

namespace HypeFire.Utilities.CustomStructures
{
	[Serializable]
	public struct MinMaxValue <T> : IMinMaxValue<T>
	{
		public T min;
		public T max;
		
		public  T Min
		{
			get => min;
			set => min = value;
		}

		public T Max
		{
			get => max;
			set => max = value;
		}

	}
}