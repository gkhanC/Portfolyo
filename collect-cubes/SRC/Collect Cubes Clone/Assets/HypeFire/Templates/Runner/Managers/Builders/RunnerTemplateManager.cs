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

using HypeFire.Library.Utilities.Extensions.Object;
using HypeFire.Library.Utilities.Logger;
using UnityEditor;
using UnityEngine;

namespace HypeFire.Templates.Runner.Managers.Builders
{
	public class RunnerTemplateManager
	{
#if UNITY_EDITOR


		[MenuItem(itemName: "Runner Template/Create Runner Manager", false)]
		public static void CreateRunnerManager()
		{
			if (RunnerManager.GloballAcces.IsNull() || RunnerManager.GloballAcces.gameObject.IsNull())
			{
				var go = new GameObject("Runner Template Manager");
				var manager = go.AddComponent<RunnerManager>();

				manager.SetInstance(manager);
				manager.Initial();
				HFLogger.Log(go, "Runner Manager is initialized.");
			}
			else
			{
				RunnerManager.GloballAcces.Reset();
				HFLogger.Log(RunnerManager.GloballAcces, "Runner Manager reset.");
			}
		}
#endif
	}
}