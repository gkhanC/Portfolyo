using HypeFire.Library.Utilities.Extensions.Object;
using LeveGenerator.Builders.Abstract;
using System.Collections.Generic;
using JetBrains.Annotations;
using LeveGenerator.Data;
using UnityEngine;

namespace LeveGenerator.Builders
{
	[SerializeField]
	public class MapBuilder : BuilderBase
	{
		[field: SerializeField] private MapLevelData _data;

		public bool isMapUsable => MapSizeCheck();

		private bool MapSizeCheck()
		{
			if (_data.IsNull()) return false;

			if (_data.topLevelMap.IsNull()) return false;

			if (_data.frontLevelMap.IsNull()) return false;

			if (_data.sideLevelMap.IsNull()) return false;

			var result = (_data.topLevelMap.width == _data.sideLevelMap.width) &&
			             (_data.topLevelMap.width == _data.frontLevelMap.width) &&
			             (_data.topLevelMap.height == _data.sideLevelMap.height) &&
			             (_data.topLevelMap.height == _data.frontLevelMap.height);

			return result;
		}

		[CanBeNull]
		public override ObjectData[] Build()
		{
			if (!isMapUsable) return null;

			var positions = new List<ObjectData>();

			Color[] topPixels = _data.topLevelMap.GetPixels();
			Color[] sidePixels = _data.sideLevelMap.GetPixels();
			Color[] frontPixels = _data.frontLevelMap.GetPixels();

			var width = _data.topLevelMap.width;

			for (int topY = 0; topY < width; topY++)
			{
				for (int topX = 0; topX < width; topX++)
				{
					var topPixelColor = topPixels[topY * width + topX];

					if (topPixelColor.a > .3f)
					{
						var pos = Vector3.zero;
						pos.x = (topX - (width / 2f)) * _data.objectSize;
						pos.z = (topY - (width / 2f)) * _data.objectSize;


						for (int sideY = 0; sideY < width; sideY++)
						{
							var sidePixelColor = sidePixels[sideY * width + topY];

							if (sidePixelColor.a > .3f)
							{
								var otherSideColor = frontPixels[sideY * width + topX];

								if (otherSideColor.a > .3f)
								{
									pos.y = sideY * _data.objectSize;

									positions.Add(
										new ObjectData(topPixelColor, pos));
								}
							}
						}
					}
				}
			}

			isBuilt = true;
			return positions.ToArray();
		}

		public MapBuilder(MapLevelData data)
		{
			_data = data;
		}
	}
}