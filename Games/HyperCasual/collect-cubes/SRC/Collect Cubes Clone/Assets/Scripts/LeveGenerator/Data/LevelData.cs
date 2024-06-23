using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace LeveGenerator.Data
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "LevelGenerator/LevelData")]
	public class LevelData : ScriptableObject
	{
		[field: SerializeField] public float objectSize { get; set; } = 1f;
		[field: SerializeField] public int objectCount { get; set; } = 0;

		public LevelData(int objectCount)
		{
			this.objectCount = objectCount;
		}

		public LevelData()
		{
		}

		private void OnEnable()
		{
			this.LogSuccess($"A new {nameof(LevelData)} Asset has been created");
		}
	}
}