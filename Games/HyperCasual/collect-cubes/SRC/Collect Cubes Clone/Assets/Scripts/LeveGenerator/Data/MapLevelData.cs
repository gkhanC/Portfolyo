using HypeFire.Library.Utilities.Logger;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Objects;
using System;

namespace LeveGenerator.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "MapLevelData", menuName = "LevelGenerator/MapLevelData")]
	public class MapLevelData : LevelData
	{
		[field: SerializeField] public ObjectController objectPrefab { get; protected set; } = null;
		[field: SerializeField] [CanBeNull] public Texture2D topLevelMap = null;
		[field: SerializeField] [CanBeNull] public Texture2D frontLevelMap = null;
		[field: SerializeField] [CanBeNull] public Texture2D sideLevelMap = null;
		
		public List<GameObject> stages = new List<GameObject>();

		public MapLevelData(Texture2D top, Texture2D front, Texture2D side, int objectCount,
			ObjectController objectPrefab) : base(
			objectCount)
		{
			topLevelMap = top;
			frontLevelMap = front;
			sideLevelMap = side;
			this.objectPrefab = objectPrefab;
		}

		public MapLevelData() : base()
		{
		}

		private void OnEnable()
		{
			this.LogSuccess($"A new {nameof(MapLevelData)} Asset has been created");
		}
	}
}