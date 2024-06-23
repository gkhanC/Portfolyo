using System;
using UnityEngine;

namespace LeveGenerator.Builders
{
	[Serializable]
	public class ObjectData
	{
		public Color color;
		public Vector3 position;

		public ObjectData(Color c, Vector3 p)
		{
			color = c;
			position = p;
		}

		public ObjectData()
		{
		}
	}
}