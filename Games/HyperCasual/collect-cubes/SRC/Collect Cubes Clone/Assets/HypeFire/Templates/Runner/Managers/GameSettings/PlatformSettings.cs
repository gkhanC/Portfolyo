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
using UnityEngine;
using UnityEngine.Serialization;

namespace HypeFire.Templates.Runner.Managers.GameSettings
{
	[Serializable]
	public class PlatformSettings
	{
		public bool useSinglePlatform;
		public GameObject platformPrefab;
		public GameObject platformInstance;
		public Vector2 platformSize;
	}
}