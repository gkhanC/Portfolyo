using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Object;
using LeveGenerator.Abstract;
using LeveGenerator.Builders;
using LeveGenerator.Data;
using Objects;
using Unity.VisualScripting;
using UnityEngine;

namespace LeveGenerator
{
	public class MapGenerator : LevelGeneratorBase
	{
		public MapBuilder builder { get; set; }
		public List<ObjectController> objectsPool = new List<ObjectController>();
		
		[field: SerializeField] public MapLevelData _levelData;
		private bool isBuilt => !builder.IsNull() && builder.isBuilt;

		public MapLevelData levelData
		{
			get => _levelData;
			private set => _levelData = value;
		}

		public override void Generate()
		{
#if UNITY_EDITOR
			if (levelData.IsNull())
				return;

			builder = new MapBuilder(_levelData);

			var objectsData = builder.Build();

			if (objectsData == null || objectsData.Length < 1)
				return;

			var stage = new GameObject("newStage");
			stage.transform.position = Vector3.up;
			levelData.objectCount = objectsData.Length;

			foreach (var data in objectsData)
			{
				var instance = Instantiate(_levelData.objectPrefab.gameObject, stage.transform)
					.GetComponent<ObjectController>();
				var instanceTransform = instance.transform;
				instanceTransform.position = data.position;
				instanceTransform.localScale = Vector3.one * _levelData.objectSize;
				instance.SetColor(data.color);
			}

			_levelData.stages.Add(stage);
#endif
		}

		public override void Clean()
		{
			builder.isBuilt = false;
			_levelData.stages.Clear();
		}
	}
}