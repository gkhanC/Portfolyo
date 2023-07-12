/*
______________________________________________________
 * Created By: Gökhan Tutku Çay
 * Email: caygkhan@gmail.com
 * 
 * Date: May 11 2023
 * 
 * Copyright (c) 2023 Gökhan Tutku Çay. All rights reserved.
______________________________________________________
*/

using System;
using HypeFire.Utilities.CustomStructures;
using UnityEngine.Serialization;

namespace HypeFire.Templates.Runner.Managers.GameSettings.Constraints
{
	[Serializable]
	public class PositionalConstraint
	{
		public bool useXConstraint;
		public MinMaxFloat consX = new MinMaxFloat(-3f, 3f);
		public MinMaxFloat offsetX = new MinMaxFloat(.5f, .5f);
		
		public bool useYConstraint;
		public MinMaxFloat consY = new MinMaxFloat(0f, 3f);
		public MinMaxFloat offsetY = new MinMaxFloat(.5f, .5f);

		public bool useZConstraint;
		public MinMaxFloat consZ = new MinMaxFloat(-50f, 50f);
		public MinMaxFloat offsetZ = new MinMaxFloat(.5f, .5f);
	}
}