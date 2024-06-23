using System;
using UnityEngine;

namespace HypeFire.Templates.Runner.Managers.Builders.LevelGenerator
{
	public class LevelGenerator : MonoBehaviour
	{
	}

	[Serializable]
	public class LootGeneratorData
	{
		public GameObject lootObject;
		public int objectCount = 1;
		public bool isObjectSet = false;
		public Vector3 objectMatrix = Vector3.forward;
		public float matrixOffset = 5f;
		public float objectOffsetInMatrix = 1f;
	}
}