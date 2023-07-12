/*
______________________________________________________
 * Created By: Gökhan Tutku Çay
 * Email: caygkhan@gmail.com
 * 
 * Date: 11 May 2023
 * 
 * Copyright (c) 2023 Gökhan Tutku Çay. All rights reserved.
______________________________________________________
*/

using System;
using HypeFire.Library.Controllers.InputControllers.ScreenToWorldPosition;
using HypeFire.Library.Utilities.Extensions.Class;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Templates.Runner.CharacterController;
using HypeFire.Templates.Runner.Managers.GameSettings;
using UnityEditor;
using UnityEngine;

namespace HypeFire.Templates.Runner.Managers.Editor
{
	[CustomEditor(typeof(RunnerManager))]
	[CanEditMultipleObjects]
	public class RunnerManagerEditor : UnityEditor.Editor
	{
		private bool isShowPlatformSettings = false,
			isShowConstrains = false,
			isPositionalConstraint = false,
			isGetsThePlatformData = false,
			useXCons = false,
			useZCons = false;

		private float platformSizeX = 6f,
			platformSizeZ = 100f,
			consXmin = 0f,
			consXmax = 0f,
			consZmin = 0f,
			consZmax = 0f;

		private Vector2 consXoffset = new Vector2(.5f, .5f), consZoffset = new Vector2(.5f, .5f);

		private RunnerManager runnerManager;

		private SerializedProperty platformSettings;

		private SerializedProperty useSinglePlatform;

