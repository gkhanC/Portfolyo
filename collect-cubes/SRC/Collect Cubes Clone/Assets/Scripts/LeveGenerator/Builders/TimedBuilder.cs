using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LeveGenerator.Builders.Abstract;
using LeveGenerator.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LeveGenerator.Builders
{
	[Serializable]
	public class TimedBuilder : BuilderBase
	{
		[field: SerializeField] private TimedLevelData _data;

		[CanBeNull]
		public override ObjectData[] Build()
		{
			var spawnPoint = _data.GetSpawnPoint;
			var objectCountForSpawner = _data.objectCount / spawnPoint.Count;
			var objectsData = new List<ObjectData>();
			var colors = _data.getColors;

			for (int i = 0; i < objectCountForSpawner; i++)
			{
				foreach (var VARIABLE in spawnPoint)
				{
					var randCircle = Random.insideUnitCircle * _data.getSpawnRange;
					var pos = VARIABLE + new Vector3(randCircle.x, 0f, randCircle.y);
					var color = colors[i % colors.Count];
					objectsData.Add(new ObjectData(color, pos));
				}
			}

			if (objectsData.Count < _data.objectCount)
			{
				var addCount = _data.objectCount - objectsData.Count;
				for (int i = 0; i < addCount; i++)
				{
					var randCircle = Random.insideUnitCircle * _data.getSpawnRange;
					var pos = spawnPoint[0] + new Vector3(randCircle.x, 0f, randCircle.y);
					var color = colors[i % colors.Count];
					objectsData.Add(new ObjectData(color, pos));
				}
			}

			isBuilt = true;
			return objectsData.ToArray();
		}

		public TimedBuilder(TimedLevelData data)
		{
			_data = data;
		}
	}
}