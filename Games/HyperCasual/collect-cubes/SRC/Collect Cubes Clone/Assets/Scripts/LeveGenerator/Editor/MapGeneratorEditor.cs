using HypeFire.Library.Utilities.Extensions.Object;
using LeveGenerator.Builders;
using UnityEditor;
using UnityEngine;

namespace LeveGenerator.Editor
{
	[CustomEditor(typeof(MapGenerator))]
	[CanEditMultipleObjects]
	public class MapGeneratorEditor : UnityEditor.Editor
	{
		public MapGenerator generator;
		public SerializedProperty levelData;

		private void OnEnable()
		{
			generator = target as MapGenerator;
			levelData = serializedObject.FindProperty(nameof(MapGenerator._levelData));
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.PropertyField(levelData);

			if (generator._levelData.IsNull())
			{
				EditorGUILayout.HelpBox("Level data not found!", MessageType.Error, true);
			}
			else
			{
				if (generator.builder.IsNull())
				{
					generator.builder = new MapBuilder(generator._levelData);
					serializedObject.ApplyModifiedProperties();
				}

				if (!generator.builder.isMapUsable)
				{
					EditorGUILayout.HelpBox("Map images not found or available!", MessageType.Error,
						true);
					return;
				}

				EditorGUILayout.Space(8f);

				if (generator.builder.isBuilt || generator._levelData.stages.Count > 0)
				{
					if (GUILayout.Button("Clean"))
					{
						var objs = generator._levelData.stages;
						for (int i = 0; i < objs.Count; i++)
						{
							DestroyImmediate(objs[i].gameObject);
						}

						generator.Clean();
					}
				}
				else
				{
					if (GUILayout.Button("Generate"))
					{
						generator.Generate();
					}
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}