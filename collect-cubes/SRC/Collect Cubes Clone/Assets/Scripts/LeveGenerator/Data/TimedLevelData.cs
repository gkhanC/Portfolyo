using HypeFire.Library.Utilities.Logger;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Objects;
using System;

namespace LeveGenerator.Data
{
	[Serializable]
	[CreateAssetMenu(fileName = "TimedLevelData", menuName = "LevelGenerator/TimedData")]
	public class TimedLevelData : LevelData
	{
		[field: SerializeField] public ObjectControllerWithPool objectPrefab { get; protected set; } = null;
		[field: SerializeField] private float _spawnDelay = 1f;
		[field: SerializeField] private float _spawnRange = 4f;
		[field: SerializeField] private List<Color> _objectColors;
		[field: SerializeField] private List<Vector3> _spawnPoints = new List<Vector3>();
		public List<Vector3> GetSpawnPoint => _spawnPoints;
		public List<GameObject> stages = new List<GameObject>();

		public List<Color> getColors => _objectColors;
		public float getSpawnRange => _spawnRange;

		public float spawnDelayTime
		{
			get => _spawnDelay;
			set => SetSpawnDelayTime(value);
		}

		private void SetSpawnDelayTime(float time)
		{
			if (time <= 0)
			{
				_spawnDelay = 0f;
				spawnDelayTime.LogWarning("The time variable cannot be negative.");
				return;
			}

			_spawnDelay = time;
		}

		public TimedLevelData(int objectCount, ObjectController objectPrefab, float spawnDelay,
			params Vector3[] spawnPoints) : base(objectCount)
		{
			SetSpawnDelayTime(spawnDelay);
			_spawnPoints = spawnPoints.ToList();
		}

		public TimedLevelData()
		{
		}

		private void OnEnable()
		{
			this.LogSuccess($"A new {nameof(TimedLevelData)} Asset has been created");
		}
	}
}