		private void OnEnable()
		{
			runnerManager = target as RunnerManager;

			platformSettings =
				serializedObject.FindProperty(nameof(RunnerManager.platformSettings));

			isPositionalConstraint = runnerManager.gameConstraints.usePositionalConstraint;
			useXCons = runnerManager.gameConstraints.moveAbleConstraints.useXConstraint;
			useXCons = runnerManager.gameConstraints.moveAbleConstraints.useZConstraint;

			consXmin = runnerManager.gameConstraints.moveAbleConstraints.consX.min;
			consXmax = runnerManager.gameConstraints.moveAbleConstraints.consX.max;

			consZmin = runnerManager.gameConstraints.moveAbleConstraints.consZ.min;
			consZmax = runnerManager.gameConstraints.moveAbleConstraints.consZ.max;

			consXoffset.x = runnerManager.gameConstraints.moveAbleConstraints.offsetX.min;
			consXoffset.y = runnerManager.gameConstraints.moveAbleConstraints.offsetX.max;

			consZoffset.x = runnerManager.gameConstraints.moveAbleConstraints.offsetZ.min;
			consZoffset.y = runnerManager.gameConstraints.moveAbleConstraints.offsetZ.max;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			isShowPlatformSettings =
				EditorGUILayout.BeginFoldoutHeaderGroup(isShowPlatformSettings, "Platform Settings");


			if (isShowPlatformSettings)
			{
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(20f);

				EditorGUILayout.BeginVertical();

				if (runnerManager.platformSettings.useSinglePlatform)
				{
					EditorGUILayout.HelpBox(
						"The manager adds movement restriction according to the platform size.",
						MessageType.None);
				}

				runnerManager.platformSettings.useSinglePlatform =
					EditorGUILayout.ToggleLeft("Use Single Platform",
						runnerManager.platformSettings.useSinglePlatform);

				if (runnerManager.platformSettings.useSinglePlatform)
				{
					EditorGUILayout.Separator();
					runnerManager.platformSettings.platformPrefab = (GameObject)
						EditorGUILayout.ObjectField("Platform Prefab",
							runnerManager.platformSettings.platformPrefab,
							typeof(GameObject), false);

					if (runnerManager.platformSettings.platformPrefab.IsNotNull() &&
					    runnerManager.platformSettings.platformPrefab.gameObject.IsNotNull())
					{
						EditorGUILayout.Separator();
						EditorGUILayout.HelpBox(
							"Sets platform size.",
							MessageType.None);
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.Separator();
						EditorGUILayout.BeginVertical();
						platformSizeX = EditorGUILayout.FloatField("Size X ", platformSizeX);
						platformSizeZ = EditorGUILayout.FloatField("Size Z ", platformSizeZ);
						runnerManager.platformSettings.platformSize.x = platformSizeX;
						runnerManager.platformSettings.platformSize.y = platformSizeZ;
						EditorGUILayout.EndVertical();
						EditorGUILayout.EndHorizontal();

						if (GUILayout.Button("Create Platform"))
						{
							runnerManager.CreatePlatform();
						}
					}
				}

				EditorGUILayout.EndVertical();

				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndFoldoutHeaderGroup();

			if (runnerManager.platformSettings.platformInstance.IsNotNull())
			{
				EditorGUILayout.Separator();
				isShowConstrains =
					EditorGUILayout.BeginFoldoutHeaderGroup(
						isShowConstrains, "Game Constraints");

				if (isShowConstrains)
				{
					EditorGUILayout.Separator();
					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(20f);
					isPositionalConstraint = EditorGUILayout.ToggleLeft(
						"Apply Positional Constraint",
						isPositionalConstraint);
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.Separator();

					runnerManager.gameConstraints.usePositionalConstraint = isPositionalConstraint;

					if (isPositionalConstraint)
					{
						EditorGUILayout.BeginHorizontal();
						GUILayout.Space(40f);

						EditorGUILayout.BeginVertical();

						useXCons = EditorGUILayout.ToggleLeft("Use X Constraint", useXCons);
						if (useXCons)
						{
							isGetsThePlatformData = EditorGUILayout.ToggleLeft(
								"Gets The Platform Data",
								isGetsThePlatformData);
							if (isGetsThePlatformData)
							{
								var mesRend = runnerManager.platformSettings
									.platformInstance
									.GetComponent<MeshRenderer>();
								if (mesRend.IsNotNull())
								{
									var boudMin = mesRend.bounds.min.x;
									var boudMax = mesRend.bounds.max.x;
									consXmin = boudMin;
									consXmax = boudMax;
								}
							}

							consXoffset =
								EditorGUILayout.Vector2Field("X Cons Offset",
									consXoffset);

							consXmin = EditorGUILayout.FloatField("X Cons Min",
								consXmin);
							consXmax = EditorGUILayout.FloatField("X Cons Max",
								consXmax);
						}

						EditorGUILayout.Separator();

						useZCons = EditorGUILayout.ToggleLeft("Use Z Constraint", useZCons);
						if (useZCons)
						{
							consZoffset =
								EditorGUILayout.Vector2Field("Z Cons Offset",
									consZoffset);

							consZmin = EditorGUILayout.FloatField("Z Cons Min",
								consZmin);
							consZmax = EditorGUILayout.FloatField("Z Cons Max",
								consZmax);
						}

						EditorGUILayout.EndVertical();

						EditorGUILayout.EndHorizontal();

						runnerManager.gameConstraints.moveAbleConstraints.useXConstraint =
							useXCons;
						runnerManager.gameConstraints.moveAbleConstraints.useZConstraint =
							useZCons;

						runnerManager.gameConstraints.moveAbleConstraints.consX
							.min = consXmin;
						runnerManager.gameConstraints.moveAbleConstraints.consX
							.max = consXmax;

						runnerManager.gameConstraints.moveAbleConstraints.consZ
							.min = consZmin;
						runnerManager.gameConstraints.moveAbleConstraints.consZ
							.max = consZmax;

						runnerManager.gameConstraints.moveAbleConstraints
							.offsetX.min = consXoffset.x;
						runnerManager.gameConstraints.moveAbleConstraints
							.offsetX.max = consXoffset.y;

						runnerManager.gameConstraints.moveAbleConstraints
							.offsetZ.min = consZoffset.x;
						runnerManager.gameConstraints.moveAbleConstraints
							.offsetZ.max = consZoffset.y;
					}
				}

				EditorGUILayout.EndFoldoutHeaderGroup();
			}

			if (runnerManager.player.IsNull())
			{
				EditorGUILayout.Separator();
				if (GUILayout.Button("Create New Player"))
				{
					var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
					go.name = "Player-runner";
					go.tag = "Player";
					var player = go.AddComponent<RunnerPlayer>();
					runnerManager.player = player.gameObject;

					if (runnerManager.inputReader.IsNotNull())
					{
						runnerManager.GetComponent<STWReader>().Init(Camera.main,
							runnerManager.player.gameObject);
					}
				}
			}
			else
			{
				EditorGUILayout.Separator();
				if (runnerManager.player.IsNotNull() && runnerManager.inputReader.IsNull())
				{
					if (GUILayout.Button("Create Input Reader"))
					{
						if (runnerManager.inputReader.IsNull())
						{
							var readerGo = new GameObject("Input Reader");
							var reader = readerGo.AddComponent<STWReader>();
							reader.Init(Camera.main,
								runnerManager.player.gameObject);
							runnerManager.inputReader = readerGo;
						}
					}
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}