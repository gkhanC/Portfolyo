/*
______________________________________________________
 * Created By: Gökhan Tutku Çay
 * Email: caygkhan@gmail.com
 * 
 * Date: 10 May 2023
 * 
 * Copyright (c) 2023  Gökhan Tutku Çay. All rights reserved.
______________________________________________________
*/


using System;
using HypeFire.Library.Controllers.InputControllers.ScreenToWorldPosition;
using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Singleton;
using HypeFire.Templates.Runner.CharacterController;
using HypeFire.Templates.Runner.Managers.GameSettings;
using HypeFire.Utilities.CustomStructures;
using UnityEngine;

namespace HypeFire.Templates.Runner.Managers
{
	[ExecuteInEditMode]
	[Serializable]
	public class RunnerManager : MonoBehaviourSingleton<RunnerManager>
	{
		#region ManagerSettings

		public static RunnerManager GloballAcces => _instance;

		#endregion

		#region GameSettings

		public PlatformSettings platformSettings = new PlatformSettings();
		public GameConstraints gameConstraints = new GameConstraints();

		#endregion

		public GameObject player = null;
		public GameObject inputReader = null;

		public void Initial()
		{
			FindAndDeleteOthers();

			var objs = FindObjectsOfType(typeof(RunnerGameManager)) as RunnerGameManager[];
			if (objs.Length > 1)
			{
				foreach (var component in objs)
				{
					if (!component.gameObject.Equals(_instance.gameObject))
					{
						DestroyImmediate(component);
					}
				}
			}
			else
			{
				var go = new GameObject("Runner Game Manager");
				go.AddComponent<RunnerGameManager>();
			}
		}

		public void CreatePlatform()
		{
#if UNITY_EDITOR
			if (platformSettings.platformInstance.IsNotNull())
			{
				DestroyImmediate(platformSettings.platformInstance);
			}

			var instance =
				Instantiate(platformSettings.platformPrefab, Vector3.zero,
					Quaternion.identity) as GameObject;
			instance.name = "Runner Platform";
			instance.transform.localScale = new Vector3(platformSettings.platformSize.x,
				instance.transform.localScale.y, platformSettings.platformSize.y);

			var pos = Vector3.zero;
			pos.z = (instance.transform.localScale.z * .5f) - 10f;

			var mesh = instance.GetComponent<MeshRenderer>();
			var yOffset = mesh.IsNotNull() ? mesh.bounds.size.y : instance.transform.position.y;
			pos.y = -.5f + (-.5f * yOffset);
			instance.transform.position = pos;

			platformSettings.platformInstance = instance;
#endif
		}

		public void Reset()
		{
		}
	}
}