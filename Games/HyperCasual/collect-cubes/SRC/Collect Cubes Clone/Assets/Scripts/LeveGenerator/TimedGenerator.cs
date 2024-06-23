using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HypeFire.Library.Utilities.Extensions.Object;
using LeveGenerator.Abstract;
using LeveGenerator.Builders;
using LeveGenerator.Data;
using Managers;
using Objects;
using UnityEngine;

namespace LeveGenerator
{
	public class TimedGenerator : LevelGeneratorBase
	{
		public TimedLevelData levelData;
		public List<ObjectController> objectPool = new List<ObjectController>();
		public List<ObjectData> objectsData = new List<ObjectData>();
		
		private bool isBuilt => !_timedBuilder.IsNull() && _timedBuilder.isBuilt;
		private TimedBuilder _timedBuilder;

		private void Update()
		{
			if (LevelManager.GloballAccess.isLevelStarted && !isBuilt)
			{
				Generate();
				StartCoroutine(this.SpawnCoroutine());
			}
		}

		public override void Generate()
		{
			if (levelData.IsNull())
				return;

			if (isBuilt) return;

			_timedBuilder = new TimedBuilder(levelData);

			var data = _timedBuilder.Build();
			if (data != null) objectsData.AddRange(data.ToArray());
		}

		public IEnumerator SpawnCoroutine()
		{
			foreach (var VARIABLE in objectsData)
			{
				var objController = levelData.objectPrefab.TakeInstance();
				objController.SetColor(VARIABLE.color);
				objController.transform.position = VARIABLE.position;
				objController.transform.localScale = Vector3.one * levelData.objectSize;
				objController.WakeUp();
				yield return new WaitForSeconds(levelData.spawnDelayTime);
			}

			yield return null;
		}

		public override void Clean()
		{
			_timedBuilder.isBuilt = false;
			levelData.stages.Clear();
		}
	}
}